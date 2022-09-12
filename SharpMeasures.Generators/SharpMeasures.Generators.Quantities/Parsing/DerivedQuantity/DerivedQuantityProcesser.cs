namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

public interface IDerivedQuantityProcessingDiagnostics
{
    public abstract Diagnostic? NullExpression(IProcessingContext context, RawDerivedQuantityDefinition definition);
    public abstract Diagnostic? EmptyExpression(IProcessingContext context, RawDerivedQuantityDefinition definition);
    public abstract Diagnostic? UnmatchedExpressionQuantity(IProcessingContext context, RawDerivedQuantityDefinition definition, int requestedIndex);
    public abstract Diagnostic? ExpressionDoesNotIncludeQuantity(IProcessingContext context, RawDerivedQuantityDefinition definition, int index);

    public abstract Diagnostic? NullSignature(IProcessingContext context, RawDerivedQuantityDefinition definition);
    public abstract Diagnostic? EmptySignature(IProcessingContext context, RawDerivedQuantityDefinition definition);
    public abstract Diagnostic? NullSignatureElement(IProcessingContext context, RawDerivedQuantityDefinition definition, int index);

    public abstract Diagnostic? UnrecognizedOperatorImplementation(IProcessingContext context, RawDerivedQuantityDefinition definition);
    public abstract Diagnostic? ExpressionNotCompatibleWithOperators(IProcessingContext context, RawDerivedQuantityDefinition definition);
}

public class DerivedQuantityProcesser : AProcesser<IProcessingContext, RawDerivedQuantityDefinition, DerivedQuantityDefinition>
{
    private Regex ExpressionQuantityPattern { get; } = new("""{(?'index'[0-9]+)}""", RegexOptions.ExplicitCapture);
    private Regex ValidImplementOperatorsExpressionPattern_TwoQuantities { get; } = new("""^\s*\(*\s*{[01]}\s*\)*\s*[+-/*]\s*\(*\s*{[01]}\s*\)*\s*$""");
    private Regex ValidImplementOperatorsExpressionPattern_OneQuantity { get; } = new("""^\s*\(*\s*1\s*\)*\s*[/]\s*\(*\s*{[0]}\s*\)*\s*$""");

    private IDerivedQuantityProcessingDiagnostics Diagnostics { get; }

    public DerivedQuantityProcesser(IDerivedQuantityProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<DerivedQuantityDefinition> Process(IProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        var processedExpression = ValidateExpressionNotNull(context, definition)
            .Validate(() => ValidateExpressionNotEmpty(context, definition))
            .Validate(() => ValidateExpressionMatchesSignature(context, definition));

        if (processedExpression.IsInvalid)
        {
            return processedExpression.AsEmptyOptional<DerivedQuantityDefinition>();
        }

        var processedSignature = ValidateSignatureNotNull(context, definition)
            .Validate(() => ValidateSignatureNotEmpty(context, definition))
            .Merge(() => ProcessSignature(context, definition));

        if (processedSignature.LacksResult)
        {
            return processedExpression.AsEmptyOptional<DerivedQuantityDefinition>().AddDiagnostics(processedSignature);
        }

        var processedImplementOperators = ProcessOperatorImplementation(context, definition);

        return OptionalWithDiagnostics.Result(ProduceResult(definition, processedSignature.Result, processedImplementOperators.Result), processedExpression.Concat(processedSignature).Concat(processedImplementOperators));
    }

    private static DerivedQuantityDefinition ProduceResult(RawDerivedQuantityDefinition definition, IReadOnlyList<NamedType> signature, DerivationOperatorImplementation operatorImplementation)
    {
        return new(definition.Expression!, signature, operatorImplementation, definition.Permutations, definition.Locations);
    }

    private IValidityWithDiagnostics ValidateExpressionNotNull(IProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression is not null, () => Diagnostics.NullExpression(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotEmpty(IProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression!.Length is not 0, () => Diagnostics.NullExpression(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionMatchesSignature(IProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        var quantityMatches = ExpressionQuantityPattern.Matches(definition.Expression);

        HashSet<int> unincludedUnits = new HashSet<int>();

        for (int i = 0; i < definition.Signature!.Count; i++)
        {
            unincludedUnits.Add(i);
        }

        foreach (var quantityMatch in quantityMatches)
        {
            var requestedIndex = int.Parse(((Match)quantityMatch).Groups["index"].Value, CultureInfo.InvariantCulture);

            if (requestedIndex > definition.Signature.Count - 1)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.UnmatchedExpressionQuantity(context, definition, requestedIndex));
            }

            unincludedUnits.Remove(requestedIndex);
        }

        if (unincludedUnits.Count > 0)
        {
            List<Diagnostic?> diagnostics = new(unincludedUnits.Select((index) => Diagnostics.ExpressionDoesNotIncludeQuantity(context, definition, index)));

            return ValidityWithDiagnostics.Invalid(diagnostics.Where(static (diagnostic) => diagnostic is not null).Select(static (diagnostic) => diagnostic!));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics ValidateSignatureNotNull(IProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Signature is not null, () => Diagnostics.NullSignature(context, definition));
    }

    private IValidityWithDiagnostics ValidateSignatureNotEmpty(IProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Signature.Count is not 0, () => Diagnostics.EmptySignature(context, definition));
    }

    private IOptionalWithDiagnostics<IReadOnlyList<NamedType>> ProcessSignature(IProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        IOptionalWithDiagnostics<NamedType[]> signature = OptionalWithDiagnostics.Result(new NamedType[definition.Signature!.Count]);

        for (int i = 0; i < definition.Signature.Count; i++)
        {
            signature = signature.Merge((signature) => ProcessSignatureElement(context, definition, i, signature));
        }

        return signature.Transform((signature) => (IReadOnlyList<NamedType>)signature);
    }

    private IOptionalWithDiagnostics<NamedType[]> ProcessSignatureElement(IProcessingContext context, RawDerivedQuantityDefinition definition, int index, NamedType[] signature)
    {
        if (definition.Signature[index] is not NamedType signatureElement)
        {
            return OptionalWithDiagnostics.Empty<NamedType[]>(Diagnostics.NullSignatureElement(context, definition, index));
        }

        signature[index] = signatureElement;

        return OptionalWithDiagnostics.Result(signature);
    }

    private IResultWithDiagnostics<DerivationOperatorImplementation> ProcessOperatorImplementation(IProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        var validity = ValidateRecognizedOperatorImplementation(context, definition)
            .Validate(() => ValidateExpressionIfImplementOperators(context, definition));

        if (validity.IsInvalid)
        {
            return ResultWithDiagnostics.Construct(DerivationOperatorImplementation.None, validity);
        }

        return ResultWithDiagnostics.Construct(definition.OperatorImplementation, validity);
    }

    private IValidityWithDiagnostics ValidateRecognizedOperatorImplementation(IProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        var operationImplementationRecognized = Enum.IsDefined(typeof(DerivationOperatorImplementation), definition.OperatorImplementation);

        return ValidityWithDiagnostics.Conditional(operationImplementationRecognized, () => Diagnostics.UnrecognizedOperatorImplementation(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionIfImplementOperators(IProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        var expressionIsValidAndImplementOperators = definition.Locations.ExplicitlySetOperatorImplementation is false || definition.OperatorImplementation is DerivationOperatorImplementation.None
            || ValidImplementOperatorsExpressionPattern_TwoQuantities.IsMatch(definition.Expression!) && definition.Expression!.Contains("{0}") && definition.Expression!.Contains("{1}") && definition.Signature.Count is 2
            || ValidImplementOperatorsExpressionPattern_OneQuantity.IsMatch(definition.Expression!) && definition.Signature.Count is 1;

        return ValidityWithDiagnostics.Conditional(expressionIsValidAndImplementOperators, () => Diagnostics.ExpressionNotCompatibleWithOperators(context, definition));
    }
}

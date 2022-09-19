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
    public abstract Diagnostic? NullExpression(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition);
    public abstract Diagnostic? EmptyExpression(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition);
    public abstract Diagnostic? UnmatchedExpressionQuantity(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition, int requestedIndex);
    public abstract Diagnostic? ExpressionDoesNotIncludeQuantity(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition, int index);

    public abstract Diagnostic? MalformedExpression(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition);
    public abstract Diagnostic? ExpressionContainsConstant(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition);

    public abstract Diagnostic? NullSignature(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition);
    public abstract Diagnostic? EmptySignature(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition);
    public abstract Diagnostic? NullSignatureElement(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition, int index);

    public abstract Diagnostic? UnrecognizedOperatorImplementation(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition);
    public abstract Diagnostic? ExpressionNotCompatibleWithOperators(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition);
}

public interface IDerivedQuantityProcessingContext : IProcessingContext
{
    public abstract QuantityType ResultingQuantityType { get; }
}

public sealed class DerivedQuantityProcesser : AProcesser<IDerivedQuantityProcessingContext, RawDerivedQuantityDefinition, DerivedQuantityDefinition>
{
    private Regex ExpressionQuantityPattern { get; } = new("""{(?'index'[0-9]+)}""", RegexOptions.ExplicitCapture);
    private Regex ValidImplementOperatorsExpressionPattern_TwoQuantities { get; } = new("""^\s*\(*\s*{[01]}\s*\)*\s*[+\-/*]\s*\(*\s*{[01]}\s*\)*\s*$""");
    private Regex ValidImplementOperatorsExpressionPattern_OneQuantity { get; } = new("""^\s*\(*\s*1\s*\)*\s*[/]\s*\(*\s*{[0]}\s*\)*\s*$""");

    private IDerivedQuantityProcessingDiagnostics Diagnostics { get; }

    public DerivedQuantityProcesser(IDerivedQuantityProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<DerivedQuantityDefinition> Process(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        var processedExpression = ValidateExpressionNotNull(context, definition)
            .Validate(() => ValidateExpressionNotEmpty(context, definition))
            .Validate(() => ValidateExpressionFormat(context, definition))
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

    private IValidityWithDiagnostics ValidateExpressionNotNull(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression is not null, () => Diagnostics.NullExpression(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotEmpty(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression!.Length is not 0, () => Diagnostics.NullExpression(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionFormat(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        string expression = definition.Expression!.Replace(" ", string.Empty);

        int parenthesisLevel = 0;

        bool canOpenParenthesis = true;
        bool canCloseParenthesis = false;

        bool canOpenFormatting = true;
        bool isFormatting = false;
        bool canCloseFormatting = false;

        bool canHaveOperator = false;
        bool canHaveConstantOne = true;

        bool requireDivision = false;

        bool canEndExpression = false;

        for (int i = 0; i < expression.Length; i++)
        {
            if (expression[i] is '(')
            {
                if (canOpenParenthesis is false)
                {
                    return ValidityWithDiagnostics.Invalid(Diagnostics.MalformedExpression(context, definition));
                }

                canCloseParenthesis = false;
                canOpenFormatting = true;
                isFormatting = false;
                canCloseFormatting = false;
                canHaveOperator = false;
                canHaveConstantOne = true;
                requireDivision = false;
                canEndExpression = false;

                parenthesisLevel += 1;

                continue;
            }

            if (expression[i] is ')')
            {
                if (canCloseParenthesis is false || parenthesisLevel is 0)
                {
                    return ValidityWithDiagnostics.Invalid(Diagnostics.MalformedExpression(context, definition));
                }

                canOpenParenthesis = false;
                canOpenFormatting = false;
                isFormatting = false;
                canCloseFormatting = false;
                canHaveOperator = true;
                canHaveConstantOne = false;
                requireDivision = false;
                canEndExpression = true;

                parenthesisLevel -= 1;

                continue;
            }

            if (expression[i] is '{')
            {
                if (canOpenFormatting is false)
                {
                    return ValidityWithDiagnostics.Invalid(Diagnostics.MalformedExpression(context, definition));
                }

                canOpenParenthesis = false;
                canCloseParenthesis = false;
                canOpenFormatting = false;
                isFormatting = true;
                canCloseFormatting = false;
                canHaveOperator = false;
                canHaveConstantOne = false;
                requireDivision = false;
                canEndExpression = false;

                continue;
            }

            if (expression[i] is '}')
            {
                if (canCloseFormatting is false)
                {
                    return ValidityWithDiagnostics.Invalid(Diagnostics.MalformedExpression(context, definition));
                }

                canOpenParenthesis = false;
                canCloseParenthesis = true;
                canOpenFormatting = false;
                isFormatting = false;
                canCloseFormatting = false;
                canHaveOperator = true;
                canHaveConstantOne = false;
                requireDivision = false;
                canEndExpression = true;

                continue;
            }

            if (expression[i] is '+' or '-' or '*' or '/' or '.' or 'x')
            {
                if (canHaveOperator is false)
                {
                    return ValidityWithDiagnostics.Invalid(Diagnostics.MalformedExpression(context, definition));
                }

                if (requireDivision && expression[i] is not '/')
                {
                    return ValidityWithDiagnostics.Invalid(Diagnostics.ExpressionContainsConstant(context, definition));
                }

                canOpenParenthesis = true;
                canCloseParenthesis = false;
                canOpenFormatting = true;
                isFormatting = false;
                canCloseFormatting = false;
                canHaveOperator = false;
                canHaveConstantOne = false;
                requireDivision = false;
                canEndExpression = false;

                continue;
            }

            if (char.IsDigit(expression[i]))
            {
                if (isFormatting)
                {
                    canOpenParenthesis = false;
                    canOpenFormatting = false;
                    isFormatting = true;
                    canCloseFormatting = true;
                    canHaveOperator = false;
                    canHaveConstantOne = false;
                    requireDivision = false;
                    canEndExpression = false;

                    continue;
                }

                if (canHaveConstantOne is false || expression[i] is not '1')
                {
                    return ValidityWithDiagnostics.Invalid(Diagnostics.ExpressionContainsConstant(context, definition));
                }

                canOpenParenthesis = false;
                canOpenFormatting = false;
                isFormatting = false;
                canCloseFormatting = false;
                canHaveOperator = true;
                canHaveConstantOne = false;
                requireDivision = true;
                canEndExpression = false;

                continue;
            }

            return ValidityWithDiagnostics.Invalid(Diagnostics.MalformedExpression(context, definition));
        }

        if (canEndExpression is false || parenthesisLevel is not 0)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.MalformedExpression(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics ValidateExpressionMatchesSignature(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
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

    private IValidityWithDiagnostics ValidateSignatureNotNull(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Signature is not null, () => Diagnostics.NullSignature(context, definition));
    }

    private IValidityWithDiagnostics ValidateSignatureNotEmpty(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Signature.Count is not 0, () => Diagnostics.EmptySignature(context, definition));
    }

    private IOptionalWithDiagnostics<IReadOnlyList<NamedType>> ProcessSignature(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        IOptionalWithDiagnostics<NamedType[]> signature = OptionalWithDiagnostics.Result(new NamedType[definition.Signature!.Count]);

        for (int i = 0; i < definition.Signature.Count; i++)
        {
            signature = signature.Merge((signature) => ProcessSignatureElement(context, definition, i, signature));
        }

        return signature.Transform((signature) => (IReadOnlyList<NamedType>)signature);
    }

    private IOptionalWithDiagnostics<NamedType[]> ProcessSignatureElement(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition, int index, NamedType[] signature)
    {
        if (definition.Signature[index] is not NamedType signatureElement)
        {
            return OptionalWithDiagnostics.Empty<NamedType[]>(Diagnostics.NullSignatureElement(context, definition, index));
        }

        signature[index] = signatureElement;

        return OptionalWithDiagnostics.Result(signature);
    }

    private IResultWithDiagnostics<DerivationOperatorImplementation> ProcessOperatorImplementation(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        var validity = ValidateRecognizedOperatorImplementation(context, definition)
            .Validate(() => ValidateExpressionIfImplementOperators(context, definition));

        if (validity.IsInvalid)
        {
            return ResultWithDiagnostics.Construct(DerivationOperatorImplementation.None, validity);
        }

        return ResultWithDiagnostics.Construct(definition.OperatorImplementation, validity);
    }

    private IValidityWithDiagnostics ValidateRecognizedOperatorImplementation(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        var operationImplementationRecognized = Enum.IsDefined(typeof(DerivationOperatorImplementation), definition.OperatorImplementation);

        return ValidityWithDiagnostics.Conditional(operationImplementationRecognized, () => Diagnostics.UnrecognizedOperatorImplementation(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionIfImplementOperators(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        if (definition.OperatorImplementation is DerivationOperatorImplementation.None)
        {
            return ValidityWithDiagnostics.Valid;
        }

        var expressionIsValid = ValidImplementOperatorsExpressionPattern_TwoQuantities.IsMatch(definition.Expression!) && definition.Expression!.Contains("{0}") && definition.Expression!.Contains("{1}") && definition.Signature.Count is 2
            || ValidImplementOperatorsExpressionPattern_OneQuantity.IsMatch(definition.Expression!) && definition.Signature.Count is 1 && context.ResultingQuantityType is QuantityType.Scalar;

        if (expressionIsValid)
        {
            return ValidityWithDiagnostics.Valid;
        }

        return ValidityWithDiagnostics.InvalidWithConditionalDiagnostics(definition.Locations.ExplicitlySetOperatorImplementation, () => Diagnostics.ExpressionNotCompatibleWithOperators(context, definition));
    }
}

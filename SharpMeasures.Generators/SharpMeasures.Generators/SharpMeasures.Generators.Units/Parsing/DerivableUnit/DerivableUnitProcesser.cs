namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;
using System.Linq;

internal interface IDerivableUnitProcessingDiagnostics
{
    public abstract Diagnostic? NullExpression(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition);
    public abstract Diagnostic? EmptyExpression(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition);
    public abstract Diagnostic? EmptySignature(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition);
    public abstract Diagnostic? NullSignatureElement(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition, int index);
    public abstract Diagnostic? DuplicateSignature(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition);
}

internal interface IDerivableUnitProcessingContext : IProcessingContext
{
    public abstract HashSet<DerivableSignature> ReservedSignatures { get; }
}

internal class DerivableUnitProcesser : AActionableProcesser<IDerivableUnitProcessingContext, RawDerivableUnitDefinition, DerivableUnitDefinition>
{
    private IDerivableUnitProcessingDiagnostics Diagnostics { get; }

    public DerivableUnitProcesser(IDerivableUnitProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition, DerivableUnitDefinition product)
    {
        context.ReservedSignatures.Add(product.Signature);
    }

    public override IOptionalWithDiagnostics<DerivableUnitDefinition> Process(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        var expressionValidity = CheckExpressionValidity(context, definition);
        IEnumerable<Diagnostic> allDiagnostics = expressionValidity.Diagnostics;

        if (expressionValidity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<DerivableUnitDefinition>(allDiagnostics);
        }

        var processedSignature = ProcessSignature(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedSignature.Diagnostics);

        if (processedSignature.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<DerivableUnitDefinition>(allDiagnostics);
        }

        DerivableUnitDefinition product = new(definition.Expression!, processedSignature.Result, definition.Locations);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IValidityWithDiagnostics CheckExpressionValidity(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        if (definition.Expression is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullExpression(context, definition));
        }

        if (definition.Expression.Length is 0)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.EmptyExpression(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IOptionalWithDiagnostics<DerivableSignature> ProcessSignature(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        if (definition.Signature.Count is 0)
        {
            return OptionalWithDiagnostics.Empty<DerivableSignature>(Diagnostics.EmptySignature(context, definition));
        }

        NamedType[] definiteSignature = new NamedType[definition.Signature.Count];

        for (int i = 0; i < definition.Signature.Count; i++)
        {
            if (definition.Signature[i] is not NamedType signatureComponent)
            {
                return OptionalWithDiagnostics.Empty<DerivableSignature>(Diagnostics.NullSignatureElement(context, definition, i));
            }

            definiteSignature[i] = signatureComponent;
        }

        var derivableSignature = new DerivableSignature(definiteSignature);

        if (context.ReservedSignatures.Contains(derivableSignature))
        {
            return OptionalWithDiagnostics.Empty<DerivableSignature>(Diagnostics.DuplicateSignature(context, definition));
        }

        return OptionalWithDiagnostics.Result(derivableSignature);
    }
}

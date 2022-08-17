namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Raw.Units;

using System.Collections.Generic;

internal interface IDerivableUnitProcessingDiagnostics
{
    public abstract Diagnostic? MultipleDerivationsButNotNamed(IDerivableUnitProcessingContext context, UnprocessedDerivableUnitDefinition definition);
    public abstract Diagnostic? DuplicateDerivationID(IDerivableUnitProcessingContext context, UnprocessedDerivableUnitDefinition definition);
    public abstract Diagnostic? NullExpression(IDerivableUnitProcessingContext context, UnprocessedDerivableUnitDefinition definition);
    public abstract Diagnostic? EmptyExpression(IDerivableUnitProcessingContext context, UnprocessedDerivableUnitDefinition definition);
    public abstract Diagnostic? NullSignature(IDerivableUnitProcessingContext context, UnprocessedDerivableUnitDefinition definition);
    public abstract Diagnostic? EmptySignature(IDerivableUnitProcessingContext context, UnprocessedDerivableUnitDefinition definition);
    public abstract Diagnostic? NullSignatureElement(IDerivableUnitProcessingContext context, UnprocessedDerivableUnitDefinition definition, int index);
}

internal interface IDerivableUnitProcessingContext : IProcessingContext
{
    public abstract bool MultipleDefinitions { get; }
    public abstract HashSet<string> ReservedIDs { get; }
}

internal class DerivableUnitProcesser : AActionableProcesser<IDerivableUnitProcessingContext, UnprocessedDerivableUnitDefinition, RawDerivableUnitDefinition>
{
    private IDerivableUnitProcessingDiagnostics Diagnostics { get; }

    public DerivableUnitProcesser(IDerivableUnitProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IDerivableUnitProcessingContext context, UnprocessedDerivableUnitDefinition definition, RawDerivableUnitDefinition product)
    {
        if (product.DerivationID is not null)
        {
            context.ReservedIDs.Add(product.DerivationID);
        }
    }

    public override IOptionalWithDiagnostics<RawDerivableUnitDefinition> Process(IDerivableUnitProcessingContext context, UnprocessedDerivableUnitDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateDerivationIDNotAmbiguous(context, definition))
            .Validate(() => ValidateDerivationIDNotDuplicate(context, definition))
            .Validate(() => ValidateExpressionNotNull(context, definition))
            .Validate(() => ValidateExpressionIsNotEmpty(context, definition))
            .Validate(() => ValidateSignatureNotNull(context, definition))
            .Validate(() => ValidateSignatureNotEmpty(context, definition))
            .Merge(() => ProcessSignature(context, definition))
            .Transform((signature) => ProduceResult(definition, signature));
    }

    private static RawDerivableUnitDefinition ProduceResult(UnprocessedDerivableUnitDefinition definition, RawUnitDerivationSignature signature)
    {
        return new(definition.DerivationID, definition.Expression!, signature, definition.Locations);
    }

    private static IValidityWithDiagnostics VerifyRequiredPropertiesSet(UnprocessedDerivableUnitDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Locations.ExplicitlySetExpression);
    }

    private IValidityWithDiagnostics ValidateDerivationIDNotAmbiguous(IDerivableUnitProcessingContext context, UnprocessedDerivableUnitDefinition definition)
    {
        var ambiguousDerivation = definition.DerivationID is null or { Length: 0 } && context.MultipleDefinitions;

        return ValidityWithDiagnostics.Conditional(ambiguousDerivation is false, () => Diagnostics.MultipleDerivationsButNotNamed(context, definition));
    }

    private IValidityWithDiagnostics ValidateDerivationIDNotDuplicate(IDerivableUnitProcessingContext context, UnprocessedDerivableUnitDefinition definition)
    {
        var derivationIDNotDuplicate = definition.DerivationID is null or { Length: 0 } || context.ReservedIDs.Contains(definition.DerivationID!) is false;

        return ValidityWithDiagnostics.Conditional(derivationIDNotDuplicate, () => Diagnostics.DuplicateDerivationID(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotNull(IDerivableUnitProcessingContext context, UnprocessedDerivableUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression is not null, () => Diagnostics.NullExpression(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionIsNotEmpty(IDerivableUnitProcessingContext context, UnprocessedDerivableUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression!.Length is not 0, () => Diagnostics.EmptyExpression(context, definition));
    }

    private IValidityWithDiagnostics ValidateSignatureNotNull(IDerivableUnitProcessingContext context, UnprocessedDerivableUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Signature is not null, () => Diagnostics.NullSignature(context, definition));
    }

    private IValidityWithDiagnostics ValidateSignatureNotEmpty(IDerivableUnitProcessingContext context, UnprocessedDerivableUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Signature!.Count is not 0, () => Diagnostics.EmptySignature(context, definition));
    }

    private IOptionalWithDiagnostics<RawUnitDerivationSignature> ProcessSignature(IDerivableUnitProcessingContext context, UnprocessedDerivableUnitDefinition definition)
    {
        NamedType[] definiteSignature = new NamedType[definition.Signature!.Count];

        for (int i = 0; i < definition.Signature.Count; i++)
        {
            if (definition.Signature[i] is not NamedType signatureComponent)
            {
                return OptionalWithDiagnostics.Empty<RawUnitDerivationSignature>(Diagnostics.NullSignatureElement(context, definition, i));
            }

            definiteSignature[i] = signatureComponent;
        }

        var derivableSignature = new RawUnitDerivationSignature(definiteSignature);

        return OptionalWithDiagnostics.Result(derivableSignature);
    }
}

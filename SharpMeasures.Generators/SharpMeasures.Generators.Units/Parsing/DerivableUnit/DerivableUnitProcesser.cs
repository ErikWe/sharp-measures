namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

internal interface IDerivableUnitProcessingDiagnostics
{
    public abstract Diagnostic? UnitIncludesBiasTerm(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition);
    public abstract Diagnostic? MultipleDerivationsButNotNamed(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition);
    public abstract Diagnostic? DuplicateDerivationID(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition);
    public abstract Diagnostic? DuplicateDerivationSignature(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition);
    public abstract Diagnostic? NullExpression(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition);
    public abstract Diagnostic? EmptyExpression(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition);
    public abstract Diagnostic? NullSignature(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition);
    public abstract Diagnostic? EmptySignature(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition);
    public abstract Diagnostic? DerivationSignatureNotPermutable(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition);
    public abstract Diagnostic? NullSignatureElement(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition, int index);
}

internal interface IDerivableUnitProcessingContext : IProcessingContext
{
    public abstract bool UnitIncludesBiasTerm { get; }

    public abstract bool MultipleDefinitions { get; }
    public abstract HashSet<string> ReservedIDs { get; }
    public abstract HashSet<DerivableUnitSignature> ReservedSignatures { get; }
}

internal class DerivableUnitProcesser : AActionableProcesser<IDerivableUnitProcessingContext, RawDerivableUnitDefinition, DerivableUnitDefinition>
{
    private IDerivableUnitProcessingDiagnostics Diagnostics { get; }

    public DerivableUnitProcesser(IDerivableUnitProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition,DerivableUnitDefinition  product)
    {
        if (product.DerivationID is not null)
        {
            context.ReservedIDs.Add(product.DerivationID);
        }

        context.ReservedSignatures.Add(new DerivableUnitSignature(product.Signature));
    }

    public override IOptionalWithDiagnostics<DerivableUnitDefinition> Process(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateUnitNotIncludingBiasTerm(context, definition))
            .Validate(() => ValidateDerivationIDNotAmbiguous(context, definition))
            .Validate(() => ValidateDerivationIDNotDuplicate(context, definition))
            .Validate(() => ValidateExpressionNotNull(context, definition))
            .Validate(() => ValidateExpressionIsNotEmpty(context, definition))
            .Validate(() => ValidateSignatureNotNull(context, definition))
            .Validate(() => ValidateSignatureNotEmpty(context, definition))
            .Validate(() => ValidatePermutationsNotRedundant(context, definition))
            .Merge(() => ProcessSignature(context, definition))
            .Validate((signature) => ValidateDerivationSignatureNotDuplicate(context, definition, signature))
            .Transform((signature) => ProduceResult(definition, signature));
    }

    private static DerivableUnitDefinition ProduceResult(RawDerivableUnitDefinition definition, IReadOnlyList<NamedType> signature)
    {
        return new(definition.DerivationID, definition.Expression!, signature, definition.Permutations, definition.Locations);
    }

    private static IValidityWithDiagnostics VerifyRequiredPropertiesSet(RawDerivableUnitDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Locations.ExplicitlySetExpression);
    }

    private IValidityWithDiagnostics ValidateUnitNotIncludingBiasTerm(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(context.UnitIncludesBiasTerm is false, () => Diagnostics.UnitIncludesBiasTerm(context, definition));
    }

    private IValidityWithDiagnostics ValidateDerivationIDNotAmbiguous(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        var ambiguousDerivation = definition.DerivationID is null or { Length: 0 } && context.MultipleDefinitions;

        return ValidityWithDiagnostics.Conditional(ambiguousDerivation is false, () => Diagnostics.MultipleDerivationsButNotNamed(context, definition));
    }

    private IValidityWithDiagnostics ValidateDerivationIDNotDuplicate(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        var derivationIDNotDuplicate = definition.DerivationID is null or { Length: 0 } || context.ReservedIDs.Contains(definition.DerivationID!) is false;

        return ValidityWithDiagnostics.Conditional(derivationIDNotDuplicate, () => Diagnostics.DuplicateDerivationID(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotNull(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression is not null, () => Diagnostics.NullExpression(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionIsNotEmpty(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression!.Length is not 0, () => Diagnostics.EmptyExpression(context, definition));
    }

    private IValidityWithDiagnostics ValidateSignatureNotNull(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Signature is not null, () => Diagnostics.NullSignature(context, definition));
    }

    private IValidityWithDiagnostics ValidateSignatureNotEmpty(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Signature!.Count is not 0, () => Diagnostics.EmptySignature(context, definition));
    }

    private IValidityWithDiagnostics ValidatePermutationsNotRedundant(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        var permutationsRedundant = definition.Locations.ExplicitlySetPermutations && definition.Permutations is true && (definition.Signature!.Count is 1 || allElementsSameType());

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(permutationsRedundant, () => Diagnostics.DerivationSignatureNotPermutable(context, definition));

        bool allElementsSameType()
        {
            foreach (var signatureElement in definition.Signature!)
            {
                if (signatureElement != definition.Signature![0])
                {
                    return false;
                }
            }

            return true;
        }
    }

    private IOptionalWithDiagnostics<IReadOnlyList<NamedType>> ProcessSignature(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        IOptionalWithDiagnostics<NamedType[]> signature = OptionalWithDiagnostics.Result(new NamedType[definition.Signature!.Count]);

        for (int i = 0; i < definition.Signature.Count; i++)
        {
            signature = signature.Merge((signature) => ProcessSignatureElement(context, definition, i, signature));
        }

        return signature.Transform((signature) => (IReadOnlyList<NamedType>)signature);
    }

    private IOptionalWithDiagnostics<NamedType[]> ProcessSignatureElement(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition, int index, NamedType[] signature)
    {
        if (definition.Signature![index] is not NamedType signatureElement)
        {
            return OptionalWithDiagnostics.Empty<NamedType[]>(Diagnostics.NullSignatureElement(context, definition, index));
        }

        signature[index] = signatureElement;

        return OptionalWithDiagnostics.Result(signature);
    }

    private IValidityWithDiagnostics ValidateDerivationSignatureNotDuplicate(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition, IReadOnlyList<NamedType> signature)
    {
        var derivationSignatureNotDuplicate = context.ReservedSignatures.Contains(new DerivableUnitSignature(signature)) is false;

        return ValidityWithDiagnostics.Conditional(derivationSignatureNotDuplicate, () => Diagnostics.DuplicateDerivationSignature(context, definition));
    }
}

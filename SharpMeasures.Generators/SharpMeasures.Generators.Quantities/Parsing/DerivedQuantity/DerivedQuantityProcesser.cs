namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

public interface IDerivedQuantityProcessingDiagnostics
{
    public abstract Diagnostic? NullExpression(IProcessingContext context, RawDerivedQuantityDefinition definition);
    public abstract Diagnostic? EmptyExpression(IProcessingContext context, RawDerivedQuantityDefinition definition);
    public abstract Diagnostic? NullSignature(IProcessingContext context, RawDerivedQuantityDefinition definition);
    public abstract Diagnostic? EmptySignature(IProcessingContext context, RawDerivedQuantityDefinition definition);
    public abstract Diagnostic? NullSignatureElement(IProcessingContext context, RawDerivedQuantityDefinition definition, int index);
}

public class DerivedQuantityProcesser : AProcesser<IProcessingContext, RawDerivedQuantityDefinition, DerivedQuantityDefinition>
{
    private IDerivedQuantityProcessingDiagnostics Diagnostics { get; }

    public DerivedQuantityProcesser(IDerivedQuantityProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<DerivedQuantityDefinition> Process(IProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return ValidateExpressionNotNull(context, definition)
            .Validate(() => ValidateExpressionNotEmpty(context, definition))
            .Validate(() => ValidateSignatureNotNull(context, definition))
            .Validate(() => ValidateSignatureNotEmpty(context, definition))
            .Merge(() => ProcessSignature(context, definition))
            .Transform((signature) => ProduceResult(definition, signature));
    }

    private static DerivedQuantityDefinition ProduceResult(RawDerivedQuantityDefinition definition, IReadOnlyList<NamedType> signature)
    {
        return new(definition.Expression!, signature, definition.ImplementOperators, definition.ImplementAlgebraicallyEquivalentDerivations, definition.Locations);
    }

    private IValidityWithDiagnostics ValidateExpressionNotNull(IProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression is not null, () => Diagnostics.NullExpression(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotEmpty(IProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression!.Length is not 0, () => Diagnostics.NullExpression(context, definition));
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
        IOptionalWithDiagnostics<IList<NamedType>> signature = OptionalWithDiagnostics.Result(new NamedType[definition.Signature!.Count] as IList<NamedType>);

        for (int i = 0; i < definition.Signature.Count; i++)
        {
            signature = signature.Merge((signature) => ProcessSignatureElement(context, definition, i, signature));
        }

        return signature.Transform((signature) => (IReadOnlyList<NamedType>)signature);
    }

    private IOptionalWithDiagnostics<IList<NamedType>> ProcessSignatureElement(IProcessingContext context, RawDerivedQuantityDefinition definition, int index, IList<NamedType> signature)
    {
        if (definition.Signature[index] is not NamedType signatureElement)
        {
            return OptionalWithDiagnostics.Empty<IList<NamedType>>(Diagnostics.NullSignatureElement(context, definition, index));
        }

        signature.Add(signatureElement);

        return OptionalWithDiagnostics.Result(signature);
    }
}

namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Raw.Quantities;

using System.Collections.Generic;

public interface IDerivedQuantityProcessingDiagnostics
{
    public abstract Diagnostic? NullExpression(IDerivedQuantityProcessingContext context, UnprocessedDerivedQuantityDefinition definition);
    public abstract Diagnostic? EmptyExpression(IDerivedQuantityProcessingContext context, UnprocessedDerivedQuantityDefinition definition);
    public abstract Diagnostic? NullSignature(IDerivedQuantityProcessingContext context, UnprocessedDerivedQuantityDefinition definition);
    public abstract Diagnostic? EmptySignature(IDerivedQuantityProcessingContext context, UnprocessedDerivedQuantityDefinition definition);
    public abstract Diagnostic? NullSignatureElement(IDerivedQuantityProcessingContext context, UnprocessedDerivedQuantityDefinition definition, int index);
}

public interface IDerivedQuantityProcessingContext : IProcessingContext
{
    public abstract HashSet<RawQuantityDerivationSignature> ListedDerivations { get; }
}

public class DerivedQuantityProcesser : AActionableProcesser<IDerivedQuantityProcessingContext, UnprocessedDerivedQuantityDefinition, RawDerivedQuantityDefinition>
{
    private IDerivedQuantityProcessingDiagnostics Diagnostics { get; }

    public DerivedQuantityProcesser(IDerivedQuantityProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IDerivedQuantityProcessingContext context, UnprocessedDerivedQuantityDefinition definition, RawDerivedQuantityDefinition product)
    {
        context.ListedDerivations.Add(product.Signature);
    }

    public override IOptionalWithDiagnostics<RawDerivedQuantityDefinition> Process(IDerivedQuantityProcessingContext context, UnprocessedDerivedQuantityDefinition definition)
    {
        return ValidateExpressionNotNull(context, definition)
            .Validate(() => ValidateExpressionNotEmpty(context, definition))
            .Validate(() => ValidateSignatureNotNull(context, definition))
            .Validate(() => ValidateSignatureNotEmpty(context, definition))
            .Merge(() => ProcessSignature(context, definition))
            .Transform((signature) => ProduceResult(definition, signature));
    }

    private static RawDerivedQuantityDefinition ProduceResult(UnprocessedDerivedQuantityDefinition definition, RawQuantityDerivationSignature signature)
    {
        return new(definition.Expression!, signature, definition.ImplementOperators, definition.ImplementAlgebraicallyEquivalentDerivations, definition.Locations);
    }

    private IValidityWithDiagnostics ValidateExpressionNotNull(IDerivedQuantityProcessingContext context, UnprocessedDerivedQuantityDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression is not null, () => Diagnostics.NullExpression(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotEmpty(IDerivedQuantityProcessingContext context, UnprocessedDerivedQuantityDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression!.Length is not 0, () => Diagnostics.NullExpression(context, definition));
    }

    private IValidityWithDiagnostics ValidateSignatureNotNull(IDerivedQuantityProcessingContext context, UnprocessedDerivedQuantityDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Signature is not null, () => Diagnostics.NullSignature(context, definition));
    }

    private IValidityWithDiagnostics ValidateSignatureNotEmpty(IDerivedQuantityProcessingContext context, UnprocessedDerivedQuantityDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Signature.Count is not 0, () => Diagnostics.EmptySignature(context, definition));
    }

    private IOptionalWithDiagnostics<RawQuantityDerivationSignature> ProcessSignature(IDerivedQuantityProcessingContext context, UnprocessedDerivedQuantityDefinition definition)
    {
        NamedType[] definiteSignature = new NamedType[definition.Signature.Count];

        for (int i = 0; i < definition.Signature.Count; i++)
        {
            if (definition.Signature[i] is not NamedType signatureComponent)
            {
                return OptionalWithDiagnostics.Empty<RawQuantityDerivationSignature>(Diagnostics.NullSignatureElement(context, definition, i));
            }

            definiteSignature[i] = signatureComponent;
        }

        var derivableSignature = new RawQuantityDerivationSignature(definiteSignature);

        return OptionalWithDiagnostics.Result(derivableSignature);
    }
}

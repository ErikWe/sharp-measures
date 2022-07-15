namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;
using System.Linq;

public interface IDerivedQuantityProcessingDiagnostics
{
    public abstract Diagnostic? NullExpression(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition);
    public abstract Diagnostic? EmptyExpression(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition);
    public abstract Diagnostic? NullSignature(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition);
    public abstract Diagnostic? EmptySignature(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition);
    public abstract Diagnostic? NullSignatureElement(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition, int index);
}

public interface IDerivedQuantityProcessingContext : IProcessingContext
{
    public abstract HashSet<UnresolvedQuantityDerivationSignature> ListedDerivations { get; }
}

public class DerivedQuantityProcesser : AActionableProcesser<IDerivedQuantityProcessingContext, RawDerivedQuantityDefinition, UnresolvedDerivedQuantityDefinition>
{
    private IDerivedQuantityProcessingDiagnostics Diagnostics { get; }

    public DerivedQuantityProcesser(IDerivedQuantityProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition,
        UnresolvedDerivedQuantityDefinition product)
    {
        context.ListedDerivations.Add(product.Signature);
    }

    public override IOptionalWithDiagnostics<UnresolvedDerivedQuantityDefinition> Process(IDerivedQuantityProcessingContext context,
        RawDerivedQuantityDefinition definition)
    {
        var validity = CheckExpressionValidity(context, definition);
        var allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedDerivedQuantityDefinition>(allDiagnostics);
        }

        var processedSignature = ProcessSignature(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedSignature.Diagnostics);

        if (processedSignature.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedDerivedQuantityDefinition>(allDiagnostics);
        }

        UnresolvedDerivedQuantityDefinition product = new(definition.Expression!, processedSignature.Result, definition.ImplementOperators,
            definition.ImplementAlgebraicallyEquivalentDerivations, definition.Locations);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IValidityWithDiagnostics CheckExpressionValidity(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
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

    private IOptionalWithDiagnostics<UnresolvedQuantityDerivationSignature> ProcessSignature(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        if (definition.Signature is null)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedQuantityDerivationSignature>(Diagnostics.NullSignature(context, definition));
        }

        if (definition.Signature.Count is 0)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedQuantityDerivationSignature>(Diagnostics.EmptySignature(context, definition));
        }

        NamedType[] definiteSignature = new NamedType[definition.Signature.Count];

        for (int i = 0; i < definition.Signature.Count; i++)
        {
            if (definition.Signature[i] is not NamedType signatureComponent)
            {
                return OptionalWithDiagnostics.Empty<UnresolvedQuantityDerivationSignature>(Diagnostics.NullSignatureElement(context, definition, i));
            }

            definiteSignature[i] = signatureComponent;
        }

        var derivableSignature = new UnresolvedQuantityDerivationSignature(definiteSignature);

        return OptionalWithDiagnostics.Result(derivableSignature);
    }
}

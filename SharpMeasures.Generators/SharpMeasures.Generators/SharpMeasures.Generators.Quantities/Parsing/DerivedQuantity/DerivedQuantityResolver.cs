namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Raw.Quantities;
using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Vectors;

public interface IDerivedQuantityResolutionDiagnostics
{
    public abstract Diagnostic? TypeNotQuantity(IDerivedQuantityResolutionContext context, UnresolvedDerivedQuantityDefinition definition, int index);
}

public interface IDerivedQuantityResolutionContext : IProcessingContext
{
    public abstract IRawScalarPopulation ScalarPopulation { get; }
    public abstract IRawVectorPopulation VectorPopulation { get; }
}

public class DerivedQuantityResolver : AProcesser<IDerivedQuantityResolutionContext,  UnresolvedDerivedQuantityDefinition, DerivedQuantityDefinition>
{
    private IDerivedQuantityResolutionDiagnostics Diagnostics { get; }

    public DerivedQuantityResolver(IDerivedQuantityResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<DerivedQuantityDefinition> Process(IDerivedQuantityResolutionContext context, UnresolvedDerivedQuantityDefinition definition)
    {
        var processedSignature = ResolveSignature(context, definition);
        var allDiagnostics = processedSignature.Diagnostics;

        if (processedSignature.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<DerivedQuantityDefinition>(allDiagnostics);
        }

        DerivedQuantityDefinition product = new(definition.Expression, processedSignature.Result, definition.ImplementOperators,
            definition.ImplementAlgebraicallyEquivalentDerivations, definition.Locations);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<QuantityDerivationSignature> ResolveSignature(IDerivedQuantityResolutionContext context, UnresolvedDerivedQuantityDefinition definition)
    {
        var quantities = new IRawQuantityType[definition.Signature.Count];

        for (int i = 0; i < quantities.Length; i++)
        {
            var signatureComponent = ResolveSignatureComponent(context, definition, i);

            if (signatureComponent.LacksResult)
            {
                return OptionalWithDiagnostics.Empty<QuantityDerivationSignature>(signatureComponent.Diagnostics);
            }

            quantities[i] = signatureComponent.Result;
        }

        return OptionalWithDiagnostics.Result(new QuantityDerivationSignature(quantities));
    }

    private IOptionalWithDiagnostics<IRawQuantityType> ResolveSignatureComponent(IDerivedQuantityResolutionContext context, UnresolvedDerivedQuantityDefinition definition,
        int index)
    {
        if (context.ScalarPopulation.Scalars.TryGetValue(definition.Signature[index], out var scalar))
        {
            return OptionalWithDiagnostics.Result(scalar as IRawQuantityType);
        }

        if (context.VectorPopulation.IndividualVectors.TryGetValue(definition.Signature[index], out var vector))
        {
            return OptionalWithDiagnostics.Result(vector as IRawQuantityType);
        }

        return OptionalWithDiagnostics.Empty<IRawQuantityType>(Diagnostics.TypeNotQuantity(context, definition, index));
    }
}

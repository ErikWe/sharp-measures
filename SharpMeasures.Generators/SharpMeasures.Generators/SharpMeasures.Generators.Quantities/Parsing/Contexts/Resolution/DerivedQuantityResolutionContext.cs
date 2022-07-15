namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Vectors;

public record class DerivedQuantityResolutionContext : SimpleProcessingContext, IDerivedQuantityResolutionContext
{
    public IUnresolvedScalarPopulation ScalarPopulation { get; }
    public IUnresolvedVectorPopulation VectorPopulation { get; }

    public DerivedQuantityResolutionContext(DefinedType type, IUnresolvedScalarPopulation scalarPopulation, IUnresolvedVectorPopulation vectorPopulation) : base(type)
    {
        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}

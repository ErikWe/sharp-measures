namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Vectors;

public record class DerivedQuantityResolutionContext : SimpleProcessingContext, IDerivedQuantityResolutionContext
{
    public IRawScalarPopulation ScalarPopulation { get; }
    public IRawVectorPopulation VectorPopulation { get; }

    public DerivedQuantityResolutionContext(DefinedType type, IRawScalarPopulation scalarPopulation, IRawVectorPopulation vectorPopulation) : base(type)
    {
        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}

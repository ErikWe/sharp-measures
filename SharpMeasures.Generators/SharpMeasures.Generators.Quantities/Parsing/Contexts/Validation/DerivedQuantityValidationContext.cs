namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Vectors;

public sealed record class DerivedQuantityValidationContext : IDerivedQuantityValidationContext
{
    public DefinedType Type { get; }

    public IScalarPopulation ScalarPopulation { get; }
    public IVectorPopulation VectorPopulation { get; }

    public DerivedQuantityValidationContext(DefinedType type, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        Type = type;

        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}

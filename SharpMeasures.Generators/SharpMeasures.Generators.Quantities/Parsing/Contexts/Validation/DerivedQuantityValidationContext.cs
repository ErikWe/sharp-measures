namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Vectors;

public record class DerivedQuantityValidationContext : SimpleProcessingContext, IDerivedQuantityValidationContext
{
    public IScalarPopulation ScalarPopulation { get; }
    public IVectorPopulation VectorPopulation { get; }

    public DerivedQuantityValidationContext(DefinedType type, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation) : base(type)
    {
        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}

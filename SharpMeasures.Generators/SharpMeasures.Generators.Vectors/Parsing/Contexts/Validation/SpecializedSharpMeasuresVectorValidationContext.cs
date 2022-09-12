namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

internal record class SpecializedSharpMeasuresVectorValidationContext : SimpleProcessingContext, ISpecializedSharpMeasuresVectorValidationContext
{
    public IUnitPopulation UnitPopulation { get; }
    public IScalarPopulation ScalarPopulation { get; }
    public IVectorPopulationWithData VectorPopulation { get; }

    public SpecializedSharpMeasuresVectorValidationContext(DefinedType type, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulationWithData vectorPopulation) : base(type)
    {
        UnitPopulation = unitPopulation;
        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}

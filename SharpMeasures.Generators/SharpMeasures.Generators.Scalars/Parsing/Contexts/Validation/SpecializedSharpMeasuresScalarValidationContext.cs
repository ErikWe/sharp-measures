namespace SharpMeasures.Generators.Scalars.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

internal record class SpecializedSharpMeasuresScalarValidationContext : SimpleProcessingContext, ISpecializedSharpMeasuresScalarValidationContext
{
    public ScalarProcessingData ProcessingData { get; }

    public IUnitPopulation UnitPopulation { get; }
    public IScalarPopulation ScalarPopulation { get; }
    public IVectorPopulation VectorPopulation { get; }

    public SpecializedSharpMeasuresScalarValidationContext(DefinedType type, ScalarProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation) : base(type)
    {
        ProcessingData = processingData;

        UnitPopulation = unitPopulation;
        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}

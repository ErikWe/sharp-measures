namespace SharpMeasures.Generators.Scalars.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

internal record class SharpMeasuresScalarValidationContext : SimpleProcessingContext, ISharpMeasuresScalarValidationContext
{
    public IUnitPopulation UnitPopulation { get; }
    public IScalarPopulationWithData ScalarPopulation { get; }
    public IVectorPopulation VectorPopulation { get; }

    public SharpMeasuresScalarValidationContext(DefinedType type, IUnitPopulation unitPopulation, IScalarPopulationWithData scalarPopulation, IVectorPopulation vectorPopulation) : base(type)
    {
        UnitPopulation = unitPopulation;
        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}

namespace SharpMeasures.Generators.Units.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Scalars;

internal record class SharpMeasuresUnitValidationContext : SimpleProcessingContext, ISharpMeasuresUnitValidationContext
{
    public IUnitPopulationWithData UnitPopulation { get; }
    public IScalarPopulation ScalarPopulation { get; }

    public SharpMeasuresUnitValidationContext(DefinedType type, IUnitPopulationWithData unitPopulation, IScalarPopulation scalarPopulation) : base(type)
    {
        UnitPopulation = unitPopulation;
        ScalarPopulation = scalarPopulation;
    }
}

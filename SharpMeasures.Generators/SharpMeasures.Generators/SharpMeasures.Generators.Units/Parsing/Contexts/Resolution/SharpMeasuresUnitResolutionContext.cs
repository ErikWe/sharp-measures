namespace SharpMeasures.Generators.Units.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Raw.Scalars;

internal record class SharpMeasuresUnitResolutionContext : SimpleProcessingContext, ISharpMeasuresUnitResolutionContext
{
    public IRawUnitPopulationWithData UnitPopulation { get; }
    public IRawScalarPopulation ScalarPopulation { get; }

    public SharpMeasuresUnitResolutionContext(DefinedType type, IRawUnitPopulationWithData unitPopulation, IRawScalarPopulation scalarPopulation) : base(type)
    {
        UnitPopulation = unitPopulation;
        ScalarPopulation = scalarPopulation;
    }
}

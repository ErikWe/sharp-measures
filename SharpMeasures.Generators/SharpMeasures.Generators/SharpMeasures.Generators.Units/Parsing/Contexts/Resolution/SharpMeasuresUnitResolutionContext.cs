namespace SharpMeasures.Generators.Units.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Unresolved.Scalars;

internal record class SharpMeasuresUnitResolutionContext : SimpleProcessingContext, ISharpMeasuresUnitResolutionContext
{
    public IUnresolvedUnitPopulationWithData UnitPopulation { get; }
    public IUnresolvedScalarPopulation ScalarPopulation { get; }

    public SharpMeasuresUnitResolutionContext(DefinedType type, IUnresolvedUnitPopulationWithData unitPopulation, IUnresolvedScalarPopulation scalarPopulation) : base(type)
    {
        UnitPopulation = unitPopulation;
        ScalarPopulation = scalarPopulation;
    }
}

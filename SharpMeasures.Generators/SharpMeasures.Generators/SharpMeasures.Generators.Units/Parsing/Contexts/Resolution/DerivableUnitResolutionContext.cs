namespace SharpMeasures.Generators.Units.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Raw.Units;

internal record class DerivableUnitResolutionContext : SimpleProcessingContext, IDerivableUnitResolutionContext
{
    public IRawUnitPopulation UnitPopulation { get; }

    public DerivableUnitResolutionContext(DefinedType type, IRawUnitPopulation unitPopulation) : base(type)
    {
        UnitPopulation = unitPopulation;
    }
}

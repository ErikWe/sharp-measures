namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

internal record class RawFixedUnitDefinition : ARawUnitDefinition<FixedUnitLocations>, IRawFixedUnit
{
    public RawFixedUnitDefinition(string name, string plural, FixedUnitLocations locations) : base(name, plural, locations) { }
}

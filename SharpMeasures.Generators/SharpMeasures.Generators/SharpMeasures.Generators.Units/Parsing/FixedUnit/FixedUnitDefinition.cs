namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.UnitInstances;

internal record class FixedUnitDefinition : ARawUnitDefinition<FixedUnitLocations>, IFixedUnit
{
    public FixedUnitDefinition(string name, string plural, FixedUnitLocations locations) : base(name, plural, locations) { }
}

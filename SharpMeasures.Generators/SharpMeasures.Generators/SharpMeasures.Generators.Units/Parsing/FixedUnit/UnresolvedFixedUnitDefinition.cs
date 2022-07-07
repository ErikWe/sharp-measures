namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

internal record class UnresolvedFixedUnitDefinition : AUnresolvedUnitDefinition<FixedUnitLocations>, IUnresolvedFixedUnit
{
    public UnresolvedFixedUnitDefinition(string name, string plural, FixedUnitLocations locations) : base(name, plural, locations) { }
}

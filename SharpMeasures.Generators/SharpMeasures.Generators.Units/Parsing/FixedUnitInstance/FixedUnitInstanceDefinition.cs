namespace SharpMeasures.Generators.Units.Parsing.FixedUnitInstance;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class FixedUnitInstanceDefinition : AUnitInstance<FixedUnitInstanceLocations>, IFixedUnitInstance
{
    IFixedUnitInstanceLocations IFixedUnitInstance.Locations => Locations;

    public FixedUnitInstanceDefinition(string name, string pluralForm, FixedUnitInstanceLocations locations) : base(name, pluralForm, locations) { }
}

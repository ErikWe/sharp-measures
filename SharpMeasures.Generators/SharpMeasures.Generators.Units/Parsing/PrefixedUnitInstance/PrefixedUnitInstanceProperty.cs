namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class PrefixedUnitInstanceProperty<TPropertyType> : AttributeProperty<RawPrefixedUnitInstanceDefinition, PrefixedUnitInstanceLocations, TPropertyType>
{
    public PrefixedUnitInstanceProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
}

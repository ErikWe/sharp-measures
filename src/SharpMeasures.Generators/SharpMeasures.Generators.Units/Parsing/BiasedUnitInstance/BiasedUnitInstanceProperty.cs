namespace SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class BiasedUnitInstanceProperty<TPropertyType> : AttributeProperty<RawBiasedUnitInstanceDefinition, BiasedUnitInstanceLocations, TPropertyType>
{
    public BiasedUnitInstanceProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
}

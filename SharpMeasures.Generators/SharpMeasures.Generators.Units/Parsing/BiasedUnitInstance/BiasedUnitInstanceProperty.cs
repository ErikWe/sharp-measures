namespace SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class BiasedUnitInstanceProperty<TPropertyType> : AttributeProperty<RawBiasedUnitInstanceDefinition, BiasedUnitInstanceLocations, TPropertyType>
{
    public BiasedUnitInstanceProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public BiasedUnitInstanceProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public BiasedUnitInstanceProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public BiasedUnitInstanceProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

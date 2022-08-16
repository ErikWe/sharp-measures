namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class BiasedUnitProperty<TPropertyType> : AttributeProperty<UnprocessedBiasedUnitDefinition, BiasedUnitLocations, TPropertyType>
{
    public BiasedUnitProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public BiasedUnitProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public BiasedUnitProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public BiasedUnitProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

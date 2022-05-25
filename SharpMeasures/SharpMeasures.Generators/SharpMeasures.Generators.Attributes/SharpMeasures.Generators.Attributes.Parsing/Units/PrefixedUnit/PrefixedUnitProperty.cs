namespace SharpMeasures.Generators.Attributes.Parsing.Units;

internal record class PrefixedUnitProperty<TPropertyType> : AttributeProperty<PrefixedUnitDefinition, PrefixedUnitParsingData, PrefixedUnitLocations, TPropertyType>
{
    public PrefixedUnitProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public PrefixedUnitProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public PrefixedUnitProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public PrefixedUnitProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}
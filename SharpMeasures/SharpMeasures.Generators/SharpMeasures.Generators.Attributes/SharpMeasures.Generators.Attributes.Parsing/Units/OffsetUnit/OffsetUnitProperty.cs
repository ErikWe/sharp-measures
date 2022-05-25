namespace SharpMeasures.Generators.Attributes.Parsing.Units;

internal record class OffsetUnitProperty<TPropertyType> : AttributeProperty<OffsetUnitDefinition, OffsetUnitParsingData, OffsetUnitLocations, TPropertyType>
{
    public OffsetUnitProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public OffsetUnitProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public OffsetUnitProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public OffsetUnitProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}
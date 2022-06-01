namespace SharpMeasures.Generators.Attributes.Parsing.Units;

internal record class FixedUnitProperty<TPropertyType> : AttributeProperty<RawFixedUnitDefinition, FixedUnitLocations, TPropertyType>
{
    public FixedUnitProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public FixedUnitProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public FixedUnitProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public FixedUnitProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}
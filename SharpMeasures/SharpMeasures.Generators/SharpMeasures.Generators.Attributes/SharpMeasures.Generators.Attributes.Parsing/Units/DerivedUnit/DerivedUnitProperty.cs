namespace SharpMeasures.Generators.Attributes.Parsing.Units;

internal record class DerivedUnitProperty<TPropertyType> : AttributeProperty<RawDerivedUnitDefinition, DerivedUnitLocations, TPropertyType>
{
    public DerivedUnitProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public DerivedUnitProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public DerivedUnitProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public DerivedUnitProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}
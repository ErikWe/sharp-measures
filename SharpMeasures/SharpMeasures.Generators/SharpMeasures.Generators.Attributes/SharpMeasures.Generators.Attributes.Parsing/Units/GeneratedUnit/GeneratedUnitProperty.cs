namespace SharpMeasures.Generators.Attributes.Parsing.Units;

internal record class GeneratedUnitProperty<TPropertyType> : AttributeProperty<RawGeneratedUnit, GeneratedUnitLocations, TPropertyType>
{
    public GeneratedUnitProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public GeneratedUnitProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public GeneratedUnitProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public GeneratedUnitProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}
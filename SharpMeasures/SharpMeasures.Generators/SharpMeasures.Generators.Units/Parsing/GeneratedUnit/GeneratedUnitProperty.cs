namespace SharpMeasures.Generators.Units.Parsing.GeneratedUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class GeneratedUnitProperty<TPropertyType> : AttributeProperty<RawGeneratedUnitDefinition, GeneratedUnitLocations, TPropertyType>
{
    public GeneratedUnitProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public GeneratedUnitProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public GeneratedUnitProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public GeneratedUnitProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

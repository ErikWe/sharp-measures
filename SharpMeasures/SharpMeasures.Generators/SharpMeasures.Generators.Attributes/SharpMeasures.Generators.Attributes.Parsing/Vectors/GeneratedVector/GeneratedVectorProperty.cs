namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

internal record class GeneratedVectorProperty<TPropertyType>
    : AttributeProperty<RawGeneratedVector, GeneratedVectorLocations, TPropertyType>
{
    public GeneratedVectorProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public GeneratedVectorProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public GeneratedVectorProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public GeneratedVectorProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}
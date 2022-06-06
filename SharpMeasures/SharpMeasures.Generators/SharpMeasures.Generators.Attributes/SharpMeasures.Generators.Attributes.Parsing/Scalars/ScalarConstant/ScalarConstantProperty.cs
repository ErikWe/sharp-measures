namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

internal record class ScalarConstantProperty<TPropertyType>
    : AttributeProperty<RawScalarConstant, ScalarConstantLocations, TPropertyType>
{
    public ScalarConstantProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public ScalarConstantProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public ScalarConstantProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public ScalarConstantProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

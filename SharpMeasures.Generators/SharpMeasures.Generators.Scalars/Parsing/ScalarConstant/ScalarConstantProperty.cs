namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class ScalarConstantProperty<TPropertyType>
    : AttributeProperty<RawScalarConstantDefinition, ScalarConstantLocations, TPropertyType>
{
    public ScalarConstantProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public ScalarConstantProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public ScalarConstantProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public ScalarConstantProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

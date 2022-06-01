namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

internal record class ConstantScalarProperty<TPropertyType>
    : AttributeProperty<RawScalarConstantDefinition, ScalarConstantLocations, TPropertyType>
{
    public ConstantScalarProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public ConstantScalarProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public ConstantScalarProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public ConstantScalarProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}
namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

internal record class GeneratedScalarProperty<TPropertyType>
    : AttributeProperty<RawGeneratedScalar, GeneratedScalarLocations, TPropertyType>
{
    public GeneratedScalarProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public GeneratedScalarProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public GeneratedScalarProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public GeneratedScalarProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}
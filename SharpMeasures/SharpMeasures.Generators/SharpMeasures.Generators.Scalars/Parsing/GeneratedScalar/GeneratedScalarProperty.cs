namespace SharpMeasures.Generators.Scalars.Parsing.GeneratedScalar;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class GeneratedScalarProperty<TPropertyType>
    : AttributeProperty<RawGeneratedScalarDefinition, GeneratedScalarLocations, TPropertyType>
{
    public GeneratedScalarProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public GeneratedScalarProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public GeneratedScalarProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public GeneratedScalarProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

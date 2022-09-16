namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class SharpMeasuresVectorProperty<TPropertyType> : AttributeProperty<SymbolicSharpMeasuresVectorDefinition, SharpMeasuresVectorLocations, TPropertyType>
{
    public SharpMeasuresVectorProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public SharpMeasuresVectorProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public SharpMeasuresVectorProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public SharpMeasuresVectorProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

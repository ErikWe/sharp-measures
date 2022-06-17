namespace SharpMeasures.Generators.Vectors.Parsing.ResizedSharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class ResizedSharpMeasuresVectorProperty<TPropertyType>
    : AttributeProperty<RawResizedSharpMeasuresVectorDefinition, ResizedSharpMeasuresVectorLocations, TPropertyType>
{
    public ResizedSharpMeasuresVectorProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public ResizedSharpMeasuresVectorProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public ResizedSharpMeasuresVectorProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public ResizedSharpMeasuresVectorProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

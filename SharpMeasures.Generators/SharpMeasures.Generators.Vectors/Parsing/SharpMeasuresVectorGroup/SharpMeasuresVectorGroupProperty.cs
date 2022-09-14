namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class SharpMeasuresVectorGroupProperty<TPropertyType> : AttributeProperty<SymbolicSharpMeasuresVectorGroupDefinition, SharpMeasuresVectorGroupLocations, TPropertyType>
{
    public SharpMeasuresVectorGroupProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public SharpMeasuresVectorGroupProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public SharpMeasuresVectorGroupProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public SharpMeasuresVectorGroupProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

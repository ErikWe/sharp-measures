namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class SharpMeasuresVectorProperty<TPropertyType> : AttributeProperty<SymbolicSharpMeasuresVectorDefinition, SharpMeasuresVectorLocations, TPropertyType>
{
    public SharpMeasuresVectorProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
}

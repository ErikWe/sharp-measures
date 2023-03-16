namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class SharpMeasuresVectorGroupProperty<TPropertyType> : AttributeProperty<SymbolicSharpMeasuresVectorGroupDefinition, SharpMeasuresVectorGroupLocations, TPropertyType>
{
    public SharpMeasuresVectorGroupProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
}

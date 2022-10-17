namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class SpecializedSharpMeasuresVectorProperty<TPropertyType> : AttributeProperty<SymbolicSpecializedSharpMeasuresVectorDefinition, SpecializedSharpMeasuresVectorLocations, TPropertyType>
{
    public SpecializedSharpMeasuresVectorProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
}

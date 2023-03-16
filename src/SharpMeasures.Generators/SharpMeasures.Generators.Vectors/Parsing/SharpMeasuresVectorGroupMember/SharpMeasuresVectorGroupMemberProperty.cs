namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class SharpMeasuresVectorGroupMemberProperty<TPropertyType> : AttributeProperty<SymbolicSharpMeasuresVectorGroupMemberDefinition, SharpMeasuresVectorGroupMemberLocations, TPropertyType>
{
    public SharpMeasuresVectorGroupMemberProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
}

namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class SharpMeasuresVectorGroupMemberProperty<TPropertyType>
    : AttributeProperty<RawSharpMeasuresVectorGroupMemberDefinition, SharpMeasuresVectorGroupMemberLocations, TPropertyType>
{
    public SharpMeasuresVectorGroupMemberProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator)
        : base(name, parameterName, setter, locator) { }
    public SharpMeasuresVectorGroupMemberProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public SharpMeasuresVectorGroupMemberProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator)
        : base(name, parameterName, setter, locator) { }
    public SharpMeasuresVectorGroupMemberProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

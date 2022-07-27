namespace SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RegisterVectorGroupMemberProperty<TPropertyType>
    : AttributeProperty<RawRegisterVectorGroupMemberDefinition, RegisterVectorGroupMemberLocations, TPropertyType>
{
    public RegisterVectorGroupMemberProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public RegisterVectorGroupMemberProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public RegisterVectorGroupMemberProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public RegisterVectorGroupMemberProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

namespace SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class IncludeBasesProperty<TPropertyType> : AttributeProperty<RawIncludeBasesDefinition, IncludeBasesLocations, TPropertyType>
{
    public IncludeBasesProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public IncludeBasesProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public IncludeBasesProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public IncludeBasesProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

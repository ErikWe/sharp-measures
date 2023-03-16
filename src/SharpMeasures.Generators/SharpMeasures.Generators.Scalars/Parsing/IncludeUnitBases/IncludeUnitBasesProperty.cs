namespace SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class IncludeUnitBasesProperty<TPropertyType> : AttributeProperty<RawIncludeUnitBasesDefinition, IncludeUnitBasesLocations, TPropertyType>
{
    public IncludeUnitBasesProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public IncludeUnitBasesProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public IncludeUnitBasesProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public IncludeUnitBasesProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

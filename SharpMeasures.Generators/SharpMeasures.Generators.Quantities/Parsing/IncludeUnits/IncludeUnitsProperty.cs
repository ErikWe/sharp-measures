namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class IncludeUnitsProperty<TPropertyType> : AttributeProperty<RawIncludeUnitsDefinition, IncludeUnitsLocations, TPropertyType>
{
    public IncludeUnitsProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public IncludeUnitsProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public IncludeUnitsProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public IncludeUnitsProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

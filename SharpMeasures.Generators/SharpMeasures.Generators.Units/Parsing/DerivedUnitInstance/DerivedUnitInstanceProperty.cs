namespace SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class DerivedUnitInstanceProperty<TPropertyType> : AttributeProperty<RawDerivedUnitInstanceDefinition, DerivedUnitInstanceLocations, TPropertyType>
{
    public DerivedUnitInstanceProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public DerivedUnitInstanceProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public DerivedUnitInstanceProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public DerivedUnitInstanceProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

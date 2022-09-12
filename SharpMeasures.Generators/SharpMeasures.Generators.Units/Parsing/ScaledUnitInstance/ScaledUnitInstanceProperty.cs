namespace SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class ScaledUnitInstanceProperty<TPropertyType> : AttributeProperty<RawScaledUnitInstanceDefinition, ScaledUnitInstanceLocations, TPropertyType>
{
    public ScaledUnitInstanceProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public ScaledUnitInstanceProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public ScaledUnitInstanceProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public ScaledUnitInstanceProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

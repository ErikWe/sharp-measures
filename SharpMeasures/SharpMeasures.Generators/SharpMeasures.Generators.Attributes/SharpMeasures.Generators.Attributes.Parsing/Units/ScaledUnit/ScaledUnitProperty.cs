namespace SharpMeasures.Generators.Attributes.Parsing.Units;

internal record class ScaledUnitProperty<TPropertyType> : AttributeProperty<RawScaledUnit, ScaledUnitLocations, TPropertyType>
{
    public ScaledUnitProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public ScaledUnitProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public ScaledUnitProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public ScaledUnitProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}
namespace SharpMeasures.Generators.Attributes.Parsing.Units;

internal record class DerivableUnitProperty<TPropertyType> : AttributeProperty<RawDerivableUnit, DerivableUnitLocations, TPropertyType>
{
    public DerivableUnitProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public DerivableUnitProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public DerivableUnitProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public DerivableUnitProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}
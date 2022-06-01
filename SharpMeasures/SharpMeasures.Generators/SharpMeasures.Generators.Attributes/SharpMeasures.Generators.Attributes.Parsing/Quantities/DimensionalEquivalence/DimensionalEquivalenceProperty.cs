namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

internal record class DimensionalEquivalenceProperty<TPropertyType> : AttributeProperty<RawDimensionalEquivalenceDefinition,
    DimensionalEquivalenceLocations, TPropertyType>
{
    public DimensionalEquivalenceProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator)
        : base(name, parameterName, setter, locator) { }
    public DimensionalEquivalenceProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public DimensionalEquivalenceProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator)
        : base(name, parameterName, setter, locator) { }
    public DimensionalEquivalenceProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}
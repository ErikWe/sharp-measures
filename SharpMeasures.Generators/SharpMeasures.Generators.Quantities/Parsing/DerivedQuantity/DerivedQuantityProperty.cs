namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class DerivedQuantityProperty<TPropertyType> : AttributeProperty<RawDerivedQuantityDefinition, DerivedQuantityLocations, TPropertyType>
{
    public DerivedQuantityProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public DerivedQuantityProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public DerivedQuantityProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public DerivedQuantityProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

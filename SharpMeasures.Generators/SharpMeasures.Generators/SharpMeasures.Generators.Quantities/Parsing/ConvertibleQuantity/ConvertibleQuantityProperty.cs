namespace SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class ConvertibleQuantityProperty<TPropertyType> : AttributeProperty<UnprocessedConvertibleQuantityDefinition, ConvertibleQuantityLocations, TPropertyType>
{
    public ConvertibleQuantityProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public ConvertibleQuantityProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public ConvertibleQuantityProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public ConvertibleQuantityProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

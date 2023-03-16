namespace SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class ConvertibleQuantityProperty<TPropertyType> : AttributeProperty<SymbolicConvertibleQuantityDefinition, ConvertibleQuantityLocations, TPropertyType>
{
    public ConvertibleQuantityProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
}

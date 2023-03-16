namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class SharpMeasuresUnitProperty<TPropertyType> : AttributeProperty<SymbolicSharpMeasuresUnitDefinition, SharpMeasuresUnitLocations, TPropertyType>
{
    public SharpMeasuresUnitProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
}

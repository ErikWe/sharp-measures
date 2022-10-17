namespace SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class QuantityOperationProperty<TPropertyType> : AttributeProperty<SymbolicQuantityOperationDefinition, QuantityOperationLocations, TPropertyType>
{
    public QuantityOperationProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
}

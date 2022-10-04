namespace SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class QuantityProcessProperty<TPropertyType> : AttributeProperty<RawQuantityProcessDefinition, QuantityProcessLocations, TPropertyType>
{
    public QuantityProcessProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public QuantityProcessProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public QuantityProcessProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public QuantityProcessProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

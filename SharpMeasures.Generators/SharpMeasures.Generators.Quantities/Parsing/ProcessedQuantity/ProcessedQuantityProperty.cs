namespace SharpMeasures.Generators.Quantities.Parsing.ProcessedQuantity;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class ProcessedQuantityProperty<TPropertyType> : AttributeProperty<RawProcessedQuantityDefinition, ProcessedQuantityLocations, TPropertyType>
{
    public ProcessedQuantityProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public ProcessedQuantityProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public ProcessedQuantityProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public ProcessedQuantityProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

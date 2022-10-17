namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class VectorConstantProperty<TPropertyType> : AttributeProperty<RawVectorConstantDefinition, VectorConstantLocations, TPropertyType>
{
    public VectorConstantProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public VectorConstantProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

internal record class VectorConstantProperty<TPropertyType>
    : AttributeProperty<RawVectorConstant, VectorConstantLocations, TPropertyType>
{
    public VectorConstantProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public VectorConstantProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public VectorConstantProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public VectorConstantProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

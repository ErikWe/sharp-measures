namespace SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class VectorOperationProperty<TPropertyType> : AttributeProperty<SymbolicVectorOperationDefinition, VectorOperationLocations, TPropertyType>
{
    public VectorOperationProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public VectorOperationProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public VectorOperationProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public VectorOperationProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

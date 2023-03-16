namespace SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class VectorOperationProperty<TPropertyType> : AttributeProperty<SymbolicVectorOperationDefinition, VectorOperationLocations, TPropertyType>
{
    public VectorOperationProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
}

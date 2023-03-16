namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class SharpMeasuresScalarProperty<TPropertyType> : AttributeProperty<SymbolicSharpMeasuresScalarDefinition, SharpMeasuresScalarLocations, TPropertyType>
{
    public SharpMeasuresScalarProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
}

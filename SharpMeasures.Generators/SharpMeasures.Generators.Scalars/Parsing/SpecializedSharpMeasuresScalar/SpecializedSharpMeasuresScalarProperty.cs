namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class SpecializedSharpMeasuresScalarProperty<TPropertyType> : AttributeProperty<SymbolicSpecializedSharpMeasuresScalarDefinition, SpecializedSharpMeasuresScalarLocations, TPropertyType>
{
    public SpecializedSharpMeasuresScalarProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
}

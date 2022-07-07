namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class SpecializedSharpMeasuresScalarProperty<TPropertyType>
    : AttributeProperty<RawSpecializedSharpMeasuresScalarDefinition, SpecializedSharpMeasuresScalarLocations, TPropertyType>
{
    public SpecializedSharpMeasuresScalarProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public SpecializedSharpMeasuresScalarProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public SpecializedSharpMeasuresScalarProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public SpecializedSharpMeasuresScalarProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}

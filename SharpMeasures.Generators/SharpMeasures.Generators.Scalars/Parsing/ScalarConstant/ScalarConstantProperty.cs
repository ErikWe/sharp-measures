namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class ScalarConstantProperty<TPropertyType> : AttributeProperty<RawScalarConstantDefinition, ScalarConstantLocations, TPropertyType>
{
    public ScalarConstantProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
}

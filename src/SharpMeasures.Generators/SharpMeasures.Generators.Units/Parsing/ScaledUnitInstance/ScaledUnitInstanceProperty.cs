namespace SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class ScaledUnitInstanceProperty<TPropertyType> : AttributeProperty<RawScaledUnitInstanceDefinition, ScaledUnitInstanceLocations, TPropertyType>
{
    public ScaledUnitInstanceProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
}

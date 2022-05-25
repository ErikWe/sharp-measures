namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class DerivableUnitParsingData : AAttributeParsingData<DerivableUnitLocations>
{
    internal static DerivableUnitParsingData Empty { get; } = new();

    private DerivableUnitParsingData() : base(DerivableUnitLocations.Empty) { }
}
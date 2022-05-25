namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class FixedUnitParsingData : AUnitParsingData<FixedUnitLocations>
{
    internal static FixedUnitParsingData Empty { get; } = new();

    private FixedUnitParsingData() : base(FixedUnitLocations.Empty) { }
}
namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class PrefixedUnitParsingData : ADependantUnitParsingData<PrefixedUnitLocations>
{
    public enum PrefixType { None, Metric, Binary }

    internal static PrefixedUnitParsingData Empty { get; } = new();

    public PrefixType SpecifiedPrefixType { get; init; } = PrefixType.None;

    private PrefixedUnitParsingData() : base(PrefixedUnitLocations.Empty) { }
}
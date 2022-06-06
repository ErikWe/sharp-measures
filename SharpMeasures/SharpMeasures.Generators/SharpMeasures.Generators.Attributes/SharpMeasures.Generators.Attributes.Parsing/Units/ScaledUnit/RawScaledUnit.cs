namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class RawScaledUnit : ARawDependantUnitDefinition<ScaledUnitParsingData, ScaledUnitLocations>
{
    internal static RawScaledUnit Empty { get; } = new();

    public string? From => DependantOn;
    public double Scale { get; init; }

    private RawScaledUnit() : base(ScaledUnitLocations.Empty, ScaledUnitParsingData.Empty) { }
}

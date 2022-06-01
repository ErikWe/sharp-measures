namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class RawScaledUnitDefinition : ARawDependantUnitDefinition<ScaledUnitParsingData, ScaledUnitLocations>
{
    internal static RawScaledUnitDefinition Empty { get; } = new();

    public string? From => DependantOn;
    public double Scale { get; init; }

    private RawScaledUnitDefinition() : base(ScaledUnitLocations.Empty, ScaledUnitParsingData.Empty) { }
}
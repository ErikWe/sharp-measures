namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class ScaledUnitParsingData : ADependantUnitParsingData<ScaledUnitLocations>
{
    internal static ScaledUnitParsingData Empty { get; } = new();

    private ScaledUnitParsingData() : base(ScaledUnitLocations.Empty) { }
}
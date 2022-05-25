namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class ScaledUnitDefinition : ADependantUnitDefinition<ScaledUnitParsingData, ScaledUnitLocations>
{
    internal static ScaledUnitDefinition Empty { get; } = new();

    public string From => DependantOn;
    public double Scale { get; init; }

    private ScaledUnitDefinition() : base(ScaledUnitParsingData.Empty) { }
}
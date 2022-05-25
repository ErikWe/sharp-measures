namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class OffsetUnitDefinition : ADependantUnitDefinition<OffsetUnitParsingData, OffsetUnitLocations>
{
    internal static OffsetUnitDefinition Empty { get; } = new();

    public string From => DependantOn;
    public double Offset { get; init; }

    private OffsetUnitDefinition() : base(OffsetUnitParsingData.Empty) { }
}
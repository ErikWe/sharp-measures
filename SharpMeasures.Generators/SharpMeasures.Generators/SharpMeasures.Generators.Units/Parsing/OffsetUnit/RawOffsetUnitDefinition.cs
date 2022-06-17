namespace SharpMeasures.Generators.Units.Parsing.OffsetUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class RawOffsetUnitDefinition : ARawDependantUnitDefinition<OffsetUnitParsingData, OffsetUnitLocations>
{
    internal static RawOffsetUnitDefinition Empty { get; } = new();

    public string? From => DependantOn;
    public double Offset { get; init; }

    private RawOffsetUnitDefinition() : base(OffsetUnitLocations.Empty, OffsetUnitParsingData.Empty) { }
}

namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class RawFixedUnitDefinition : ARawUnitDefinition<FixedUnitParsingData, FixedUnitLocations>
{
    public static RawFixedUnitDefinition Empty { get; } = new();
    
    public double Value { get; init; }
    public double Bias { get; init; }

    private RawFixedUnitDefinition() : base(FixedUnitLocations.Empty, FixedUnitParsingData.Empty) { }
}

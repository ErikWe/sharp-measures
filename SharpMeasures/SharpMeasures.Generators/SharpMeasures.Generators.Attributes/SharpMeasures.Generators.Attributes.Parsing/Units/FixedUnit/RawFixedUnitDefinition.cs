namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class RawFixedUnitDefinition : ARawUnitDefinition<FixedUnitParsingData, FixedUnitLocations>
{
    internal static RawFixedUnitDefinition Empty { get; } = new();
    
    public double Value { get; init; }
    public double Bias { get; init; }

    private RawFixedUnitDefinition() : base(FixedUnitLocations.Empty, FixedUnitParsingData.Empty) { }
}
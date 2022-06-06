namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class RawFixedUnit : ARawUnitDefinition<FixedUnitParsingData, FixedUnitLocations>
{
    internal static RawFixedUnit Empty { get; } = new();
    
    public double Value { get; init; }
    public double Bias { get; init; }

    private RawFixedUnit() : base(FixedUnitLocations.Empty, FixedUnitParsingData.Empty) { }
}

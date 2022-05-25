namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class FixedUnitDefinition : AUnitDefinition<FixedUnitParsingData, FixedUnitLocations>
{
    internal static FixedUnitDefinition Empty { get; } = new();
    
    public double Value { get; init; }
    public double Bias { get; init; }

    private FixedUnitDefinition() : base(FixedUnitParsingData.Empty) { }
}
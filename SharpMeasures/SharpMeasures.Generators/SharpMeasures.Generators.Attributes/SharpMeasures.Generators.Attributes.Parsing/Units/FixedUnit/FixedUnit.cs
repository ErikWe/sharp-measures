namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class FixedUnit : AUnitDefinition<FixedUnitLocations>
{
    public double Value { get; }
    public double Bias { get; }

    public FixedUnit(string name, string plural, double value, double bias, FixedUnitLocations locations) : base(name, plural, locations)
    {
        Value = value;
        Bias = bias;
    }
}

namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class FixedUnitDefinition : AUnitDefinition<FixedUnitLocations>
{
    public double Value { get; }
    public double Bias { get; }

    public FixedUnitDefinition(string name, string plural, double value, double bias, FixedUnitLocations locations) : base(name, plural, locations)
    {
        Value = value;
        Bias = bias;
    }
}

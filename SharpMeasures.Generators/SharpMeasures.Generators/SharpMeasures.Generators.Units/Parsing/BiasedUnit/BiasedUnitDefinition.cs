namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class BiasedUnitDefinition : ADependantUnitDefinition<BiasedUnitLocations>
{
    public string? From => DependantOn;
    public double Bias { get; }
    public string? Expression { get; }

    public BiasedUnitDefinition(string name, string plural, string from, double bias, string? expression, BiasedUnitLocations locations)
        : base(name, plural, from, locations)
    {
        Bias = bias;
        Expression = expression;
    }
}

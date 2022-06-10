namespace SharpMeasures.Generators.Units.Parsing.ScaledUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class ScaledUnitDefinition : ADependantUnitDefinition<ScaledUnitLocations>
{
    public string From => DependantOn;
    public double Scale { get; init; }

    public ScaledUnitDefinition(string name, string plural, string from, double scale, ScaledUnitLocations locations) : base(name, plural, from, locations)
    {
        Scale = scale;
    }
}

namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class ScaledUnit : ADependantUnitDefinition<ScaledUnitLocations>
{
    public string From => DependantOn;
    public double Scale { get; init; }

    public ScaledUnit(string name, string plural, string from, double scale, ScaledUnitLocations locations) : base(name, plural, from, locations)
    {
        Scale = scale;
    }
}

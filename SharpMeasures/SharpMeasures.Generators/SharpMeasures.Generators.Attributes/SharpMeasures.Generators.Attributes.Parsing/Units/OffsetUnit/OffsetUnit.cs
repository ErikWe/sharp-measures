namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class OffsetUnit : ADependantUnitDefinition<OffsetUnitLocations>
{
    public string? From => DependantOn;
    public double Offset { get; }

    public OffsetUnit(string name, string plural, string from, double offset, OffsetUnitLocations locations) : base(name, plural, from, locations)
    {
        Offset = offset;
    }
}

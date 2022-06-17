namespace SharpMeasures.Generators.Units.Parsing.OffsetUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class OffsetUnitDefinition : ADependantUnitDefinition<OffsetUnitLocations>
{
    public string? From => DependantOn;
    public double Offset { get; }

    public OffsetUnitDefinition(string name, string plural, string from, double offset, OffsetUnitLocations locations) : base(name, plural, from, locations)
    {
        Offset = offset;
    }
}

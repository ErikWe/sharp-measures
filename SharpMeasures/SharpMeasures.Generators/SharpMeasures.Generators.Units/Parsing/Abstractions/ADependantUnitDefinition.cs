namespace SharpMeasures.Generators.Units.Parsing.Abstractions;
internal abstract record class ADependantUnitDefinition<TLocations> : AUnitDefinition<TLocations>, IDependantUnitDefinition
    where TLocations : ADependantUnitLocations
{
    public string DependantOn { get; }

    IDependantUnitLocations IDependantUnitDefinition.Locations => Locations;

    protected ADependantUnitDefinition(string name, string plural, string dependantOn, TLocations locations) : base(name, plural, locations)
    {
        DependantOn = dependantOn;
    }
}

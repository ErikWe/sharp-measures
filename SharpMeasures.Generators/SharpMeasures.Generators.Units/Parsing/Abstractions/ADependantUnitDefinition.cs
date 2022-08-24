namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Units.UnitInstances;

internal abstract record class ADependantUnitDefinition<TLocations> : AUnitDefinition<TLocations>, IDependantUnitDefinition<TLocations>
    where TLocations : ADependantUnitLocations<TLocations>
{
    protected string DependantOn { get; }

    string IDependantUnitInstance.DependantOn => DependantOn;

    protected ADependantUnitDefinition(string name, string plural, string dependantOn, TLocations locations) : base(name, plural, locations)
    {
        DependantOn = dependantOn;
    }
}

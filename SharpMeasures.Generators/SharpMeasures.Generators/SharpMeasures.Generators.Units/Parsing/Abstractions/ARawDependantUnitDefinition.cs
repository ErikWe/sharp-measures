namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Raw.Units.UnitInstances;

internal abstract record class ARawDependantUnitDefinition<TLocations> : ARawUnitDefinition<TLocations>, IRawDependantUnitDefinition<TLocations>
    where TLocations : ADependantUnitLocations<TLocations>
{
    protected string DependantOn { get; }

    string IRawDependantUnitInstance.DependantOn => DependantOn;

    protected ARawDependantUnitDefinition(string name, string plural, string dependantOn, TLocations locations) : base(name, plural, locations)
    {
        DependantOn = dependantOn;
    }
}

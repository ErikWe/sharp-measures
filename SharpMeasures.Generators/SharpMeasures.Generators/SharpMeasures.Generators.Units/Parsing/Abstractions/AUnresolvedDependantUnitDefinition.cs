namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

internal abstract record class AUnresolvedDependantUnitDefinition<TLocations> : AUnresolvedUnitDefinition<TLocations>, IUnresolvedDependantUnitDefinition<TLocations>
    where TLocations : ADependantUnitLocations<TLocations>
{
    protected string DependantOn { get; }

    string IUnresolvedDependantUnitInstance.DependantOn => DependantOn;

    protected AUnresolvedDependantUnitDefinition(string name, string plural, string dependantOn, TLocations locations) : base(name, plural, locations)
    {
        DependantOn = dependantOn;
    }
}

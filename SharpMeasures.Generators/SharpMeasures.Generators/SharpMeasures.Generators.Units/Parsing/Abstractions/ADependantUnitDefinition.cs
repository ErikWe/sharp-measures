namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

internal abstract record class ADependantUnitDefinition<TLocations> : AUnitDefinition<TLocations>, IDependantUnitDefinition<TLocations>
    where TLocations : ADependantUnitLocations<TLocations>
{
    protected IUnresolvedUnitInstance DependantOn { get; private init; }

    IUnresolvedUnitInstance IDependantUnitInstance.DependantOn => DependantOn;

    protected ADependantUnitDefinition(string name, string plural, IUnresolvedUnitInstance dependantOn, TLocations locations) : base(name, plural, locations)
    {
        DependantOn = dependantOn;
    }
}

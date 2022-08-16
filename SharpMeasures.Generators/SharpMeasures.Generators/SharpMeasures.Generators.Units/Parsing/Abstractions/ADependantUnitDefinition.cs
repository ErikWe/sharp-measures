namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

internal abstract record class ADependantUnitDefinition<TLocations> : AUnitDefinition<TLocations>, IDependantUnitDefinition<TLocations>
    where TLocations : ADependantUnitLocations<TLocations>
{
    protected IRawUnitInstance DependantOn { get; }

    IRawUnitInstance IDependantUnitInstance.DependantOn => DependantOn;

    protected ADependantUnitDefinition(string name, string plural, IRawUnitInstance dependantOn, TLocations locations) : base(name, plural, locations)
    {
        DependantOn = dependantOn;
    }
}

namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

internal abstract record class ADependantUnitLocations<TLocations> : AUnitLocations<TLocations>, IOpenDependantUnitLocations<TLocations>
    where TLocations : ADependantUnitLocations<TLocations>
{
    protected MinimalLocation? DependantOn { get; private init; }

    protected bool ExplicitlySetDependantOn => DependantOn is not null;

    MinimalLocation? IDependantUnitLocations.DependantOn => DependantOn;
    bool IDependantUnitLocations.ExplicitlySetDependantOn => ExplicitlySetDependantOn;

    protected TLocations WithDependantOn(MinimalLocation dependantOn) => Locations with { DependantOn = dependantOn };
    TLocations IOpenDependantUnitLocations<TLocations>.WithDependantOn(MinimalLocation dependantOn) => WithDependantOn(dependantOn);
}

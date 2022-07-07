namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IDependantUnitLocations : IUnitLocations
{
    public abstract MinimalLocation? DependantOn { get; }

    public abstract bool ExplicitlySetDependantOn { get; }
}

internal interface IOpenDependantUnitLocations<out TLocations> : IDependantUnitLocations, IOpenUnitLocations<TLocations>
    where TLocations : IOpenDependantUnitLocations<TLocations>
{
    public abstract TLocations WithDependantOn(MinimalLocation dependantOn);
}

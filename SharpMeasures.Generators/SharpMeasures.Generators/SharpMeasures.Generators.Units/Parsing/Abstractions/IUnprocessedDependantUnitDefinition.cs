namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IUnprocessedDependantUnitDefinition<out TLocations> : IUnprocessedUnitDefinition<TLocations>
    where TLocations : IDependantUnitLocations
{
    public abstract string? DependantOn { get; }
}

internal interface IOpenUnprocessedDependantUnitDefinition<out TDefinition, TLocations> : IOpenUnprocessedUnitDefinition<TDefinition, TLocations>, IUnprocessedDependantUnitDefinition<TLocations>
    where TDefinition : IOpenUnprocessedDependantUnitDefinition<TDefinition, TLocations>
    where TLocations : IDependantUnitLocations
{
    public abstract TDefinition WithDependantOn(string? dependantOn);
}

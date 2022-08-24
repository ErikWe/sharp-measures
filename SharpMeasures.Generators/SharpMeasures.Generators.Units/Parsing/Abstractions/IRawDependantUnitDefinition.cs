namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IRawDependantUnitDefinition<out TLocations> : IRawUnitDefinition<TLocations>
    where TLocations : IDependantUnitLocations
{
    public abstract string? DependantOn { get; }
}

internal interface IOpenRawDependantUnitDefinition<out TDefinition, TLocations> : IOpenRawUnitDefinition<TDefinition, TLocations>, IRawDependantUnitDefinition<TLocations>
    where TDefinition : IOpenRawDependantUnitDefinition<TDefinition, TLocations>
    where TLocations : IDependantUnitLocations
{
    public abstract TDefinition WithDependantOn(string? dependantOn);
}

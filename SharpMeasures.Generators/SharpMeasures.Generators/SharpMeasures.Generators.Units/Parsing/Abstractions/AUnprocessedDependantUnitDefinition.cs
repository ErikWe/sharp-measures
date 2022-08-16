namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

internal abstract record class AUnprocessedDependantUnitDefinition<TDefinition, TLocations> : AUnprocessedUnitDefinition<TDefinition, TLocations>, IOpenUnprocessedDependantUnitDefinition<TDefinition, TLocations>
    where TDefinition : AUnprocessedDependantUnitDefinition<TDefinition, TLocations>
    where TLocations : IOpenDependantUnitLocations<TLocations>
{
    protected string? DependantOn { get; private init; }

    string? IUnprocessedDependantUnitDefinition<TLocations>.DependantOn => DependantOn;

    protected AUnprocessedDependantUnitDefinition(TLocations locations) : base(locations) { }

    protected TDefinition WithDependantOn(string? dependantOn) => Definition with { DependantOn = dependantOn };

    TDefinition IOpenUnprocessedDependantUnitDefinition<TDefinition, TLocations>.WithDependantOn(string? name) => WithDependantOn(name);
}

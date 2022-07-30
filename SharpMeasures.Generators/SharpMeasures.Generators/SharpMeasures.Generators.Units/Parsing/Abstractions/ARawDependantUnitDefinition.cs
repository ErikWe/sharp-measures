namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

internal abstract record class ARawDependantUnitDefinition<TDefinition, TLocations> : ARawUnitDefinition<TDefinition, TLocations>,
    IOpenRawDependantUnitDefinition<TDefinition, TLocations>
    where TDefinition : ARawDependantUnitDefinition<TDefinition, TLocations>
    where TLocations : IOpenDependantUnitLocations<TLocations>
{
    protected string? DependantOn { get; private init; }

    string? IRawDependantUnitDefinition<TLocations>.DependantOn => DependantOn;

    protected ARawDependantUnitDefinition(TLocations locations) : base(locations) { }

    protected TDefinition WithDependantOn(string? dependantOn) => Definition with { DependantOn = dependantOn };

    TDefinition IOpenRawDependantUnitDefinition<TDefinition, TLocations>.WithDependantOn(string? name) => WithDependantOn(name);
}

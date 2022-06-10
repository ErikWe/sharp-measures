namespace SharpMeasures.Generators.Units.Parsing.Abstractions;
internal abstract record class ADependantUnitLocations : AUnitLocations, IDependantUnitLocations
{
    public MinimalLocation? DependantOn { get; init; }

    public bool ExplicitlySetDependantOn => DependantOn is not null;
}

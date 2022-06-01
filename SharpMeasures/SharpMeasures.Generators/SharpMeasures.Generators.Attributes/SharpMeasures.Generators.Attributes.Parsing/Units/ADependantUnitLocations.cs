namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public abstract record class ADependantUnitLocations : AUnitLocations, IDependantUnitLocations
{
    public MinimalLocation? DependantOn { get; init; }

    public bool ExplicitlySetDependantOn => DependantOn is not null;
}

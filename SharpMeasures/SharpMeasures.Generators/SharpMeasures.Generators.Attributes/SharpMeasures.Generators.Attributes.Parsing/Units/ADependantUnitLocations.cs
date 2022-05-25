namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public abstract record class ADependantUnitLocations : AUnitLocations, IDependantUnitLocations
{
    public MinimalLocation DependantOn { get; init; }
}

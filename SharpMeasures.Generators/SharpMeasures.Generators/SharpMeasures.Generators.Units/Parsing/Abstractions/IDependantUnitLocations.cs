namespace SharpMeasures.Generators.Units.Parsing.Abstractions;
internal interface IDependantUnitLocations : IUnitLocations
{
    public abstract MinimalLocation? DependantOn { get; }

    public abstract bool ExplicitlySetDependantOn { get; }
}

namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public interface IDependantUnitLocations : IUnitLocations
{
    public abstract MinimalLocation? DependantOn { get; }

    public abstract bool ExplicitlySetDependantOn { get; }
}
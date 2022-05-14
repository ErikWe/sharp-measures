namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public interface IDependantUnitDefinition : IUnitDefinition
{
    public abstract string DependantOn { get; }

    public new abstract IDependantUnitLocations Locations { get; }
}
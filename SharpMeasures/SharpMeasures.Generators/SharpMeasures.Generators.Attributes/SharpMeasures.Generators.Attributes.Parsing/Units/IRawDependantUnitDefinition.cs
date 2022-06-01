namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public interface IRawDependantUnitDefinition : IRawUnitDefinition
{
    public abstract string? DependantOn { get; }

    new public abstract IDependantUnitLocations Locations { get; }
}
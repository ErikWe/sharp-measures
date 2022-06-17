namespace SharpMeasures.Generators.Units.Parsing.Abstractions;
internal interface IRawDependantUnitDefinition : IRawUnitDefinition
{
    public abstract string? DependantOn { get; }

    new public abstract IDependantUnitLocations Locations { get; }
}

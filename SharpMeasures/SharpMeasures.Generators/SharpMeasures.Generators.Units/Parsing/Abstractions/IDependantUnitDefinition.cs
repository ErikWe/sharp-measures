namespace SharpMeasures.Generators.Units.Parsing.Abstractions;
internal interface IDependantUnitDefinition : IUnitDefinition
{
    public abstract string DependantOn { get; }

    new public abstract IDependantUnitLocations Locations { get; }
}

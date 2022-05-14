namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public interface IUnitDefinition
{
    public abstract string Name { get; }
    public abstract string Plural { get; }

    public abstract IUnitLocations Locations { get; }
}

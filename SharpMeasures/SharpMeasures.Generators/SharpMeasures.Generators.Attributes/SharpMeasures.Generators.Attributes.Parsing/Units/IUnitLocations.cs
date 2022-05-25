namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public interface IUnitLocations : IAttributeLocations
{
    public abstract MinimalLocation Name { get; }
    public abstract MinimalLocation Plural { get; }
}

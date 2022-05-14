namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

public interface IUnitLocations
{
    public abstract Location Name { get; }
    public abstract Location Plural { get; }
}

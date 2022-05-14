namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

public interface IDependantUnitLocations : IUnitLocations
{
    public abstract Location DependantOn { get; }
}
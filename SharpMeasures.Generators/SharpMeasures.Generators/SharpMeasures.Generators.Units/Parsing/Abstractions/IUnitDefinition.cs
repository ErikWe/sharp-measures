namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;

internal interface IUnitDefinition : IAttributeDefinition
{
    public abstract string Name { get; }
    public abstract string Plural { get; }

    new public abstract IUnitLocations Locations { get; }
}

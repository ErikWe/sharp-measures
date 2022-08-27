namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;

internal interface IUnitLocations : IAttributeLocations
{
    public abstract MinimalLocation? Name { get; }
    public abstract MinimalLocation? Plural { get; }

    public abstract bool ExplicitlySetName { get; }
    public abstract bool ExplicitlySetPlural { get; }
}

internal interface IOpenUnitLocations<out TLocations> : IOpenAttributeLocations<TLocations>, IUnitLocations
    where TLocations : IOpenUnitLocations<TLocations>
{
    public abstract TLocations WithName(MinimalLocation name);
    public abstract TLocations WithPlural(MinimalLocation plural);
}

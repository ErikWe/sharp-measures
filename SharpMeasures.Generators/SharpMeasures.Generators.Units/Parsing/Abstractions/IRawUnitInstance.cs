namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;

internal interface IRawUnitInstance<out TLocations> : IAttributeDefinition<TLocations>
    where TLocations : IUnitInstanceLocations
{
    public abstract string? Name { get; }
    public abstract string? PluralForm { get; }
}

internal interface IOpenRawUnitInstance<out TDefinition, TLocations> : IOpenAttributeDefinition<TDefinition, TLocations>, IRawUnitInstance<TLocations>
    where TDefinition : IOpenRawUnitInstance<TDefinition, TLocations>
    where TLocations : IUnitInstanceLocations
{
    public abstract TDefinition WithName(string? name);
    public abstract TDefinition WithPluralForm(string? pluralForm);
}

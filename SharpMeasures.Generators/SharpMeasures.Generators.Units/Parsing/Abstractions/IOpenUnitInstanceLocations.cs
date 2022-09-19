namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;

internal interface IOpenUnitInstanceLocations<out TLocations> : IOpenAttributeLocations<TLocations>, IUnitInstanceLocations where TLocations : IOpenUnitInstanceLocations<TLocations>
{
    public abstract TLocations WithName(MinimalLocation name);
    public abstract TLocations WithPluralForm(MinimalLocation pluralForm);

    public abstract TLocations WithPluralFormRegexSubstitution(MinimalLocation pluralFormRegexSubstitution);
}

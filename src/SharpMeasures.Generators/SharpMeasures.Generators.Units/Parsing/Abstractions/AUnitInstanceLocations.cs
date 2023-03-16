namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;

internal abstract record class AUnitInstanceLocations<TLocations> : AAttributeLocations<TLocations>, IOpenUnitInstanceLocations<TLocations> where TLocations : AUnitInstanceLocations<TLocations>
{
    public MinimalLocation? Name { get; private init; }
    public MinimalLocation? PluralForm { get; private init; }

    public MinimalLocation? PluralFormRegexSubstitution { get; private init; }

    public bool ExplicitlySetName => Name is not null;
    public bool ExplicitlySetPluralForm => PluralForm is not null;

    public bool ExplicitlySetPluralFormRegexSubstitution => PluralFormRegexSubstitution is not null;

    protected TLocations WithName(MinimalLocation name) => Locations with { Name = name };
    protected TLocations WithPluralForm(MinimalLocation pluralForm) => Locations with { PluralForm = pluralForm };

    protected TLocations WithPluralFormRegexSubstitution(MinimalLocation pluralFormRegexSubstitution) => Locations with { PluralFormRegexSubstitution = pluralFormRegexSubstitution };

    TLocations IOpenUnitInstanceLocations<TLocations>.WithName(MinimalLocation name) => WithName(name);
    TLocations IOpenUnitInstanceLocations<TLocations>.WithPluralForm(MinimalLocation pluralForm) => WithPluralForm(pluralForm);
    TLocations IOpenUnitInstanceLocations<TLocations>.WithPluralFormRegexSubstitution(MinimalLocation pluralFormRegexSubstitution) => WithPluralFormRegexSubstitution(pluralFormRegexSubstitution);
}

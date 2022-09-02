namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;

internal abstract record class ARawUnitInstance<TDefinition, TLocations> : ARawAttributeDefinition<TDefinition, TLocations>, IOpenRawUnitInstance<TDefinition, TLocations>
    where TDefinition : ARawUnitInstance<TDefinition, TLocations>
    where TLocations : IOpenUnitInstanceLocations<TLocations>
{
    public string? Name { get; private init; }
    public string? PluralForm { get; private init; }

    protected ARawUnitInstance(TLocations locations) : base(locations) { }

    protected TDefinition WithName(string? name) => Definition with { Name = name };
    protected TDefinition WithPluralForm(string? pluralForm) => Definition with { PluralForm = pluralForm };

    TDefinition IOpenRawUnitInstance<TDefinition, TLocations>.WithName(string? name) => WithName(name);
    TDefinition IOpenRawUnitInstance<TDefinition, TLocations>.WithPluralForm(string? pluralForm) => WithPluralForm(pluralForm);
}

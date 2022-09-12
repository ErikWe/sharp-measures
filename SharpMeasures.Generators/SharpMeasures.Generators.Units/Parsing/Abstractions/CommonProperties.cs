namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class CommonProperties
{
    public static IAttributeProperty<TDefinition> Name<TDefinition, TLocations>(string name)
        where TDefinition : IOpenRawUnitInstance<TDefinition, TLocations>
        where TLocations : IOpenUnitInstanceLocations<TLocations>
    {
        return new AttributeProperty<TDefinition, TLocations, string>
        (
            name: name,
            setter: static (definition, name) => definition.WithName(name),
            locator: static (locations, nameLocation) => locations.WithName(nameLocation)
        );
    }

    public static IAttributeProperty<TDefinition> PluralForm<TDefinition, TLocations>(string name)
        where TDefinition : IOpenRawUnitInstance<TDefinition, TLocations>
        where TLocations : IOpenUnitInstanceLocations<TLocations>
    {
        return new AttributeProperty<TDefinition, TLocations, string>
        (
            name: name,
            setter: static (definition, pluralForm) => definition.WithPluralForm(pluralForm),
            locator: static (locations, pluralFormLocation) => locations.WithPluralForm(pluralFormLocation)
        );
    }

    public static IAttributeProperty<TDefinition> PluralFormRegexSubstitution<TDefinition, TLocations>(string name)
        where TDefinition : IOpenRawUnitInstance<TDefinition, TLocations>
        where TLocations : IOpenUnitInstanceLocations<TLocations>
    {
        return new AttributeProperty<TDefinition, TLocations, string>
        (
            name: name,
            setter: static (definition, pluralFormRegexSubstitution) => definition.WithPluralFormRegexSubstitution(pluralFormRegexSubstitution),
            locator: static (locations, pluralFormRegexSubtitiutionLocation) => locations.WithPluralFormRegexSubstitution(pluralFormRegexSubtitiutionLocation)
        );
    }

    public static IAttributeProperty<TDefinition> OriginalUnitInstance<TDefinition, TLocations>(string name)
        where TDefinition : IOpenRawModifiedUnitInstance<TDefinition, TLocations>
        where TLocations : IOpenModifiedUnitInstanceLocations<TLocations>
    {
        return new AttributeProperty<TDefinition, TLocations, string>
        (
            name: name,
            setter: static (definition, originalUnitInstance) => definition.WithOriginalUnitInstance(originalUnitInstance),
            locator: static (locations, originalUnitInstanceLocation) => locations.WithOriginalUnitInstance(originalUnitInstanceLocation)
        );
    }
}

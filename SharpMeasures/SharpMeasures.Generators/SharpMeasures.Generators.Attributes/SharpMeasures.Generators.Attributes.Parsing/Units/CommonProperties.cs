namespace SharpMeasures.Generators.Attributes.Parsing.Units;

internal static class CommonProperties
{
    public static IAttributeProperty<TDefinition> Name<TDefinition, TParsingData, TLocations>(string name)
        where TDefinition : AUnitDefinition<TParsingData, TLocations>
        where TParsingData : AUnitParsingData<TLocations>
        where TLocations : AUnitLocations
    {
        return new AttributeProperty<TDefinition, TParsingData, TLocations, string>
        (
            name: name,
            setter: static (definition, name) => definition with { Name = name },
            locator: static (locations, nameLocation) => locations with { Name = nameLocation }
        );
    }

    public static IAttributeProperty<TDefinition> Plural<TDefinition, TParsingData, TLocations>(string name)
        where TDefinition : AUnitDefinition<TParsingData, TLocations>
        where TParsingData : AUnitParsingData<TLocations>
        where TLocations : AUnitLocations
    {
        return new AttributeProperty<TDefinition, TParsingData, TLocations, string>
        (
            name: name,
            setter: static (definition, plural) => definition with { Plural = plural },
            locator: static (locations, pluralLocation) => locations with { Plural = pluralLocation }
        );
    }

    public static IAttributeProperty<TDefinition> DependantOn<TDefinition, TParsingData, TLocations>(string name)
        where TDefinition : ADependantUnitDefinition<TParsingData, TLocations>
        where TParsingData : ADependantUnitParsingData<TLocations>
        where TLocations : ADependantUnitLocations
    {
        return new AttributeProperty<TDefinition, TParsingData, TLocations, string>
        (
            name: name,
            setter: static (definition, dependantOn) => definition with { DependantOn = dependantOn },
            locator: static (locations, dependantOnLocation) => locations with { DependantOn = dependantOnLocation }
        );
    }
}

namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class CommonProperties
{
    public static IAttributeProperty<TDefinition> Name<TDefinition, TLocations>(string name)
        where TDefinition : IOpenRawUnitDefinition<TDefinition, TLocations>
        where TLocations : IOpenUnitLocations<TLocations>
    {
        return new AttributeProperty<TDefinition, TLocations, string>
        (
            name: name,
            setter: static (definition, name) => definition.WithName(name),
            locator: static (locations, nameLocation) => locations.WithName(nameLocation)
        );
    }

    public static IAttributeProperty<TDefinition> Plural<TDefinition, TLocations>(string name)
        where TDefinition : IOpenRawUnitDefinition<TDefinition, TLocations>
        where TLocations : IOpenUnitLocations<TLocations>
    {
        return new AttributeProperty<TDefinition, TLocations, string>
        (
            name: name,
            setter: static (definition, plural) => definition.WithPlural(plural),
            locator: static (locations, pluralLocation) => locations.WithPlural(pluralLocation)
        );
    }

    public static IAttributeProperty<TDefinition> DependantOn<TDefinition, TLocations>(string name)
        where TDefinition : IOpenRawDependantUnitDefinition<TDefinition, TLocations>
        where TLocations : IOpenDependantUnitLocations<TLocations>
    {
        return new AttributeProperty<TDefinition, TLocations, string>
        (
            name: name,
            setter: static (definition, dependantOn) => definition.WithDependantOn(dependantOn),
            locator: static (locations, dependantOnLocation) => locations.WithDependantOn(dependantOnLocation)
        );
    }
}

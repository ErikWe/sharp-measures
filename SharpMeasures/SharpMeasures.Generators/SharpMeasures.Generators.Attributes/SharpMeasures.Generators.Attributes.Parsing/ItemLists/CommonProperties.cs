namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

internal static class CommonProperties
{
    public static IAttributeProperty<TDefinition> ItemNames<TDefinition, TParsingData, TLocations>(string name)
        where TDefinition : AItemListDefinition<TParsingData, TLocations>
        where TParsingData : AItemListParsingData<TLocations>
        where TLocations : AItemListLocations
    {
        return new AttributeProperty<TDefinition, TParsingData, TLocations, string[]>
        (
            name: name,
            setter: static (definition, itemNames) => definition with { ItemNames = itemNames },
            locator: static (locations, collectionLocation, elementLocations) => locations with
            {
                ItemNamesCollection = collectionLocation,
                ItemNamesElements = elementLocations
            }
        );
    }
}

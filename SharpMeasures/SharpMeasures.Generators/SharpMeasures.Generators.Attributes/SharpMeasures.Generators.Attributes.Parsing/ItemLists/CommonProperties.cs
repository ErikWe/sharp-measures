namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using SharpMeasures.Equatables;

using System;

public static class CommonProperties
{
    public static IAttributeProperty<TDefinition> Items<TItem, TDefinition, TLocations>(string name)
        where TDefinition : ARawItemListDefinition<TItem, TLocations>
        where TLocations : AItemListLocations
    {
        return new AttributeProperty<TDefinition, TLocations, TItem[]>
        (
            name: name,
            setter: static (definition, items) => definition with { Items = items.AsReadOnlyEquatable() },
            locator: static (locations, collectionLocation, elementLocations) => locations with
            {
                ItemsCollection = collectionLocation,
                ItemsElements = elementLocations
            }
        );
    }

    public static IAttributeProperty<TDefinition> Items<TRawItem, TItem, TDefinition, TLocations>(string name, Func<TRawItem, TItem> transform)
        where TDefinition : ARawItemListDefinition<TItem, TLocations>
        where TLocations : AItemListLocations
    {
        return new AttributeProperty<TDefinition, TLocations, TRawItem[]>
        (
            name: name,
            setter: (definition, items) =>
            {
                TItem[] transformedItems = new TItem[items.Length];

                for (int i = 0; i < transformedItems.Length; i++)
                {
                    transformedItems[i] = transform(items[i]);
                }

                return definition with { Items = transformedItems.AsReadOnlyEquatable() };
            },
            locator: static (locations, collectionLocation, elementLocations) => locations with
            {
                ItemsCollection = collectionLocation,
                ItemsElements = elementLocations
            }
        );
    }
}

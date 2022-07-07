namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using SharpMeasures.Equatables;

using System;
using System.Linq;

public static class CommonProperties
{
    public static IAttributeProperty<TDefinition> Items<TItem, TDefinition, TLocations>(string name)
        where TDefinition : IOpenItemListDefinition<TItem, TDefinition, TLocations>
        where TLocations : IOpenItemListLocations<TLocations>
    {
        return new AttributeProperty<TDefinition, TLocations, TItem[]>
        (
            name: name,
            setter: static (definition, items) => definition.WithItems(items.AsReadOnlyEquatable()),
            locator: static (locations, collectionLocation, elementLocations) => locations.WithItems(collectionLocation, elementLocations)
        );
    }

    public static IAttributeProperty<TDefinition> Items<TRawItem, TItem, TDefinition, TLocations>(string name, Func<TRawItem, TItem> transform)
        where TDefinition : IOpenItemListDefinition<TItem, TDefinition, TLocations>
        where TLocations : IOpenItemListLocations<TLocations>
    {
        return new AttributeProperty<TDefinition, TLocations, TRawItem[]>
        (
            name: name,
            setter: (definition, items) => definition.WithItems(items.Select((x) => transform(x)).ToList()),
            locator: static (locations, collectionLocation, elementLocations) => locations.WithItems(collectionLocation, elementLocations)
        );
    }
}

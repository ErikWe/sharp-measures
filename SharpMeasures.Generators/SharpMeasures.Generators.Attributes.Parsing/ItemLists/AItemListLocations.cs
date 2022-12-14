namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public abstract record class AItemListLocations<TLocations> : AAttributeLocations<TLocations>, IOpenItemListLocations<TLocations> where TLocations : AItemListLocations<TLocations>
{
    protected MinimalLocation? ItemsCollection { get; private init; }
    protected IReadOnlyList<MinimalLocation> ItemsElements
    {
        get => itemsElementsField;
        private init => itemsElementsField = value.AsReadOnlyEquatable();
    }

    protected bool ExplicitlySetItems => ItemsCollection is not null;

    MinimalLocation? IItemListLocations.ItemsCollection => ItemsCollection;
    IReadOnlyList<MinimalLocation> IItemListLocations.ItemsElements => ItemsElements;
    bool IItemListLocations.ExplicitlySetItems => ExplicitlySetItems;

    private readonly IReadOnlyList<MinimalLocation> itemsElementsField = ReadOnlyEquatableList<MinimalLocation>.Empty;

    protected TLocations WithItems(MinimalLocation collection, IReadOnlyList<MinimalLocation> elements) => Locations with
    {
        ItemsCollection = collection,
        ItemsElements = elements
    };

    TLocations IOpenItemListLocations<TLocations>.WithItems(MinimalLocation collectionLocation, IReadOnlyList<MinimalLocation> elementLocations) => WithItems(collectionLocation, elementLocations);
}

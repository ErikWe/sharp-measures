namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public abstract record class AItemListLocations : AAttributeLocations, IItemListLocations
{
    public MinimalLocation? ItemsCollection { get; init; }
    public ReadOnlyEquatableList<MinimalLocation> ItemsElements { get; init; } = ReadOnlyEquatableList<MinimalLocation>.Empty;
    IReadOnlyList<MinimalLocation> IItemListLocations.ItemsElements => ItemsElements;

    public bool ExplicitlySetItems => ItemsCollection is not null;
}

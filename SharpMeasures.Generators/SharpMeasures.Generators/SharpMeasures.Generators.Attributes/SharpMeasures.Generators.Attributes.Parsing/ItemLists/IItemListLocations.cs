namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public interface IItemListLocations : IAttributeLocations
{
    public abstract MinimalLocation? ItemsCollection { get; }
    public abstract IReadOnlyList<MinimalLocation> ItemsElements { get; }

    public abstract bool ExplicitlySetItems { get; }
}

public interface IOpenItemListLocations<out TLocations> : IItemListLocations, IOpenAttributeLocations<TLocations>
    where TLocations : IOpenItemListLocations<TLocations>
{
    public abstract TLocations WithItems(MinimalLocation collection, IReadOnlyList<MinimalLocation> elements);
}

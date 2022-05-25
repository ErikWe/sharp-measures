namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public interface IItemListLocations : IAttributeLocations
{
    public abstract MinimalLocation ItemNamesCollection { get; }
    public abstract IReadOnlyList<MinimalLocation> ItemNamesElements { get; }
}

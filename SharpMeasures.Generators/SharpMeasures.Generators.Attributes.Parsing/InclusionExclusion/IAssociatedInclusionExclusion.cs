namespace SharpMeasures.Generators.Attributes.Parsing.InclusionExclusion;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public interface IAssociatedInclusionExclusion<out TItem, out TIncludeDefinition, out TExcludeDefinition, out TLocations> :
    IInclusionExclusion<TItem, TIncludeDefinition, TExcludeDefinition, TLocations>
    where TIncludeDefinition : IItemListDefinition<TItem, TLocations>
    where TExcludeDefinition : IItemListDefinition<TItem, TLocations>
    where TLocations : IItemListLocations
{
    public abstract IInclusionExclusion<TItem, TIncludeDefinition, TExcludeDefinition, TLocations> Associated { get; }
}

namespace SharpMeasures.Generators.Attributes.Parsing.InclusionExclusion;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public interface IInclusionExclusion<out TItem, out TIncludeDefinition, out TExcludeDefinition, out TLocations>
    where TIncludeDefinition : IItemListDefinition<TItem, TLocations>
    where TExcludeDefinition : IItemListDefinition<TItem, TLocations>
    where TLocations : IItemListLocations
{
    public abstract NamedType Type { get; }

    public abstract IEnumerable<TIncludeDefinition> Inclusions { get; }
    public abstract IEnumerable<TExcludeDefinition> Exclusions { get; }
}

namespace SharpMeasures.Generators.Quantities.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public interface IUnitListInclusionExclusion<TIncludeDefinition, TExcludeDefinition>
    where TIncludeDefinition : IItemListDefinition<string>
    where TExcludeDefinition : IItemListDefinition<string>
{
    public abstract IEnumerable<TIncludeDefinition> IncludeUnits { get; }
    public abstract IEnumerable<TExcludeDefinition> ExcludeUnits { get; }
}

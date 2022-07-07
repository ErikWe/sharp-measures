namespace SharpMeasures.Generators.Attributes.Parsing.InclusionExclusion;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;
using System.Linq;

public class InclusionPopulation<TItem> : ReadOnlyEquatableDictionary<NamedType, ResolvedInclusion<TItem>>
{
    public InclusionPopulation(IReadOnlyDictionary<NamedType, ResolvedInclusion<TItem>> items) : base(items) { }
}

public static class InclusionPopulation
{
    public static InclusionPopulation<TItem> Build<TItem, TIncludeDefinition, TExcludeDefinition, TLocations>
        (IEnumerable<IInclusionExclusion<TItem, TIncludeDefinition, TExcludeDefinition, TLocations>> inclusionExclusions)
        where TIncludeDefinition : IItemListDefinition<TItem, TLocations>
        where TExcludeDefinition : IItemListDefinition<TItem, TLocations>
        where TLocations : IItemListLocations
    {
        return new Builder<TItem, TIncludeDefinition, TExcludeDefinition, TLocations>().ResolveInclusions(inclusionExclusions);
    }

    private class Builder<TItem, TIncludeDefinition, TExcludeDefinition, TLocations>
        where TIncludeDefinition : IItemListDefinition<TItem, TLocations>
        where TExcludeDefinition : IItemListDefinition<TItem, TLocations>
        where TLocations : IItemListLocations
    {
        private Dictionary<NamedType, ResolvedInclusion<TItem>> ResolvedInclusions { get; } = new();

        public InclusionPopulation<TItem> ResolveInclusions(IEnumerable<IInclusionExclusion<TItem, TIncludeDefinition, TExcludeDefinition, TLocations>> inclusionExclusions)
        {
            foreach (var inclusionExclusion in inclusionExclusions)
            {
                ResolveInclusionExclusion(inclusionExclusion);
            }

            return new(ResolvedInclusions);
        }

        private ResolvedInclusion<TItem> ResolveInclusionExclusion(IInclusionExclusion<TItem, TIncludeDefinition, TExcludeDefinition, TLocations> inclusionExclusion)
        {
            if (inclusionExclusion is IAssociatedInclusionExclusion<TItem, TIncludeDefinition, TExcludeDefinition, TLocations> associated)
            {
                if (ResolvedInclusions.TryGetValue(associated.Associated.Type, out var associatedUnitInclusion) is false)
                {
                    associatedUnitInclusion = ResolveInclusionExclusion(associated);
                }

                var unitInclusion = DeriveInclusion(inclusionExclusion, associatedUnitInclusion);
                ResolvedInclusions.Add(inclusionExclusion.Type, unitInclusion);
                return unitInclusion;
            }
            else
            {
                var unitInclusion = CreateInclusion(inclusionExclusion);
                ResolvedInclusions.Add(inclusionExclusion.Type, unitInclusion);
                return unitInclusion;
            }
        }

        private static ResolvedInclusion<TItem> CreateInclusion(IInclusionExclusion<TItem, TIncludeDefinition, TExcludeDefinition, TLocations> inclusionExclusion)
        {
            HashSet<TItem> items = new();
            List<int> redundantIndices = new();

            IEnumerable<TItem> listedItems = inclusionExclusion.Inclusions.Any()
                ? inclusionExclusion.Inclusions.SelectMany(static (x) => x.Items)
                : inclusionExclusion.Exclusions.SelectMany(static (x) => x.Items);

            AddItems(items, redundantIndices, listedItems);
            InclusionMode mode = inclusionExclusion.Inclusions.Any() ? InclusionMode.Include : InclusionMode.Exclude;

            return new(mode, items, redundantIndices);
        }

        private static ResolvedInclusion<TItem> DeriveInclusion(IInclusionExclusion<TItem, TIncludeDefinition, TExcludeDefinition, TLocations> inclusionExclusion,
            ResolvedInclusion<TItem> parent)
        {
            HashSet<TItem> items = new(parent.Items);
            List<int> redundantIndices = new();

            if (parent.Mode is InclusionMode.Include)
            {
                if (inclusionExclusion.Inclusions.Any())
                {
                    AddItems(items, redundantIndices, inclusionExclusion.Inclusions.SelectMany(static (x) => x.Items));
                }
                else
                {
                    RemoveItems(items, redundantIndices, inclusionExclusion.Exclusions.SelectMany(static (x) => x.Items));
                }
            }
            else
            {
                if (inclusionExclusion.Inclusions.Any())
                {
                    RemoveItems(items, redundantIndices, inclusionExclusion.Inclusions.SelectMany(static (x) => x.Items));
                }
                else
                {
                    AddItems(items, redundantIndices, inclusionExclusion.Exclusions.SelectMany(static (x) => x.Items));
                }
            }

            return new(parent.Mode, items, redundantIndices);
        }

        private static void AddItems(HashSet<TItem> listedItems, IList<int> redundantIndices, IEnumerable<TItem> items)
        {
            int index = 0;
            foreach (TItem item in items)
            {
                if (listedItems.Add(item) is false)
                {
                    redundantIndices.Add(index);
                }

                index += 1;
            }
        }

        private static void RemoveItems(HashSet<TItem> listedItems, IList<int> redundantIndices, IEnumerable<TItem> items)
        {
            int index = 0;
            foreach (TItem item in items)
            {
                if (listedItems.Remove(item))
                {
                    redundantIndices.Add(index);
                }

                index += 1;
            }
        }
    }
}

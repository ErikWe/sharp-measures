namespace SharpMeasures.Generators.Attributes.Parsing.InclusionExclusion;

using SharpMeasures.Equatables;

using System.Collections.Generic;
using System.Linq;

public class InclusionPopulation<TItem> : ReadOnlyEquatableDictionary<NamedType, ResolvedInclusion<TItem>>
{
    public InclusionPopulation(IReadOnlyDictionary<NamedType, ResolvedInclusion<TItem>> items) : base(items) { }
}

public static class InclusionPopulation
{
    public static InclusionPopulation<TItem> Build<TItem>(IEnumerable<IInclusionExclusion<TItem>> inclusionExclusions)
    {
        return new Builder<TItem>().ResolveInclusions(inclusionExclusions);
    }

    private class Builder<TItem>
    {
        private Dictionary<NamedType, ResolvedInclusion<TItem>> ResolvedInclusions { get; } = new();

        public InclusionPopulation<TItem> ResolveInclusions(IEnumerable<IInclusionExclusion<TItem>> inclusionExclusions)
        {
            foreach (var inclusionExclusion in inclusionExclusions)
            {
                ResolveInclusionExclusion(inclusionExclusion);
            }

            return new(ResolvedInclusions);
        }

        private ResolvedInclusion<TItem> ResolveInclusionExclusion(IInclusionExclusion<TItem> inclusionExclusion)
        {
            if (inclusionExclusion is IAssociatedInclusionExclusion<TItem> associated)
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

        private static ResolvedInclusion<TItem> CreateInclusion(IInclusionExclusion<TItem> inclusionExclusion)
        {
            HashSet<TItem> items = new();
            List<int> redundantIndices = new();

            AddItems(items, redundantIndices, inclusionExclusion.Inclusions.Any() ? inclusionExclusion.Inclusions : inclusionExclusion.Exclusions);
            InclusionMode mode = inclusionExclusion.Inclusions.Any() ? InclusionMode.Include : InclusionMode.Exclude;

            return new(mode, items, redundantIndices);
        }

        private static ResolvedInclusion<TItem> DeriveInclusion(IInclusionExclusion<TItem> inclusionExclusion, ResolvedInclusion<TItem> parent)
        {
            HashSet<TItem> items = new(parent.Items);
            List<int> redundantIndices = new();

            if (parent.Mode is InclusionMode.Include)
            {
                if (inclusionExclusion.Inclusions.Any())
                {
                    AddItems(items, redundantIndices, inclusionExclusion.Inclusions);
                }
                else
                {
                    RemoveItems(items, redundantIndices, inclusionExclusion.Exclusions);
                }
            }
            else
            {
                if (inclusionExclusion.Inclusions.Any())
                {
                    RemoveItems(items, redundantIndices, inclusionExclusion.Inclusions);
                }
                else
                {
                    AddItems(items, redundantIndices, inclusionExclusion.Exclusions);
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

namespace SharpMeasures.Generators.Attributes.Parsing.InclusionExclusion;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class ResolvedInclusion<TItem>
{
    public InclusionMode Mode { get; }

    public ReadOnlyEquatableHashSet<TItem> Items { get; }

    public ReadOnlyEquatableCollection<int> RedundantIndices { get; }

    public ResolvedInclusion(InclusionMode mode, HashSet<TItem> items, IReadOnlyCollection<int> redundantIndices)
    {
        Mode = mode;
        Items = items.AsReadOnlyEquatable();

        RedundantIndices = redundantIndices.AsReadOnlyEquatable();
    }
}

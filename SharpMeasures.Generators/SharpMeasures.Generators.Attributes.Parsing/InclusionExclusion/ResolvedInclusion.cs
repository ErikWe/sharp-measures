namespace SharpMeasures.Generators.Attributes.Parsing.InclusionExclusion;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class ResolvedInclusion<TItem>
{
    public InclusionMode Mode { get; }

    public IReadOnlyHashSet<TItem> Items => items;
    public IReadOnlyCollection<int> RedundantIndices => redundantIndices;

    private ReadOnlyEquatableHashSet<TItem> items { get; }
    private ReadOnlyEquatableCollection<int> redundantIndices { get; }

    public ResolvedInclusion(InclusionMode mode, HashSet<TItem> items, IReadOnlyCollection<int> redundantIndices)
    {
        Mode = mode;

        this.items = items.AsReadOnlyEquatable();
        this.redundantIndices = redundantIndices.AsReadOnlyEquatable();
    }
}

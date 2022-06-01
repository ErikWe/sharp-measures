namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class RawExcludeBasesDefinition : ARawItemListDefinition<string?, ExcludeBasesLocations>
{
    internal static RawExcludeBasesDefinition Empty => new();

    public IReadOnlyList<string?> ExcludedBases => Items;

    private RawExcludeBasesDefinition() : base(ExcludeBasesLocations.Empty) { }
}
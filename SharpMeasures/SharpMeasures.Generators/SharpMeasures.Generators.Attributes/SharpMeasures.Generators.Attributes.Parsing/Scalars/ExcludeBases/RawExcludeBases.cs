namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class RawExcludeBases : ARawItemListDefinition<string?, ExcludeBasesLocations>
{
    internal static RawExcludeBases Empty => new();

    public IReadOnlyList<string?> ExcludedBases => Items;

    private RawExcludeBases() : base(ExcludeBasesLocations.Empty) { }
}

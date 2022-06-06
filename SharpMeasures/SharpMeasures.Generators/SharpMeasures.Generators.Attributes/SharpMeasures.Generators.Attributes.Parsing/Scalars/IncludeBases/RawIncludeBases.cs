namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class RawIncludeBases : ARawItemListDefinition<string?, IncludeBasesLocations>
{
    internal static RawIncludeBases Empty => new();

    public IReadOnlyList<string?> IncludedBases => Items;

    private RawIncludeBases() : base(IncludeBasesLocations.Empty) { }
}

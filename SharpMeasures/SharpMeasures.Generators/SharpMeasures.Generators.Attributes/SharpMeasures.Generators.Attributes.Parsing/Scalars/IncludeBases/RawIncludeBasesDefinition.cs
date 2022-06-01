namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class RawIncludeBasesDefinition : ARawItemListDefinition<string?, IncludeBasesLocations>
{
    internal static RawIncludeBasesDefinition Empty => new();

    public IReadOnlyList<string?> IncludedBases => Items;

    private RawIncludeBasesDefinition() : base(IncludeBasesLocations.Empty) { }
}
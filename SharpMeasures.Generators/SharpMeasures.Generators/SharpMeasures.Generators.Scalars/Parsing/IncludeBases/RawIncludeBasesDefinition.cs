namespace SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

internal record class RawIncludeBasesDefinition : ARawItemListDefinition<string?, RawIncludeBasesDefinition, IncludeBasesLocations>
{
    public static RawIncludeBasesDefinition Empty => new();

    public IReadOnlyList<string?> IncludedBases => Items;

    protected override RawIncludeBasesDefinition Definition => this;

    private RawIncludeBasesDefinition() : base(IncludeBasesLocations.Empty) { }
}

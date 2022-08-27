namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

internal record class RawExcludeBasesDefinition : ARawItemListDefinition<string?, RawExcludeBasesDefinition, ExcludeBasesLocations>
{
    public static RawExcludeBasesDefinition Empty => new();

    public IReadOnlyList<string?> ExcludedBases => Items;

    protected override RawExcludeBasesDefinition Definition => this;

    private RawExcludeBasesDefinition() : base(ExcludeBasesLocations.Empty) { }
}

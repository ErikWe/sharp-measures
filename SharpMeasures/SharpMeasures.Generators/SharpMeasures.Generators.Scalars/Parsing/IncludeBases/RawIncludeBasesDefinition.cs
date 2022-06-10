namespace SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

internal record class RawIncludeBasesDefinition : ARawItemListDefinition<string?, IncludeBasesLocations>
{
    public static RawIncludeBasesDefinition Empty => new();

    public ReadOnlyEquatableList<string?> IncludedBases => Items;

    private RawIncludeBasesDefinition() : base(IncludeBasesLocations.Empty) { }
}

namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

internal record class RawExcludeBasesDefinition : ARawItemListDefinition<string?, ExcludeBasesLocations>
{
    public static RawExcludeBasesDefinition Empty => new();

    public ReadOnlyEquatableList<string?> ExcludedBases => Items;

    private RawExcludeBasesDefinition() : base(ExcludeBasesLocations.Empty) { }
}

namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

internal record class ExcludeBasesLocations : AItemListLocations<ExcludeBasesLocations>
{
    internal static ExcludeBasesLocations Empty { get; } = new();

    public MinimalLocation? ExcludedBasesCollection => ItemsCollection;
    public IReadOnlyList<MinimalLocation> ExcludeBasesElements => ItemsElements;

    public bool ExplicitlySetExcludedBases => ExplicitlySetItems;

    protected override ExcludeBasesLocations Locations => this;

    private ExcludeBasesLocations() { }
}

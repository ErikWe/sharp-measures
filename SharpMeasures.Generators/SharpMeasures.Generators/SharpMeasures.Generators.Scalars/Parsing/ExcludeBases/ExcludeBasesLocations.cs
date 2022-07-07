namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

internal record class ExcludeBasesLocations : AItemListLocations<ExcludeBasesLocations>
{
    public static ExcludeBasesLocations Empty { get; } = new();

    public MinimalLocation? ExcludedBasesCollection => ItemsCollection;
    public IReadOnlyList<MinimalLocation> ExcludedBasesElements => ItemsElements;

    public bool ExplicitlySetExcludedBases => ExcludedBasesCollection is not null;

    protected override ExcludeBasesLocations Locations => this;

    private ExcludeBasesLocations() { }
}

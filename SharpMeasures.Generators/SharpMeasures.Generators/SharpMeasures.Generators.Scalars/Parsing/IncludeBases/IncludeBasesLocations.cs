namespace SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

internal record class IncludeBasesLocations : AItemListLocations<IncludeBasesLocations>
{
    public static IncludeBasesLocations Empty { get; } = new();

    public MinimalLocation? IncludedBasesCollection => ItemsCollection;
    public IReadOnlyList<MinimalLocation> IncludedBasesElements => ItemsElements;

    public bool ExplicitlySetIncludedBases => IncludedBasesCollection is not null;

    protected override IncludeBasesLocations Locations => this;

    private IncludeBasesLocations() { }
}

namespace SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

internal record class IncludeBasesLocations : AItemListLocations
{
    public static IncludeBasesLocations Empty { get; } = new();

    public MinimalLocation? IncludedBasesCollection => ItemsCollection;
    public ReadOnlyEquatableList<MinimalLocation> IncludedBasesElements => ItemsElements;

    public bool ExplicitlySetIncludedBases => IncludedBasesCollection is not null;

    private IncludeBasesLocations() { }
}

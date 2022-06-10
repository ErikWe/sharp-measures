namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

internal record class ExcludeBasesLocations : AItemListLocations
{
    public static ExcludeBasesLocations Empty { get; } = new();

    public MinimalLocation? ExcludedBasesCollection => ItemsCollection;
    public ReadOnlyEquatableList<MinimalLocation> ExcludedBasesElements => ItemsElements;

    public bool ExplicitlySetExcludedBases => ExcludedBasesCollection is not null;

    private ExcludeBasesLocations() { }
}

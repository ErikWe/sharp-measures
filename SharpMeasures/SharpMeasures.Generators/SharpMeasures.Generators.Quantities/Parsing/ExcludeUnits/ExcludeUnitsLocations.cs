namespace SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public record class ExcludeUnitsLocations : AItemListLocations
{
    internal static ExcludeUnitsLocations Empty { get; } = new();

    public MinimalLocation? ExcludedUnitsCollection => ItemsCollection;
    public ReadOnlyEquatableList<MinimalLocation> ExcludedUnitsElements => ItemsElements;

    public bool ExplicitlySetExcludedUnits => ExcludedUnitsCollection is not null;

    private ExcludeUnitsLocations() { }
}

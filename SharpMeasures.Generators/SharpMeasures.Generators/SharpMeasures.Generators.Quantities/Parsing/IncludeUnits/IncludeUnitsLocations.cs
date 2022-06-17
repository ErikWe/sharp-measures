namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public record class IncludeUnitsLocations : AItemListLocations
{
    internal static IncludeUnitsLocations Empty { get; } = new();

    public MinimalLocation? IncludedUnitsCollection => ItemsCollection;
    public ReadOnlyEquatableList<MinimalLocation> IncludedUnitsElements => ItemsElements;

    public bool ExplicitlySetIncludedUnits => IncludedUnitsCollection is not null;

    private IncludeUnitsLocations() { }
}

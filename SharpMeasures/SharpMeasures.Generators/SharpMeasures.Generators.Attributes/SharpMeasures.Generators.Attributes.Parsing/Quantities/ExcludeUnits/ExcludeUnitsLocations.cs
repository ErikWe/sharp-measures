namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class ExcludeUnitsLocations : AItemListLocations
{
    internal static ExcludeUnitsLocations Empty { get; } = new();

    public MinimalLocation? ExcludedUnitsCollection => ItemsCollection;
    public IReadOnlyList<MinimalLocation> ExcludedUnitsElements => ItemsElements;

    public bool ExplicitlySetExcludedUnits => ExcludedUnitsCollection is not null;

    private ExcludeUnitsLocations() { }
}
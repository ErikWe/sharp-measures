namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class IncludeUnitsLocations : AItemListLocations
{
    internal static IncludeUnitsLocations Empty { get; } = new();

    public MinimalLocation? IncludedUnitsCollection => ItemsCollection;
    public IReadOnlyList<MinimalLocation> IncludedUnitsElements => ItemsElements;

    public bool ExplicitlySetIncludedUnits => IncludedUnitsCollection is not null;

    private IncludeUnitsLocations() { }
}
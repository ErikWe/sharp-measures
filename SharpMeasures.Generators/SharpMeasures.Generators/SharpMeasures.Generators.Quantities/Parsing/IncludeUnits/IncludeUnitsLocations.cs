namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class IncludeUnitsLocations : AItemListLocations<IncludeUnitsLocations>
{
    internal static IncludeUnitsLocations Empty { get; } = new();

    public MinimalLocation? IncludedUnitsCollection => ItemsCollection;
    public IReadOnlyList<MinimalLocation> IncludedUnitsElements => ItemsElements;

    public bool ExplicitlySetIncludedUnits => ExplicitlySetItems;

    protected override IncludeUnitsLocations Locations => this;

    private IncludeUnitsLocations() { }
}

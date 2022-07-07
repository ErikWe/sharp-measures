namespace SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class ExcludeUnitsLocations : AItemListLocations<ExcludeUnitsLocations>
{
    internal static ExcludeUnitsLocations Empty { get; } = new();

    public MinimalLocation? ExcludedUnitsCollection => ItemsCollection;
    public IReadOnlyList<MinimalLocation> ExcludedUnitsElements => ItemsElements;

    public bool ExplicitlySetExcludedUnits => ExplicitlySetItems;

    protected override ExcludeUnitsLocations Locations => this;

    private ExcludeUnitsLocations() { }
}

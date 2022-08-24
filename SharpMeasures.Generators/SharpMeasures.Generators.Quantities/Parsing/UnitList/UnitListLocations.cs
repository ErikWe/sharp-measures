namespace SharpMeasures.Generators.Quantities.Parsing.UnitList;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class UnitListLocations : AItemListLocations<UnitListLocations>
{
    internal static UnitListLocations Empty { get; } = new();

    public MinimalLocation? UnitsCollection => ItemsCollection;
    public IReadOnlyList<MinimalLocation> UnitsElements => ItemsElements;

    public bool ExplicitlySetUnits => ExplicitlySetItems;

    protected override UnitListLocations Locations => this;

    private UnitListLocations() { }
}

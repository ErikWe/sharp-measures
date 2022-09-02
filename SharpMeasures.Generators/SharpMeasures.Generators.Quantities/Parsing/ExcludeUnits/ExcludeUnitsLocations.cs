namespace SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class ExcludeUnitsLocations : AItemListLocations<ExcludeUnitsLocations>, IUnitInstanceListLocations
{
    internal static ExcludeUnitsLocations Empty { get; } = new();

    public MinimalLocation? UnitInstancesCollection => ItemsCollection;
    public IReadOnlyList<MinimalLocation> UnitInstancesElements => ItemsElements;

    public bool ExplicitlySetUnitInstances => ExplicitlySetItems;

    protected override ExcludeUnitsLocations Locations => this;

    private ExcludeUnitsLocations() { }
}

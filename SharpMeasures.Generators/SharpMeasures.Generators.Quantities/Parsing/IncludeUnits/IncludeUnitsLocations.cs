namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class IncludeUnitsLocations : AItemListLocations<IncludeUnitsLocations>, IUnitInstanceInclusionListLocations
{
    internal static IncludeUnitsLocations Empty { get; } = new();

    public MinimalLocation? UnitInstancesCollection => ItemsCollection;
    public IReadOnlyList<MinimalLocation> UnitInstancesElements => ItemsElements;

    public MinimalLocation? StackingMode { get; init; }

    public bool ExplicitlySetUnitInstances => ExplicitlySetItems;
    public bool ExplicitlySetStackingMode => StackingMode is not null;

    protected override IncludeUnitsLocations Locations => this;

    private IncludeUnitsLocations() { }
}

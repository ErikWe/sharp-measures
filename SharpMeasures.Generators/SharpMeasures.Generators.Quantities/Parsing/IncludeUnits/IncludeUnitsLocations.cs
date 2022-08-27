namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class IncludeUnitsLocations : AItemListLocations<IncludeUnitsLocations>
{
    internal static IncludeUnitsLocations Empty { get; } = new();

    public MinimalLocation? IncludedUnitsCollection => ItemsCollection;
    public IReadOnlyList<MinimalLocation> IncludeUnitsElements => ItemsElements;

    public MinimalLocation? StackingMode { get; init; }

    public bool ExplicitlySetIncludedUnits => ExplicitlySetItems;
    public bool ExplicitlySetStackingMode => StackingMode is not null;

    protected override IncludeUnitsLocations Locations => this;

    private IncludeUnitsLocations() { }
}

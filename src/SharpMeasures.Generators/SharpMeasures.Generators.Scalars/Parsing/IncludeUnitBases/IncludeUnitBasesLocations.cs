namespace SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public sealed record class IncludeUnitBasesLocations : AItemListLocations<IncludeUnitBasesLocations>, IUnitInstanceInclusionListLocations
{
    internal static IncludeUnitBasesLocations Empty { get; } = new();

    public MinimalLocation? UnitInstancesCollection => ItemsCollection;
    public IReadOnlyList<MinimalLocation> UnitInstancesElements => ItemsElements;

    public MinimalLocation? StackingMode { get; init; }

    public bool ExplicitlySetUnitInstances => ExplicitlySetItems;
    public bool ExplicitlySetStackingMode => StackingMode is not null;

    protected override IncludeUnitBasesLocations Locations => this;

    private IncludeUnitBasesLocations() { }
}

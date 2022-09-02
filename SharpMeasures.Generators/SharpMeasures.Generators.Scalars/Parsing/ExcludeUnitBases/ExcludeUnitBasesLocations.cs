namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal record class ExcludeUnitBasesLocations : AItemListLocations<ExcludeUnitBasesLocations>, IUnitInstanceListLocations
{
    internal static ExcludeUnitBasesLocations Empty { get; } = new();

    public MinimalLocation? UnitInstancesCollection => ItemsCollection;
    public IReadOnlyList<MinimalLocation> UnitInstancesElements => ItemsElements;

    public bool ExplicitlySetUnitInstances => ExplicitlySetItems;

    protected override ExcludeUnitBasesLocations Locations => this;

    private ExcludeUnitBasesLocations() { }
}

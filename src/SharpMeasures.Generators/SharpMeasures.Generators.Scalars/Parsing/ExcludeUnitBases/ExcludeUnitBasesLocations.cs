﻿namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal sealed record class ExcludeUnitBasesLocations : AItemListLocations<ExcludeUnitBasesLocations>, IUnitInstanceListLocations
{
    internal static ExcludeUnitBasesLocations Empty { get; } = new();

    public MinimalLocation? UnitInstancesCollection => ItemsCollection;
    public IReadOnlyList<MinimalLocation> UnitInstancesElements => ItemsElements;

    public bool ExplicitlySetUnitInstances => ExplicitlySetItems;

    protected override ExcludeUnitBasesLocations Locations => this;

    private ExcludeUnitBasesLocations() { }
}

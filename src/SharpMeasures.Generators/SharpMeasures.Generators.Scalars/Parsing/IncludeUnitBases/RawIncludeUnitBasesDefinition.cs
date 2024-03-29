﻿namespace SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public sealed record class RawIncludeUnitBasesDefinition : ARawItemListDefinition<string?, RawIncludeUnitBasesDefinition, IncludeUnitBasesLocations>
{
    public static RawIncludeUnitBasesDefinition Empty => new();

    public IReadOnlyList<string?> UnitInstances => Items;

    public InclusionStackingMode StackingMode { get; init; } = InclusionStackingMode.Intersection;

    protected override RawIncludeUnitBasesDefinition Definition => this;

    private RawIncludeUnitBasesDefinition() : base(IncludeUnitBasesLocations.Empty) { }
}

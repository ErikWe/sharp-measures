namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

public record class RawIncludeUnitsDefinition : ARawItemListDefinition<string?, RawIncludeUnitsDefinition, IncludeUnitsLocations>
{
    public static RawIncludeUnitsDefinition Empty => new();

    public IReadOnlyList<string?> IncludedUnits => Items;

    public InclusionStackingMode StackingMode { get; init; } = InclusionStackingMode.Intersection;

    protected override RawIncludeUnitsDefinition Definition => this;

    private RawIncludeUnitsDefinition() : base(IncludeUnitsLocations.Empty) { }
}

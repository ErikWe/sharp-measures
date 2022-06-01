namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class RawIncludeUnitsDefinition : ARawItemListDefinition<string?, IncludeUnitsLocations>
{
    internal static RawIncludeUnitsDefinition Empty => new();

    public IReadOnlyList<string?> IncludedUnits => Items;

    private RawIncludeUnitsDefinition() : base(IncludeUnitsLocations.Empty) { }
}
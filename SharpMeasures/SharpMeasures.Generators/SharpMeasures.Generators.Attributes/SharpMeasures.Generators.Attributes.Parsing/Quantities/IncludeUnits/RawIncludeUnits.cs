namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class RawIncludeUnits : ARawItemListDefinition<string?, IncludeUnitsLocations>
{
    internal static RawIncludeUnits Empty => new();

    public IReadOnlyList<string?> IncludedUnits => Items;

    private RawIncludeUnits() : base(IncludeUnitsLocations.Empty) { }
}

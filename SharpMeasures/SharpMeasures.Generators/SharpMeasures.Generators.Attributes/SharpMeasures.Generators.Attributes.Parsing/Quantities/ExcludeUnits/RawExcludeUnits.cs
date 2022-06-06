namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class RawExcludeUnits : ARawItemListDefinition<string?, ExcludeUnitsLocations>
{
    internal static RawExcludeUnits Empty => new();

    public IReadOnlyList<string?> ExcludedUnits => Items;

    private RawExcludeUnits() : base(ExcludeUnitsLocations.Empty) { }
}

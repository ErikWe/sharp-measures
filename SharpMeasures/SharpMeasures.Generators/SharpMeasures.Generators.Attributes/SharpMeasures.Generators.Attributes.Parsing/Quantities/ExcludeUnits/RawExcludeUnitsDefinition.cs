namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class RawExcludeUnitsDefinition : ARawItemListDefinition<string?, ExcludeUnitsLocations>
{
    internal static RawExcludeUnitsDefinition Empty => new();

    public IReadOnlyList<string?> ExcludedUnits => Items;

    private RawExcludeUnitsDefinition() : base(ExcludeUnitsLocations.Empty) { }
}
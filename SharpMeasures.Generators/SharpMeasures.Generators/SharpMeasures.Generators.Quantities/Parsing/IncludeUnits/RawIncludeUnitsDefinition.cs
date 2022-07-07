namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class RawIncludeUnitsDefinition : ARawItemListDefinition<string?, RawIncludeUnitsDefinition, IncludeUnitsLocations>
{
    internal static RawIncludeUnitsDefinition Empty => new();

    public IReadOnlyList<string?> IncludedUnits => Items;

    protected override RawIncludeUnitsDefinition Definition => this;

    private RawIncludeUnitsDefinition() : base(IncludeUnitsLocations.Empty) { }
}

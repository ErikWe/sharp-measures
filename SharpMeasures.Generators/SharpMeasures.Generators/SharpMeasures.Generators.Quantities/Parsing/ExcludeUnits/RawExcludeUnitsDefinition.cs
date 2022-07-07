namespace SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class RawExcludeUnitsDefinition : ARawItemListDefinition<string?, RawExcludeUnitsDefinition, ExcludeUnitsLocations>
{
    internal static RawExcludeUnitsDefinition Empty => new();

    public IReadOnlyList<string?> ExcludedUnits => Items;

    protected override RawExcludeUnitsDefinition Definition => this;

    private RawExcludeUnitsDefinition() : base(ExcludeUnitsLocations.Empty) { }
}

namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public record class RawIncludeUnitsDefinition : ARawItemListDefinition<string?, IncludeUnitsLocations>
{
    internal static RawIncludeUnitsDefinition Empty => new();

    public ReadOnlyEquatableList<string?> IncludedUnits => Items;

    private RawIncludeUnitsDefinition() : base(IncludeUnitsLocations.Empty) { }
}

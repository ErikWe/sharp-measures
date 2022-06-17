namespace SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public record class RawExcludeUnitsDefinition : ARawItemListDefinition<string?, ExcludeUnitsLocations>
{
    internal static RawExcludeUnitsDefinition Empty => new();

    public ReadOnlyEquatableList<string?> ExcludedUnits => Items;

    private RawExcludeUnitsDefinition() : base(ExcludeUnitsLocations.Empty) { }
}

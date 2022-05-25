namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public record class IncludeUnitsDefinition : AItemListDefinition<IncludeUnitsParsingData, IncludeUnitsLocations>
{
    internal static IncludeUnitsDefinition Empty => new();

    private IncludeUnitsDefinition() : base(IncludeUnitsParsingData.Empty) { }
}
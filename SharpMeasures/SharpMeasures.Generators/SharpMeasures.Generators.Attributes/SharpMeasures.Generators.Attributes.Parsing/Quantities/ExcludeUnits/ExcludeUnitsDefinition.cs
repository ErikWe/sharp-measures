namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public record class ExcludeUnitsDefinition : AItemListDefinition<ExcludeUnitsParsingData, ExcludeUnitsLocations>
{
    internal static ExcludeUnitsDefinition Empty => new();

    private ExcludeUnitsDefinition() : base(ExcludeUnitsParsingData.Empty) { }
}
namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public record class DimensionalEquivalenceDefinition : AItemListDefinition<DimensionalEquivalenceParsingData, DimensionalEquivalenceLocations>
{
    internal static DimensionalEquivalenceDefinition Empty => new();

    private DimensionalEquivalenceDefinition() : base(DimensionalEquivalenceParsingData.Empty) { }
}
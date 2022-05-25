namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public record class DimensionalEquivalenceParsingData : AItemListParsingData<DimensionalEquivalenceLocations>
{
    internal static DimensionalEquivalenceParsingData Empty { get; } = new();

    private DimensionalEquivalenceParsingData() : base(DimensionalEquivalenceLocations.Empty) { }
}
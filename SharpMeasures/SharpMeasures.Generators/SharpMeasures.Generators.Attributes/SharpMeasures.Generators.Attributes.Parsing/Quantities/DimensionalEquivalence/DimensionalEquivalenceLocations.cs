namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public record class DimensionalEquivalenceLocations : AItemListLocations
{
    internal static DimensionalEquivalenceLocations Empty { get; } = new();

    private DimensionalEquivalenceLocations() { }
}
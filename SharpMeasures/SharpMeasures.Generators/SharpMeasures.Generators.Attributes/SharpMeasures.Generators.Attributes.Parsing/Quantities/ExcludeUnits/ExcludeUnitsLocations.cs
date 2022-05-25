namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public record class ExcludeUnitsLocations : AItemListLocations
{
    internal static ExcludeUnitsLocations Empty { get; } = new();

    private ExcludeUnitsLocations() { }
}
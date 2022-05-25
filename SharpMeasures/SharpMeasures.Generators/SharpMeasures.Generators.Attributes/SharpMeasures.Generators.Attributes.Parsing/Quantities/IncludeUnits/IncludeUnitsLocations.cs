namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public record class IncludeUnitsLocations : AItemListLocations
{
    internal static IncludeUnitsLocations Empty { get; } = new();

    private IncludeUnitsLocations() { }
}
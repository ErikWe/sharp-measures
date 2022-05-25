namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public record class IncludeUnitsParsingData : AItemListParsingData<IncludeUnitsLocations>
{
    internal static IncludeUnitsParsingData Empty { get; } = new();

    private IncludeUnitsParsingData() : base(IncludeUnitsLocations.Empty) { }
}
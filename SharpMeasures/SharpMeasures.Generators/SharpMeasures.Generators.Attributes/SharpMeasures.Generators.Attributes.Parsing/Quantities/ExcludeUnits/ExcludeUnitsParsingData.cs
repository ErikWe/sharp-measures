namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public record class ExcludeUnitsParsingData : AItemListParsingData<ExcludeUnitsLocations>
{
    internal static ExcludeUnitsParsingData Empty { get; } = new();

    private ExcludeUnitsParsingData() : base(ExcludeUnitsLocations.Empty) { }
}
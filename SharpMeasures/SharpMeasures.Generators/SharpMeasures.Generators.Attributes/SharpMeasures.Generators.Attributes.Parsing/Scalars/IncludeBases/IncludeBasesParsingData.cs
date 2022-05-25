namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public record class IncludeBasesParsingData : AItemListParsingData<IncludeBasesLocations>
{
    internal static IncludeBasesParsingData Empty { get; } = new();

    private IncludeBasesParsingData() : base(IncludeBasesLocations.Empty) { }
}
namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public record class ExcludeBasesParsingData : AItemListParsingData<ExcludeBasesLocations>
{
    internal static ExcludeBasesParsingData Empty { get; } = new();

    private ExcludeBasesParsingData() : base(ExcludeBasesLocations.Empty) { }
}
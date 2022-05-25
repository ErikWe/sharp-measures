namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public record class IncludeBasesDefinition : AItemListDefinition<IncludeBasesParsingData, IncludeBasesLocations>
{
    internal static IncludeBasesDefinition Empty => new();

    private IncludeBasesDefinition() : base(IncludeBasesParsingData.Empty) { }
}
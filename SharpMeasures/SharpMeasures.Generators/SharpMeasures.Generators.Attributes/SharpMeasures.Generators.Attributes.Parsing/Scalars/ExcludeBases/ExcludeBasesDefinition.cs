namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public record class ExcludeBasesDefinition : AItemListDefinition<ExcludeBasesParsingData, ExcludeBasesLocations>
{
    internal static ExcludeBasesDefinition Empty => new();

    private ExcludeBasesDefinition() : base(ExcludeBasesParsingData.Empty) { }
}
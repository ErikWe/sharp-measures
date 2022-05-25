namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public record class ExcludeBasesLocations : AItemListLocations
{
    internal static ExcludeBasesLocations Empty { get; } = new();

    private ExcludeBasesLocations() { }
}
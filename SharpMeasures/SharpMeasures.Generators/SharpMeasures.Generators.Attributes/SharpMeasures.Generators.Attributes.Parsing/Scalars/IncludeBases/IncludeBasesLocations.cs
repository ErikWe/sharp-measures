namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public record class IncludeBasesLocations : AItemListLocations
{
    internal static IncludeBasesLocations Empty { get; } = new();

    private IncludeBasesLocations() { }
}
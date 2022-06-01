namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class IncludeBasesLocations : AItemListLocations
{
    internal static IncludeBasesLocations Empty { get; } = new();

    public MinimalLocation? IncludedBasesCollection => ItemsCollection;
    public IReadOnlyList<MinimalLocation> IncludedBasesElements => ItemsElements;

    public bool ExplicitlySetIncludedBases => IncludedBasesCollection is not null;

    private IncludeBasesLocations() { }
}
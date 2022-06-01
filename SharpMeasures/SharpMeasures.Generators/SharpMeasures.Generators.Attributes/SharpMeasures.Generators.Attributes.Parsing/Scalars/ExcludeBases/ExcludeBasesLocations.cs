namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class ExcludeBasesLocations : AItemListLocations
{
    internal static ExcludeBasesLocations Empty { get; } = new();

    public MinimalLocation? ExcludedBasesCollection => ItemsCollection;
    public IReadOnlyList<MinimalLocation> ExcludedBasesElements => ItemsElements;

    public bool ExplicitlySetExcludedBases => ExcludedBasesCollection is not null;

    private ExcludeBasesLocations() { }
}
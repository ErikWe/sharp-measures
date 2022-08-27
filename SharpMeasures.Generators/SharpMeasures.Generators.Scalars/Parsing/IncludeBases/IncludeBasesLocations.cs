namespace SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class IncludeBasesLocations : AItemListLocations<IncludeBasesLocations>
{
    internal static IncludeBasesLocations Empty { get; } = new();

    public MinimalLocation? IncludedBasesCollection => ItemsCollection;
    public IReadOnlyList<MinimalLocation> IncludeBasesElements => ItemsElements;

    public MinimalLocation? StackingMode { get; init; }

    public bool ExplicitlySetIncludedBases => ExplicitlySetItems;
    public bool ExplicitlySetStackingMode => StackingMode is not null;

    protected override IncludeBasesLocations Locations => this;

    private IncludeBasesLocations() { }
}

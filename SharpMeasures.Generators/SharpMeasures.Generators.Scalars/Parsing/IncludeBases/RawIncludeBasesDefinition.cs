namespace SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

public record class RawIncludeBasesDefinition : ARawItemListDefinition<string?, RawIncludeBasesDefinition, IncludeBasesLocations>
{
    public static RawIncludeBasesDefinition Empty => new();

    public IReadOnlyList<string?> IncludedBases => Items;

    public InclusionStackingMode StackingMode { get; init; } = InclusionStackingMode.Intersection;

    protected override RawIncludeBasesDefinition Definition => this;

    private RawIncludeBasesDefinition() : base(IncludeBasesLocations.Empty) { }
}

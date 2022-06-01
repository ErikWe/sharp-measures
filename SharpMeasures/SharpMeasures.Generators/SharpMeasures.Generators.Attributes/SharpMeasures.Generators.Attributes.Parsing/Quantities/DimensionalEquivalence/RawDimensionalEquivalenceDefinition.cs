namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities.Utility;

using System.Collections.Generic;

public record class RawDimensionalEquivalenceDefinition : ARawItemListDefinition<NamedType?, DimensionalEquivalenceLocations>
{
    internal static RawDimensionalEquivalenceDefinition Empty => new();

    public IReadOnlyList<NamedType?> Quantities => Items;

    public ConversionOperationBehaviour CastOperatorBehaviour { get; init; }

    private RawDimensionalEquivalenceDefinition() : base(DimensionalEquivalenceLocations.Empty) { }
}
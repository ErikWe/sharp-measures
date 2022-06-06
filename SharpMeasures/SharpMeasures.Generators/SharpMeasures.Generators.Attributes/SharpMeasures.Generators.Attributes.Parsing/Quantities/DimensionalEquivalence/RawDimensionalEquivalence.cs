namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities.Utility;

using System.Collections.Generic;

public record class RawDimensionalEquivalence : ARawItemListDefinition<NamedType?, DimensionalEquivalenceLocations>
{
    internal static RawDimensionalEquivalence Empty => new();

    public IReadOnlyList<NamedType?> Quantities => Items;

    public ConversionOperationBehaviour CastOperatorBehaviour { get; init; }

    private RawDimensionalEquivalence() : base(DimensionalEquivalenceLocations.Empty) { }
}

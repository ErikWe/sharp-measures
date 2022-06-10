namespace SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities.Utility;

using System.Collections.Generic;

public record class RawDimensionalEquivalenceDefinition : ARawItemListDefinition<NamedType?, DimensionalEquivalenceLocations>
{
    internal static RawDimensionalEquivalenceDefinition Empty => new();

    public ReadOnlyEquatableList<NamedType?> Quantities => Items;

    public ConversionOperationBehaviour CastOperatorBehaviour { get; init; }

    private RawDimensionalEquivalenceDefinition() : base(DimensionalEquivalenceLocations.Empty) { }
}

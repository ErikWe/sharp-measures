namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Quantities.Utility;

using System.Collections.Generic;

public record class DimensionalEquivalence : AAttributeDefinition<DimensionalEquivalenceLocations>
{
    public IReadOnlyList<NamedType> Quantities { get; }

    public ConversionOperationBehaviour CastOperatorBehaviour { get; }

    public DimensionalEquivalence(IReadOnlyList<NamedType> quantities, ConversionOperationBehaviour castOperatorBehaviour,
        DimensionalEquivalenceLocations locations) : base(locations)
    {
        Quantities = quantities;
        CastOperatorBehaviour = castOperatorBehaviour;
    }
}

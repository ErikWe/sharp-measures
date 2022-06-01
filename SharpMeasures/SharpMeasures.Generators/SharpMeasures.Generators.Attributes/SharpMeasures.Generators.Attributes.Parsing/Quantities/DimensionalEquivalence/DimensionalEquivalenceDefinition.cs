namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Quantities.Utility;

using System.Collections.Generic;

public record class DimensionalEquivalenceDefinition : AAttributeDefinition<DimensionalEquivalenceLocations>
{
    public IReadOnlyList<NamedType> Quantities { get; }

    public ConversionOperationBehaviour CastOperatorBehaviour { get; }

    public DimensionalEquivalenceDefinition(IReadOnlyList<NamedType> quantities, ConversionOperationBehaviour castOperatorBehaviour,
        DimensionalEquivalenceLocations locations) : base(locations)
    {
        Quantities = quantities;
        CastOperatorBehaviour = castOperatorBehaviour;
    }
}
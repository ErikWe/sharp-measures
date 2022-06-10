namespace SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Utility;

using System.Collections.Generic;

public record class DimensionalEquivalenceDefinition : AAttributeDefinition<DimensionalEquivalenceLocations>
{
    public ReadOnlyEquatableList<NamedType> Quantities { get; }

    public ConversionOperationBehaviour CastOperatorBehaviour { get; }

    public DimensionalEquivalenceDefinition(IReadOnlyList<NamedType> quantities, ConversionOperationBehaviour castOperatorBehaviour,
        DimensionalEquivalenceLocations locations) : base(locations)
    {
        Quantities = new(quantities);
        CastOperatorBehaviour = castOperatorBehaviour;
    }
}

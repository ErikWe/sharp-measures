namespace SharpMeasures.Generators.Scalars.Refinement;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Utility;

using System.Collections.Generic;

internal readonly record struct RefinedDimensionalEquivalenceDefinition
{
    public ReadOnlyEquatableList<ScalarInterface> Quantities { get; }

    public ConversionOperationBehaviour CastOperatorBehaviour { get; }

    public RefinedDimensionalEquivalenceDefinition(IReadOnlyList<ScalarInterface> quantities, ConversionOperationBehaviour castOperatorBehaviour)
    {
        Quantities = quantities.AsReadOnlyEquatable();
        CastOperatorBehaviour = castOperatorBehaviour;
    }
}

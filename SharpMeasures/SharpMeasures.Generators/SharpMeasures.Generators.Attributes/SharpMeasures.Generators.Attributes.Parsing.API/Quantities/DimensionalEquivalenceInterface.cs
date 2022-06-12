namespace SharpMeasures.Generators.Quantities;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Utility;

using System.Collections.Generic;

public class DimensionalEquivalenceInterface
{
    public ReadOnlyEquatableList<NamedType> Quantities { get; }

    public ConversionOperationBehaviour CastOperatorBehaviour { get; }

    public DimensionalEquivalenceInterface(IReadOnlyList<NamedType> quantities, ConversionOperationBehaviour castOperatorBehaviour)
    {
        Quantities = quantities.AsReadOnlyEquatable();

        CastOperatorBehaviour = castOperatorBehaviour;
    }
}

namespace SharpMeasures.Generators.Scalars.Refinement.DimensionalEquivalence;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

internal readonly record struct RefinedConvertibleQuantityDefinition
{
    public ReadOnlyEquatableList<IScalarType> Quantities { get; }

    public ConversionOperatorBehaviour CastOperatorBehaviour { get; }

    public RefinedConvertibleQuantityDefinition(IReadOnlyList<IScalarType> quantities, ConversionOperatorBehaviour castOperatorBehaviour)
    {
        Quantities = quantities.AsReadOnlyEquatable();
        CastOperatorBehaviour = castOperatorBehaviour;
    }
}

namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public sealed record class ForeignScalarProcessingResult
{
    internal IReadOnlyList<ScalarBaseType> ScalarBases { get; }
    internal IReadOnlyList<ScalarSpecializationType> ScalarSpecializations { get; }

    internal ForeignScalarProcessingResult(IReadOnlyList<ScalarBaseType> scalarBases, IReadOnlyList<ScalarSpecializationType> scalarSpecializations)
    {
        ScalarBases = scalarBases.AsReadOnlyEquatable();
        ScalarSpecializations = scalarSpecializations.AsReadOnlyEquatable();
    }
}

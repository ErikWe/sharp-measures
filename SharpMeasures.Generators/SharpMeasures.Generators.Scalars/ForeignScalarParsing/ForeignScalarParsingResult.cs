namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Scalars.Parsing;

using System.Collections.Generic;

public sealed record class ForeignScalarParsingResult
{
    internal IReadOnlyList<RawScalarBaseType> ScalarBases { get; }
    internal IReadOnlyList<RawScalarSpecializationType> ScalarSpecializations { get; }

    internal ForeignScalarParsingResult(IReadOnlyList<RawScalarBaseType> scalarBases, IReadOnlyList<RawScalarSpecializationType> scalarSpecializations)
    {
        ScalarBases = scalarBases.AsReadOnlyEquatable();
        ScalarSpecializations = scalarSpecializations.AsReadOnlyEquatable();
    }
}

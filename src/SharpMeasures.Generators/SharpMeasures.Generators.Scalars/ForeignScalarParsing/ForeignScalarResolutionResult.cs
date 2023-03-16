namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public sealed record class ForeignScalarResolutionResult
{
    public IReadOnlyList<IResolvedScalarType> Scalars { get; }

    internal ForeignScalarResolutionResult(IReadOnlyList<IResolvedScalarType> scalars)
    {
        Scalars = scalars.AsReadOnlyEquatable();
    }
}

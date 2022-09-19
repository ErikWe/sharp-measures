namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using SharpMeasures.Equatables;

using System.Collections.Generic;

internal sealed record class ForeignVectorResolutionResult
{
    public IReadOnlyList<IResolvedVectorGroupType> Groups { get; }
    public IReadOnlyList<IResolvedVectorType> Vectors { get; }

    public ForeignVectorResolutionResult(IReadOnlyList<IResolvedVectorGroupType> groups, IReadOnlyList<IResolvedVectorType> vectors)
    {
        Groups = groups.AsReadOnlyEquatable();
        Vectors = vectors.AsReadOnlyEquatable();
    }
}

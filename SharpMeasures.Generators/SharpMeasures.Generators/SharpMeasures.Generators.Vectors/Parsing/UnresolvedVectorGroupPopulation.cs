namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Generic;

internal class UnresolvedVectorGroupPopulation : IUnresolvedVectorGroupPopulation
{
    public IReadOnlyDictionary<int, IUnresolvedVectorGroupMemberType> VectorGroupMembersByDimension => vectorGroupMembersByDimension;

    private ReadOnlyEquatableDictionary<int, IUnresolvedVectorGroupMemberType> vectorGroupMembersByDimension { get; }

    public UnresolvedVectorGroupPopulation(IReadOnlyDictionary<int, IUnresolvedVectorGroupMemberType> vectorGroupMembersByDimension)
    {
        this.vectorGroupMembersByDimension = vectorGroupMembersByDimension.AsReadOnlyEquatable();
    }
}

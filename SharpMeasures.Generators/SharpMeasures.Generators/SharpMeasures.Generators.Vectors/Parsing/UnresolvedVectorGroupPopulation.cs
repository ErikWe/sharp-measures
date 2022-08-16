namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Raw.Vectors;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using System.Collections.Generic;

internal class UnresolvedVectorGroupPopulation : IRawVectorGroupPopulation
{
    public IReadOnlyDictionary<int, IRawVectorGroupMemberType> VectorGroupMembersByDimension => vectorGroupMembersByDimension;

    private ReadOnlyEquatableDictionary<int, IRawVectorGroupMemberType> vectorGroupMembersByDimension { get; }

    public UnresolvedVectorGroupPopulation(IReadOnlyDictionary<int, IRawVectorGroupMemberType> vectorGroupMembersByDimension)
    {
        this.vectorGroupMembersByDimension = vectorGroupMembersByDimension.AsReadOnlyEquatable();
    }
}

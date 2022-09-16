namespace SharpMeasures.Generators.Populations;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Vectors;

using System.Collections.Generic;

internal class VectorGroupPopulation : IGroupPopulation
{
    public IReadOnlyDictionary<int, IVectorGroupMemberType> GroupMembersByDimension => vectorGroupMembersByDimension;

    private ReadOnlyEquatableDictionary<int, IVectorGroupMemberType> vectorGroupMembersByDimension { get; }

    public VectorGroupPopulation(IReadOnlyDictionary<int, IVectorGroupMemberType> vectorGroupMembersByDimension)
    {
        this.vectorGroupMembersByDimension = vectorGroupMembersByDimension.AsReadOnlyEquatable();
    }
}

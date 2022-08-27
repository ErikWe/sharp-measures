namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;

using System.Collections.Generic;

internal class GroupPopulation : IGroupPopulation
{
    public IReadOnlyDictionary<int, IVectorGroupMemberType> GroupMembersByDimension => vectorGroupMembersByDimension;

    private ReadOnlyEquatableDictionary<int, IVectorGroupMemberType> vectorGroupMembersByDimension { get; }

    public GroupPopulation(IReadOnlyDictionary<int, IVectorGroupMemberType> vectorGroupMembersByDimension)
    {
        this.vectorGroupMembersByDimension = vectorGroupMembersByDimension.AsReadOnlyEquatable();
    }
}

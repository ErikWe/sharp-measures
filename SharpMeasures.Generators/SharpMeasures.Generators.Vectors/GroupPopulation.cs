namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;

using System.Collections.Generic;

internal sealed record class GroupPopulation : IGroupPopulation
{
    public IReadOnlyDictionary<int, IVectorGroupMemberType> GroupMembersByDimension { get; }

    public GroupPopulation(IReadOnlyDictionary<int, IVectorGroupMemberType> groupMembersByDimension)
    {
        GroupMembersByDimension = groupMembersByDimension.AsReadOnlyEquatable();
    }
}

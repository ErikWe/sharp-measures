namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using SharpMeasures.Equatables;

using System.Collections.Generic;

internal sealed record class ForeignVectorProcessingResult
{
    public IReadOnlyList<GroupBaseType> GroupBases { get; }
    public IReadOnlyList<GroupSpecializationType> GroupSpecializations { get; }
    public IReadOnlyList<GroupMemberType> GroupMembers { get; }

    public IReadOnlyList<VectorBaseType> VectorBases { get; }
    public IReadOnlyList<VectorSpecializationType> VectorSpecializations { get; }

    public ForeignVectorProcessingResult(IReadOnlyList<GroupBaseType> groupBases, IReadOnlyList<GroupSpecializationType> groupSpecializations, IReadOnlyList<GroupMemberType> groupMembers, IReadOnlyList<VectorBaseType> vectorBases, IReadOnlyList<VectorSpecializationType> vectorSpecializations)
    {
        GroupBases = groupBases.AsReadOnlyEquatable();
        GroupSpecializations = groupSpecializations.AsReadOnlyEquatable();
        GroupMembers = groupMembers.AsReadOnlyEquatable();

        VectorBases = vectorBases.AsReadOnlyEquatable();
        VectorSpecializations = vectorSpecializations.AsReadOnlyEquatable();
    }
}

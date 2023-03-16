namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public sealed record class ForeignVectorProcessingResult
{
    internal IReadOnlyList<GroupBaseType> GroupBases { get; }
    internal IReadOnlyList<GroupSpecializationType> GroupSpecializations { get; }
    internal IReadOnlyList<GroupMemberType> GroupMembers { get; }

    internal IReadOnlyList<VectorBaseType> VectorBases { get; }
    internal IReadOnlyList<VectorSpecializationType> VectorSpecializations { get; }

    internal ForeignVectorProcessingResult(IReadOnlyList<GroupBaseType> groupBases, IReadOnlyList<GroupSpecializationType> groupSpecializations, IReadOnlyList<GroupMemberType> groupMembers, IReadOnlyList<VectorBaseType> vectorBases, IReadOnlyList<VectorSpecializationType> vectorSpecializations)
    {
        GroupBases = groupBases.AsReadOnlyEquatable();
        GroupSpecializations = groupSpecializations.AsReadOnlyEquatable();
        GroupMembers = groupMembers.AsReadOnlyEquatable();

        VectorBases = vectorBases.AsReadOnlyEquatable();
        VectorSpecializations = vectorSpecializations.AsReadOnlyEquatable();
    }
}

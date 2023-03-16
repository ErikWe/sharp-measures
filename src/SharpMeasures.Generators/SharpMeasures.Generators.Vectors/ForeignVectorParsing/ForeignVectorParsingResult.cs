namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Vectors.Parsing;

using System.Collections.Generic;

public sealed record class ForeignVectorParsingResult
{
    internal IReadOnlyList<RawGroupBaseType> GroupBases { get; }
    internal IReadOnlyList<RawGroupSpecializationType> GroupSpecializations { get; }
    internal IReadOnlyList<RawGroupMemberType> GroupMembers { get; }

    internal IReadOnlyList<RawVectorBaseType> VectorBases { get; }
    internal IReadOnlyList<RawVectorSpecializationType> VectorSpecializations { get; }

    internal ForeignVectorParsingResult(IReadOnlyList<RawGroupBaseType> groupBases, IReadOnlyList<RawGroupSpecializationType> groupSpecializations, IReadOnlyList<RawGroupMemberType> groupMembers, IReadOnlyList<RawVectorBaseType> vectorBases, IReadOnlyList<RawVectorSpecializationType> vectorSpecializations)
    {
        GroupBases = groupBases.AsReadOnlyEquatable();
        GroupSpecializations = groupSpecializations.AsReadOnlyEquatable();
        GroupMembers = groupMembers.AsReadOnlyEquatable();

        VectorBases = vectorBases.AsReadOnlyEquatable();
        VectorSpecializations = vectorSpecializations.AsReadOnlyEquatable();
    }
}

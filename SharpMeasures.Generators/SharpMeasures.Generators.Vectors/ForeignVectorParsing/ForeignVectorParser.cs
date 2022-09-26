namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Vectors.Parsing;

using System;
using System.Collections.Generic;

public sealed class ForeignVectorParser
{
    private static GroupBaseParser GroupBaseParser { get; } = new();
    private static GroupSpecializationParser GroupSpecializationParser { get; } = new();
    private static GroupMemberParser GroupMemberParser { get; } = new();

    private static VectorBaseParser VectorBaseParser { get; } = new();
    private static VectorSpecializationParser VectorSpecializationParser { get; } = new();

    private List<RawGroupBaseType> GroupBases { get; } = new();
    private List<RawGroupSpecializationType> GroupSpecializations { get; } = new();
    private List<RawGroupMemberType> GroupMembers { get; } = new();

    private List<RawVectorBaseType> VectorBases { get; } = new();
    private List<RawVectorSpecializationType> VectorSpecializations { get; } = new();

    public (bool Success, IEnumerable<INamedTypeSymbol> ReferencedSymbols) TryParse(INamedTypeSymbol typeSymbol)
    {
        (var groupBase, var groupBaseSymbols) = GroupBaseParser.Parse(typeSymbol);

        if (groupBase.HasValue)
        {
            GroupBases.Add(groupBase.Value);

            return (true, groupBaseSymbols);
        }

        (var groupSpecialization, var groupSpecializationSymbols) = GroupSpecializationParser.Parse(typeSymbol);

        if (groupSpecialization.HasValue)
        {
            GroupSpecializations.Add(groupSpecialization.Value);

            return (true, groupSpecializationSymbols);
        }

        (var member, var memberSymbols) = GroupMemberParser.Parse(typeSymbol);

        if (member.HasValue)
        {
            GroupMembers.Add(member.Value);

            return (true, memberSymbols);
        }

        (var vectorBase, var vectorBaseSymbols) = VectorBaseParser.Parse(typeSymbol);

        if (vectorBase.HasValue)
        {
            VectorBases.Add(vectorBase.Value);

            return (true, vectorBaseSymbols);
        }

        (var vectorSpecialization, var vectorSpecializationSymbols) = VectorSpecializationParser.Parse(typeSymbol);

        if (vectorSpecialization.HasValue)
        {
            VectorSpecializations.Add(vectorSpecialization.Value);

            return (true, vectorSpecializationSymbols);
        }

        return (false, Array.Empty<INamedTypeSymbol>());
    }

    public ForeignVectorParsingResult Finalize() => new(GroupBases, GroupSpecializations, GroupMembers, VectorBases, VectorSpecializations);
}

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
        var groupBaseResult = GroupBaseParser.Parse(typeSymbol);

        if (groupBaseResult.HasValue)
        {
            GroupBases.Add(groupBaseResult.Value.Definition);

            return (true, groupBaseResult.Value.ForeignSymbols);
        }

        var groupSpecializationResult = GroupSpecializationParser.Parse(typeSymbol);

        if (groupSpecializationResult.HasValue)
        {
            GroupSpecializations.Add(groupSpecializationResult.Value.Definition);

            return (true, groupSpecializationResult.Value.ForeignSymbols);
        }

        var groupMemberResult = GroupMemberParser.Parse(typeSymbol);

        if (groupMemberResult.HasValue)
        {
            GroupMembers.Add(groupMemberResult.Value.Definition);

            return (true, groupMemberResult.Value.ForeignSymbols);
        }

        var vectorBaseResult = VectorBaseParser.Parse(typeSymbol);

        if (vectorBaseResult.HasValue)
        {
            VectorBases.Add(vectorBaseResult.Value.Definition);

            return (true, vectorBaseResult.Value.ForeignSymbols);
        }

        var vectorSpecializationResult = VectorSpecializationParser.Parse(typeSymbol);

        if (vectorSpecializationResult.HasValue)
        {
            VectorSpecializations.Add(vectorSpecializationResult.Value.Definition);

            return (true, vectorSpecializationResult.Value.ForeignSymbols);
        }

        return (false, Array.Empty<INamedTypeSymbol>());
    }

    public ForeignVectorParsingResult Finalize() => new(GroupBases, GroupSpecializations, GroupMembers, VectorBases, VectorSpecializations);
}

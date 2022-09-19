namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Vectors.Parsing;

using System.Collections.Generic;

public sealed class ForeignVectorParser
{
    private List<RawGroupBaseType> GroupBases { get; } = new();
    private List<RawGroupSpecializationType> GroupSpecializations { get; } = new();
    private List<RawGroupMemberType> GroupMembers { get; } = new();

    private List<RawVectorBaseType> VectorBases { get; } = new();
    private List<RawVectorSpecializationType> VectorSpecializations { get; } = new();

    public Optional<IEnumerable<INamedTypeSymbol>> TryParse(INamedTypeSymbol typeSymbol)
    {
        (var groupBase, var groupBaseSymbols) = ForeignGroupBaseParser.Parse(typeSymbol);

        if (groupBase.HasValue)
        {
            GroupBases.Add(groupBase.Value);

            return new Optional<IEnumerable<INamedTypeSymbol>>(groupBaseSymbols);
        }

        (var groupSpecialization, var groupSpecializationSymbols) = ForeignGroupSpecializationParser.Parse(typeSymbol);

        if (groupSpecialization.HasValue)
        {
            GroupSpecializations.Add(groupSpecialization.Value);

            return new Optional<IEnumerable<INamedTypeSymbol>>(groupSpecializationSymbols);
        }

        (var member, var memberSymbols) = ForeignGroupMemberParser.Parse(typeSymbol);

        if (member.HasValue)
        {
            GroupMembers.Add(member.Value);

            return new Optional<IEnumerable<INamedTypeSymbol>>(memberSymbols);
        }

        (var vectorBase, var vectorBaseSymbols) = ForeignVectorBaseParser.Parse(typeSymbol);

        if (vectorBase.HasValue)
        {
            VectorBases.Add(vectorBase.Value);

            return new Optional<IEnumerable<INamedTypeSymbol>>(vectorBaseSymbols);
        }

        (var vectorSpecialization, var vectorSpecializationSymbols) = ForeignVectorSpecializationParser.Parse(typeSymbol);

        if (vectorSpecialization.HasValue)
        {
            VectorSpecializations.Add(vectorSpecialization.Value);

            return new Optional<IEnumerable<INamedTypeSymbol>>(vectorSpecializationSymbols);
        }

        return new Optional<IEnumerable<INamedTypeSymbol>>();
    }

    public IForeignVectorProcesser Finalize() => new ForeignVectorProcesser(new ForeignVectorParsingResult(GroupBases, GroupSpecializations, GroupMembers, VectorBases, VectorSpecializations));
}

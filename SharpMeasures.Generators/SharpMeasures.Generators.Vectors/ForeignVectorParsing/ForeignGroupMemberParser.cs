namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using System;
using System.Collections.Generic;
using System.Linq;

internal static class ForeignGroupMemberParser
{
    public static (Optional<RawGroupMemberType> Definition, IEnumerable<INamedTypeSymbol> ReferencedSymbols) Parse(INamedTypeSymbol typeSymbol)
    {
        (var member, var memberReferencedSymbols) = ParseMember(typeSymbol);

        if (member.HasValue is false)
        {
            return (new Optional<RawGroupMemberType>(), Array.Empty<INamedTypeSymbol>());
        }

        (var derivations, var derivationsReferencedSymbols) = CommonParsing.ParseDerivations(typeSymbol);
        var constants = CommonParsing.ParseConstants(typeSymbol);
        (var conversions, var conversionsReferencedSymbols) = CommonParsing.ParseConversions(typeSymbol);

        var includeUnitInstances = CommonParsing.ParseIncludeUnits(typeSymbol);
        var excludeUnitInstances = CommonParsing.ParseExcludeUnits(typeSymbol);

        RawGroupMemberType rawMemberType = new(typeSymbol.AsDefinedType(), MinimalLocation.None, member.Value, derivations, constants, conversions, includeUnitInstances, excludeUnitInstances);
        var referencedSymbols = memberReferencedSymbols.Concat(derivationsReferencedSymbols).Concat(conversionsReferencedSymbols);

        return (rawMemberType, referencedSymbols);
    }
    
    private static (Optional<RawSharpMeasuresVectorGroupMemberDefinition>, IEnumerable<INamedTypeSymbol>) ParseMember(INamedTypeSymbol typeSymbol)
    {
        if (SharpMeasuresVectorGroupMemberParser.Parser.ParseFirstOccurrence(typeSymbol) is not SymbolicSharpMeasuresVectorGroupMemberDefinition symbolicMember)
        {
            return (new Optional<RawSharpMeasuresVectorGroupMemberDefinition>(), Array.Empty<INamedTypeSymbol>());
        }

        var rawMember = RawSharpMeasuresVectorGroupMemberDefinition.FromSymbolic(symbolicMember);
        var foreignSymbols = symbolicMember.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: true);

        return (rawMember, foreignSymbols);
    }
}

namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using System;
using System.Collections.Generic;
using System.Linq;

public static class ForeignGroupSpecializationParser
{
    public static (Optional<IVectorGroupSpecializationType> Definition, IEnumerable<INamedTypeSymbol> ReferencedSymbols) Parse(INamedTypeSymbol typeSymbol)
    {
        (var group, var groupReferencedSymbols) = ParseGroup(typeSymbol);

        if (group.HasValue is false)
        {
            return (new Optional<IVectorGroupSpecializationType>(), Array.Empty<INamedTypeSymbol>());
        }

        (var derivations, var derivationsReferencedSymbols) = CommonParsing.ParseDerivations(typeSymbol);
        (var conversions, var conversionsReferencedSymbols) = CommonParsing.ParseConversions(typeSymbol);

        var includeUnitInstances = CommonParsing.ParseIncludeUnits(typeSymbol);
        var excludeUnitInstances = CommonParsing.ParseExcludeUnits(typeSymbol);

        RawGroupSpecializationType rawGroupType = new(typeSymbol.AsDefinedType(), MinimalLocation.None, group.Value, derivations, conversions, includeUnitInstances, excludeUnitInstances);

        var groupType = new ForeignGroupSpecializationProcesser().Process(rawGroupType);

        if (groupType.HasValue is false)
        {
            return (new Optional<IVectorGroupSpecializationType>(), Array.Empty<INamedTypeSymbol>());
        }

        var referencedSymbols = groupReferencedSymbols.Concat(derivationsReferencedSymbols).Concat(conversionsReferencedSymbols);

        return (groupType.Value, referencedSymbols);
    }
    
    private static (Optional<RawSpecializedSharpMeasuresVectorGroupDefinition>, IEnumerable<INamedTypeSymbol>) ParseGroup(INamedTypeSymbol typeSymbol)
    {
        if (SpecializedSharpMeasuresVectorGroupParser.Parser.ParseFirstOccurrence(typeSymbol) is not SymbolicSpecializedSharpMeasuresVectorGroupDefinition symbolicGroup)
        {
            return (new Optional<RawSpecializedSharpMeasuresVectorGroupDefinition>(), Array.Empty<INamedTypeSymbol>());
        }

        var rawGroup = RawSpecializedSharpMeasuresVectorGroupDefinition.FromSymbolic(symbolicGroup);
        var foreignSymbols = symbolicGroup.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: true);

        return (rawGroup, foreignSymbols);
    }
}

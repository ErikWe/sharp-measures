namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using System;
using System.Collections.Generic;
using System.Linq;

public static class ForeignGroupBaseParser
{
    public static (Optional<IVectorGroupBaseType> Definition, IEnumerable<INamedTypeSymbol> ReferencedSymbols) Parse(INamedTypeSymbol typeSymbol)
    {
        (var group, var groupReferencedSymbols) = ParseGroup(typeSymbol);

        if (group.HasValue is false)
        {
            return (new Optional<IVectorGroupBaseType>(), Array.Empty<INamedTypeSymbol>());
        }

        (var derivations, var derivationsReferencedSymbols) = CommonParsing.ParseDerivations(typeSymbol);
        (var conversions, var conversionsReferencedSymbols) = CommonParsing.ParseConversions(typeSymbol);

        var includeUnitInstances = CommonParsing.ParseIncludeUnits(typeSymbol);
        var excludeUnitInstances = CommonParsing.ParseExcludeUnits(typeSymbol);

        RawGroupBaseType rawGroupType = new(typeSymbol.AsDefinedType(), MinimalLocation.None, group.Value, derivations, conversions, includeUnitInstances, excludeUnitInstances);

        var groupType = new ForeignGroupBaseProcesser().Process(rawGroupType);

        if (groupType.HasValue is false)
        {
            return (new Optional<IVectorGroupBaseType>(), Array.Empty<INamedTypeSymbol>());
        }

        var referencedSymbols = groupReferencedSymbols.Concat(derivationsReferencedSymbols).Concat(conversionsReferencedSymbols);

        return (groupType.Value, referencedSymbols);
    }
    
    private static (Optional<RawSharpMeasuresVectorGroupDefinition>, IEnumerable<INamedTypeSymbol>) ParseGroup(INamedTypeSymbol typeSymbol)
    {
        if (SharpMeasuresVectorGroupParser.Parser.ParseFirstOccurrence(typeSymbol) is not SymbolicSharpMeasuresVectorGroupDefinition symbolicGroup)
        {
            return (new Optional<RawSharpMeasuresVectorGroupDefinition>(), Array.Empty<INamedTypeSymbol>());
        }

        var rawGroup = RawSharpMeasuresVectorGroupDefinition.FromSymbolic(symbolicGroup);
        var foreignSymbols = symbolicGroup.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: true);

        return (rawGroup, foreignSymbols);
    }
}

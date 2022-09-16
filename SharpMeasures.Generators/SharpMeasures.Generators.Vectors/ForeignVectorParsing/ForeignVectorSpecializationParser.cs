namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using System;
using System.Collections.Generic;
using System.Linq;

public static class ForeignVectorSpecializationParser
{
    public static (Optional<IVectorSpecializationType> Definition, IEnumerable<INamedTypeSymbol> ReferencedSymbols) Parse(INamedTypeSymbol typeSymbol)
    {
        (var vector, var vectorReferencedSymbols) = ParseVector(typeSymbol);

        if (vector.HasValue is false)
        {
            return (new Optional<IVectorSpecializationType>(), Array.Empty<INamedTypeSymbol>());
        }

        (var derivations, var derivationsReferencedSymbols) = CommonParsing.ParseDerivations(typeSymbol);
        var constants = CommonParsing.ParseConstants(typeSymbol);
        (var conversions, var conversionsReferencedSymbols) = CommonParsing.ParseConversions(typeSymbol);

        var includeUnitInstances = CommonParsing.ParseIncludeUnits(typeSymbol);
        var excludeUnitInstances = CommonParsing.ParseExcludeUnits(typeSymbol);

        RawVectorSpecializationType rawVectorType = new(typeSymbol.AsDefinedType(), MinimalLocation.None, vector.Value, derivations, constants, conversions, includeUnitInstances, excludeUnitInstances);

        var vectorType = new ForeignVectorSpecializationProcesser().Process(rawVectorType);

        if (vectorType.HasValue is false)
        {
            return (new Optional<IVectorSpecializationType>(), Array.Empty<INamedTypeSymbol>());
        }

        var referencedSymbols = vectorReferencedSymbols.Concat(derivationsReferencedSymbols).Concat(conversionsReferencedSymbols);

        return (vectorType.Value, referencedSymbols);
    }
    
    private static (Optional<RawSpecializedSharpMeasuresVectorDefinition>, IEnumerable<INamedTypeSymbol>) ParseVector(INamedTypeSymbol typeSymbol)
    {
        if (SpecializedSharpMeasuresVectorParser.Parser.ParseFirstOccurrence(typeSymbol) is not SymbolicSpecializedSharpMeasuresVectorDefinition symbolicVector)
        {
            return (new Optional<RawSpecializedSharpMeasuresVectorDefinition>(), Array.Empty<INamedTypeSymbol>());
        }

        var rawVector = RawSpecializedSharpMeasuresVectorDefinition.FromSymbolic(symbolicVector);
        var foreignSymbols = symbolicVector.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: true);

        return (rawVector, foreignSymbols);
    }
}

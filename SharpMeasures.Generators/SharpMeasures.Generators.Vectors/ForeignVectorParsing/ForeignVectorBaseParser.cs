﻿namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using System;
using System.Collections.Generic;
using System.Linq;

internal static class ForeignVectorBaseParser
{
    public static (Optional<RawVectorBaseType> Definition, IEnumerable<INamedTypeSymbol> ReferencedSymbols) Parse(INamedTypeSymbol typeSymbol)
    {
        (var vector, var vectorReferencedSymbols) = ParseVector(typeSymbol);

        if (vector.HasValue is false)
        {
            return (new Optional<RawVectorBaseType>(), Array.Empty<INamedTypeSymbol>());
        }

        (var derivations, var derivationsReferencedSymbols) = CommonParsing.ParseDerivations(typeSymbol);
        var constants = CommonParsing.ParseConstants(typeSymbol);
        (var conversions, var conversionsReferencedSymbols) = CommonParsing.ParseConversions(typeSymbol);

        var includeUnitInstances = CommonParsing.ParseIncludeUnits(typeSymbol);
        var excludeUnitInstances = CommonParsing.ParseExcludeUnits(typeSymbol);

        RawVectorBaseType rawVectorType = new(typeSymbol.AsDefinedType(), MinimalLocation.None, vector.Value, derivations, constants, conversions, includeUnitInstances, excludeUnitInstances);
        var referencedSymbols = vectorReferencedSymbols.Concat(derivationsReferencedSymbols).Concat(conversionsReferencedSymbols);

        return (rawVectorType, referencedSymbols);
    }
    
    private static (Optional<RawSharpMeasuresVectorDefinition>, IEnumerable<INamedTypeSymbol>) ParseVector(INamedTypeSymbol typeSymbol)
    {
        if (SharpMeasuresVectorParser.Parser.ParseFirstOccurrence(typeSymbol) is not SymbolicSharpMeasuresVectorDefinition symbolicVector)
        {
            return (new Optional<RawSharpMeasuresVectorDefinition>(), Array.Empty<INamedTypeSymbol>());
        }

        var rawVector = RawSharpMeasuresVectorDefinition.FromSymbolic(symbolicVector);
        var foreignSymbols = symbolicVector.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: true);

        return (rawVector, foreignSymbols);
    }
}

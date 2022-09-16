namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars.Parsing;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using System;
using System.Collections.Generic;
using System.Linq;

public static class ForeignScalarBaseParser
{
    public static (Optional<IScalarBaseType> Definition, IEnumerable<INamedTypeSymbol> ReferencedSymbols) Parse(INamedTypeSymbol typeSymbol)
    {
        (var scalar, var scalarReferencedSymbols) = ParseScalar(typeSymbol);

        if (scalar.HasValue is false)
        {
            return (new Optional<IScalarBaseType>(), Array.Empty<INamedTypeSymbol>());
        }

        (var derivations, var derivationsReferencedSymbols) = CommonParsing.ParseDerivations(typeSymbol);
        var constants = CommonParsing.ParseConstants(typeSymbol);
        (var conversions, var conversionsReferencedSymbols) = CommonParsing.ParseConversions(typeSymbol);

        var includeUnitInstanceBases = CommonParsing.ParseIncludeUnitBases(typeSymbol);
        var excludeUnitInstanceBases = CommonParsing.ParseExcludeUnitBases(typeSymbol);

        var includeUnitInstances = CommonParsing.ParseIncludeUnits(typeSymbol);
        var excludeUnitInstances = CommonParsing.ParseExcludeUnits(typeSymbol);

        RawScalarBaseType rawScalarType = new(typeSymbol.AsDefinedType(), MinimalLocation.None, scalar.Value, derivations, constants, conversions, includeUnitInstanceBases, excludeUnitInstanceBases, includeUnitInstances, excludeUnitInstances);

        var scalarType = new ForeignScalarBaseProcesser().Process(rawScalarType);

        if (scalarType.HasValue is false)
        {
            return (new Optional<IScalarBaseType>(), Array.Empty<INamedTypeSymbol>());
        }

        var referencedSymbols = scalarReferencedSymbols.Concat(derivationsReferencedSymbols).Concat(conversionsReferencedSymbols);

        return (scalarType.Value, referencedSymbols);
    }
    
    private static (Optional<RawSharpMeasuresScalarDefinition>, IEnumerable<INamedTypeSymbol>) ParseScalar(INamedTypeSymbol typeSymbol)
    {
        if (SharpMeasuresScalarParser.Parser.ParseFirstOccurrence(typeSymbol) is not SymbolicSharpMeasuresScalarDefinition symbolicScalar)
        {
            return (new Optional<RawSharpMeasuresScalarDefinition>(), Array.Empty<INamedTypeSymbol>());
        }

        var rawScalar = RawSharpMeasuresScalarDefinition.FromSymbolic(symbolicScalar);
        var foreignSymbols = symbolicScalar.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: true);

        return (rawScalar, foreignSymbols);
    }
}

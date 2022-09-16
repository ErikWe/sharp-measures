namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars.Parsing;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using System;
using System.Collections.Generic;
using System.Linq;

public static class ForeignScalarSpecializationParser
{
    public static (Optional<IScalarSpecializationType> Definition, IEnumerable<INamedTypeSymbol> ReferencedSymbols) Parse(INamedTypeSymbol typeSymbol)
    {
        (var scalar, var scalarReferencedSymbols) = ParseScalar(typeSymbol);

        if (scalar.HasValue is false)
        {
            return (new Optional<IScalarSpecializationType>(), Array.Empty<INamedTypeSymbol>());
        }

        (var derivations, var derivationsReferencedSymbols) = CommonParsing.ParseDerivations(typeSymbol);
        var constants = CommonParsing.ParseConstants(typeSymbol);
        (var conversions, var conversionsReferencedSymbols) = CommonParsing.ParseConversions(typeSymbol);

        var includeUnitInstanceBases = CommonParsing.ParseIncludeUnitBases(typeSymbol);
        var excludeUnitInstanceBases = CommonParsing.ParseExcludeUnitBases(typeSymbol);

        var includeUnitInstances = CommonParsing.ParseIncludeUnits(typeSymbol);
        var excludeUnitInstances = CommonParsing.ParseExcludeUnits(typeSymbol);

        RawScalarSpecializationType rawScalarType = new(typeSymbol.AsDefinedType(), MinimalLocation.None, scalar.Value, derivations, constants, conversions, includeUnitInstanceBases, excludeUnitInstanceBases, includeUnitInstances, excludeUnitInstances);

        var scalarType = new ForeignScalarSpecializationProcesser().Process(rawScalarType);

        if (scalarType.HasValue is false)
        {
            return (new Optional<IScalarSpecializationType>(), Array.Empty<INamedTypeSymbol>());
        }

        var referencedSymbols = scalarReferencedSymbols.Concat(derivationsReferencedSymbols).Concat(conversionsReferencedSymbols);

        return (scalarType.Value, referencedSymbols);
    }
    
    private static (Optional<RawSpecializedSharpMeasuresScalarDefinition>, IEnumerable<INamedTypeSymbol>) ParseScalar(INamedTypeSymbol typeSymbol)
    {
        if (SpecializedSharpMeasuresScalarParser.Parser.ParseFirstOccurrence(typeSymbol) is not SymbolicSpecializedSharpMeasuresScalarDefinition symbolicScalar)
        {
            return (new Optional<RawSpecializedSharpMeasuresScalarDefinition>(), Array.Empty<INamedTypeSymbol>());
        }

        var rawScalar = RawSpecializedSharpMeasuresScalarDefinition.FromSymbolic(symbolicScalar);
        var foreignSymbols = symbolicScalar.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: true);

        return (rawScalar, foreignSymbols);
    }
}

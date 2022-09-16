namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;
using System.Linq;

internal static class CommonParsing
{
    public static (IEnumerable<RawDerivedQuantityDefinition> Definitions, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseDerivations(INamedTypeSymbol typeSymbol)
    {
        var symbolicDerivations = DerivedQuantityParser.Parser.ParseAllOccurrences(typeSymbol);

        var rawDerivations = symbolicDerivations.Select(static (symbolicDerivation) => RawDerivedQuantityDefinition.FromSymbolic(symbolicDerivation));
        var foreignSymbols = symbolicDerivations.SelectMany((symbolicDerivation) => symbolicDerivation.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: false));

        return (rawDerivations, foreignSymbols);
    }

    public static IEnumerable<RawVectorConstantDefinition> ParseConstants(INamedTypeSymbol typeSymbol)
    {
        return VectorConstantParser.Parser.ParseAllOccurrences(typeSymbol);
    }

    public static (IEnumerable<RawConvertibleQuantityDefinition> Definitions, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseConversions(INamedTypeSymbol typeSymbol)
    {
        var symbolicConversions = ConvertibleQuantityParser.Parser.ParseAllOccurrences(typeSymbol);

        var rawConversions = symbolicConversions.Select(static (symbolicConversion) => RawConvertibleQuantityDefinition.FromSymbolic(symbolicConversion));
        var foreignSymbols = symbolicConversions.SelectMany((symbolicConversion) => symbolicConversion.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: false));

        return (rawConversions, foreignSymbols);
    }

    public static IEnumerable<RawIncludeUnitsDefinition> ParseIncludeUnitInstances(INamedTypeSymbol typeSymbol)
    {
        return IncludeUnitsParser.Parser.ParseAllOccurrences(typeSymbol);
    }

    public static IEnumerable<RawExcludeUnitsDefinition> ParseExcludeUnitInstances(INamedTypeSymbol typeSymbol)
    {
        return ExcludeUnitsParser.Parser.ParseAllOccurrences(typeSymbol);
    }
}

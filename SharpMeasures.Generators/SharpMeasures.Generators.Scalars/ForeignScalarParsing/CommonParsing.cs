namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using System.Collections.Generic;
using System.Linq;

internal static class CommonParsing
{
    public static (IEnumerable<RawDerivedQuantityDefinition> Definitions, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseDerivations(INamedTypeSymbol typeSymbol)
    {
        var symbolicDerivations = DerivedQuantityParser.Parser.ParseAllOccurrences(typeSymbol);

        var rawDerivations = symbolicDerivations.Select(static (symbolicDerivation) => RawDerivedQuantityDefinition.FromSymbolic(symbolicDerivation));
        var foreignSymbols = symbolicDerivations.SelectMany((symbolicDerivation) => symbolicDerivation.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: true));

        return (rawDerivations, foreignSymbols);
    }

    public static IEnumerable<RawScalarConstantDefinition> ParseConstants(INamedTypeSymbol typeSymbol)
    {
        return ScalarConstantParser.Parser.ParseAllOccurrences(typeSymbol);
    }

    public static (IEnumerable<RawConvertibleQuantityDefinition> Definitions, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseConversions(INamedTypeSymbol typeSymbol)
    {
        var symbolicConversions = ConvertibleQuantityParser.Parser.ParseAllOccurrences(typeSymbol);

        var rawConversions = symbolicConversions.Select(static (symbolicConversion) => RawConvertibleQuantityDefinition.FromSymbolic(symbolicConversion));
        var foreignSymbols = symbolicConversions.SelectMany((symbolicConversion) => symbolicConversion.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: true));

        return (rawConversions, foreignSymbols);
    }

    public static IEnumerable<RawIncludeUnitBasesDefinition> ParseIncludeUnitBases(INamedTypeSymbol typeSymbol)
    {
        return IncludeUnitBasesParser.Parser.ParseAllOccurrences(typeSymbol);
    }

    public static IEnumerable<RawExcludeUnitBasesDefinition> ParseExcludeUnitBases(INamedTypeSymbol typeSymbol)
    {
        return ExcludeUnitBasesParser.Parser.ParseAllOccurrences(typeSymbol);
    }

    public static IEnumerable<RawIncludeUnitsDefinition> ParseIncludeUnits(INamedTypeSymbol typeSymbol)
    {
        return IncludeUnitsParser.Parser.ParseAllOccurrences(typeSymbol);
    }

    public static IEnumerable<RawExcludeUnitsDefinition> ParseExcludeUnits(INamedTypeSymbol typeSymbol)
    {
        return ExcludeUnitsParser.Parser.ParseAllOccurrences(typeSymbol);
    }
}

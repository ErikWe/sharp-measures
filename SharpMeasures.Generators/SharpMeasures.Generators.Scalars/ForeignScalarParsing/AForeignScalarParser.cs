namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using System;
using System.Collections.Generic;
using System.Linq;

internal abstract class AForeignScalarParser<TProductType, TProductDefinition>
{
    public (Optional<TProductType> Definition, IEnumerable<INamedTypeSymbol> ReferencedSymbols) Parse(INamedTypeSymbol typeSymbol)
    {
        (var scalar, var scalarReferencedSymbols) = ParseScalar(typeSymbol);

        if (scalar.HasValue is false)
        {
            return (new Optional<TProductType>(), Array.Empty<INamedTypeSymbol>());
        }

        (var derivations, var derivationsReferencedSymbols) = ParseDerivations(typeSymbol);
        var constants = ParseConstants(typeSymbol);
        (var conversions, var conversionsReferencedSymbols) = ParseConversions(typeSymbol);

        var includeUnitInstanceBases = ParseIncludeUnitBases(typeSymbol);
        var excludeUnitInstanceBases = ParseExcludeUnitBases(typeSymbol);

        var includeUnitInstances = ParseIncludeUnits(typeSymbol);
        var excludeUnitInstances = ParseExcludeUnits(typeSymbol);

        TProductType product = ProduceResult(typeSymbol.AsDefinedType(), scalar.Value, derivations, constants, conversions, includeUnitInstanceBases, excludeUnitInstanceBases, includeUnitInstances, excludeUnitInstances);
        var referencedSymbols = scalarReferencedSymbols.Concat(derivationsReferencedSymbols).Concat(conversionsReferencedSymbols);

        return (product, referencedSymbols);
    }

    protected abstract TProductType ProduceResult(DefinedType type, TProductDefinition definition, IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawScalarConstantDefinition> constants, IEnumerable<RawConvertibleQuantityDefinition> conversions,
        IEnumerable<RawIncludeUnitBasesDefinition> baseInclusions, IEnumerable<RawExcludeUnitBasesDefinition> baseExclusions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions);

    protected abstract (Optional<TProductDefinition>, IEnumerable<INamedTypeSymbol>) ParseScalar(INamedTypeSymbol typeSymbol);

    private static (IEnumerable<RawDerivedQuantityDefinition> Definitions, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseDerivations(INamedTypeSymbol typeSymbol)
    {
        var symbolicDerivations = DerivedQuantityParser.Parser.ParseAllOccurrences(typeSymbol);

        var rawDerivations = symbolicDerivations.Select(static (symbolicDerivation) => RawDerivedQuantityDefinition.FromSymbolic(symbolicDerivation));
        var foreignSymbols = symbolicDerivations.SelectMany((symbolicDerivation) => symbolicDerivation.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: true));

        return (rawDerivations, foreignSymbols);
    }

    private static IEnumerable<RawScalarConstantDefinition> ParseConstants(INamedTypeSymbol typeSymbol) => ScalarConstantParser.Parser.ParseAllOccurrences(typeSymbol);
    private static (IEnumerable<RawConvertibleQuantityDefinition> Definitions, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseConversions(INamedTypeSymbol typeSymbol)
    {
        var symbolicConversions = ConvertibleQuantityParser.Parser.ParseAllOccurrences(typeSymbol);

        var rawConversions = symbolicConversions.Select(static (symbolicConversion) => RawConvertibleQuantityDefinition.FromSymbolic(symbolicConversion));
        var foreignSymbols = symbolicConversions.SelectMany((symbolicConversion) => symbolicConversion.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: true));

        return (rawConversions, foreignSymbols);
    }

    private static IEnumerable<RawIncludeUnitBasesDefinition> ParseIncludeUnitBases(INamedTypeSymbol typeSymbol) => IncludeUnitBasesParser.Parser.ParseAllOccurrences(typeSymbol);
    private static IEnumerable<RawExcludeUnitBasesDefinition> ParseExcludeUnitBases(INamedTypeSymbol typeSymbol) => ExcludeUnitBasesParser.Parser.ParseAllOccurrences(typeSymbol);
    private static IEnumerable<RawIncludeUnitsDefinition> ParseIncludeUnits(INamedTypeSymbol typeSymbol) => IncludeUnitsParser.Parser.ParseAllOccurrences(typeSymbol);
    private static IEnumerable<RawExcludeUnitsDefinition> ParseExcludeUnits(INamedTypeSymbol typeSymbol) => ExcludeUnitsParser.Parser.ParseAllOccurrences(typeSymbol);
}

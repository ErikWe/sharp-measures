﻿namespace SharpMeasures.Generators.Scalars.Parsing.Abstraction;

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
using System.Threading;

internal abstract class AScalarParser<TDefinition, TProduct>
{
    public (Optional<TProduct> Definition, IEnumerable<INamedTypeSymbol> ForeignSymbols) Parse(Optional<INamedTypeSymbol> typeSymbol, CancellationToken token)
    {
        if (token.IsCancellationRequested || typeSymbol.HasValue is false)
        {
            return (new Optional<TProduct>(), Array.Empty<INamedTypeSymbol>());
        }

        return Parse(typeSymbol.Value);
    }

    public (Optional<TProduct>, IEnumerable<INamedTypeSymbol>) Parse(INamedTypeSymbol typeSymbol)
    {
        (var scalar, var scalarForeignSymbols) = ParseScalar(typeSymbol);

        if (scalar.HasValue is false)
        {
            return (new Optional<TProduct>(), Array.Empty<INamedTypeSymbol>());
        }

        (var derivations, var derivationsForeignSymbols) = ParseDerivations(typeSymbol);
        var constants = ParseConstants(typeSymbol);
        (var conversions, var conversionsForeignSymbols) = ParseConversions(typeSymbol);

        var includeUnitInstanceBases = ParseIncludeUnitBases(typeSymbol);
        var excludeUnitInstanceBases = ParseExcludeUnitBases(typeSymbol);

        var includeUnitInstances = ParseIncludeUnits(typeSymbol);
        var excludeUnitInstances = ParseExcludeUnits(typeSymbol);

        TProduct product = ProduceResult(typeSymbol.AsDefinedType(), scalar.Value, derivations, constants, conversions, includeUnitInstanceBases, excludeUnitInstanceBases, includeUnitInstances, excludeUnitInstances);
        var foreignSymbols = scalarForeignSymbols.Concat(derivationsForeignSymbols).Concat(conversionsForeignSymbols);

        return (product, foreignSymbols);
    }

    protected abstract TProduct ProduceResult(DefinedType type, TDefinition definition, IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawScalarConstantDefinition> constants, IEnumerable<RawConvertibleQuantityDefinition> conversions,
        IEnumerable<RawIncludeUnitBasesDefinition> baseInclusions, IEnumerable<RawExcludeUnitBasesDefinition> baseExclusions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions);

    protected abstract (Optional<TDefinition>, IEnumerable<INamedTypeSymbol>)  ParseScalar(INamedTypeSymbol typeSymbol);

    private static (IEnumerable<RawDerivedQuantityDefinition>, IEnumerable<INamedTypeSymbol>) ParseDerivations(INamedTypeSymbol typeSymbol)
    {
        var symbolicDerivations = DerivedQuantityParser.Parser.ParseAllOccurrences(typeSymbol);

        var rawDerivations = symbolicDerivations.Select(static (symbolicDerivation) => RawDerivedQuantityDefinition.FromSymbolic(symbolicDerivation));
        var foreignSymbols = symbolicDerivations.SelectMany((symbolicDerivation) => symbolicDerivation.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: false));

        return (rawDerivations, foreignSymbols);
    }

    private static IEnumerable<RawScalarConstantDefinition> ParseConstants(INamedTypeSymbol typeSymbol) => ScalarConstantParser.Parser.ParseAllOccurrences(typeSymbol);
    private static (IEnumerable<RawConvertibleQuantityDefinition> Definitions, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseConversions(INamedTypeSymbol typeSymbol)
    {
        var symbolicConversions = ConvertibleQuantityParser.Parser.ParseAllOccurrences(typeSymbol);

        var rawConversions = symbolicConversions.Select(static (symbolicConversion) => RawConvertibleQuantityDefinition.FromSymbolic(symbolicConversion));
        var foreignSymbols = symbolicConversions.SelectMany((symbolicConversion) => symbolicConversion.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: false));

        return (rawConversions, foreignSymbols);
    }

    private static IEnumerable<RawIncludeUnitBasesDefinition> ParseIncludeUnitBases(INamedTypeSymbol typeSymbol) => IncludeUnitBasesParser.Parser.ParseAllOccurrences(typeSymbol);
    private static IEnumerable<RawExcludeUnitBasesDefinition> ParseExcludeUnitBases(INamedTypeSymbol typeSymbol) => ExcludeUnitBasesParser.Parser.ParseAllOccurrences(typeSymbol);
    private static IEnumerable<RawIncludeUnitsDefinition> ParseIncludeUnits(INamedTypeSymbol typeSymbol) => IncludeUnitsParser.Parser.ParseAllOccurrences(typeSymbol);
    private static IEnumerable<RawExcludeUnitsDefinition> ParseExcludeUnits(INamedTypeSymbol typeSymbol) => ExcludeUnitsParser.Parser.ParseAllOccurrences(typeSymbol);
}

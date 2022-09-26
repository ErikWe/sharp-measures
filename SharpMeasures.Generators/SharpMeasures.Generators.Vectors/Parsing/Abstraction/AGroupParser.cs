namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal abstract class AGroupParser<TDefinition, TProduct>
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
        (var group, var groupForeignSymbols) = ParseGroup(typeSymbol);

        if (group.HasValue is false)
        {
            return (new Optional<TProduct>(), Array.Empty<INamedTypeSymbol>());
        }

        (var derivations, var derivationForeignSymbols) = CommonParsing.ParseDerivations(typeSymbol);
        (var conversions, var conversionForeignSymbols) = CommonParsing.ParseConversions(typeSymbol);

        var includeUnitInstances = CommonParsing.ParseIncludeUnitInstances(typeSymbol);
        var excludeUnitInstances = CommonParsing.ParseExcludeUnitInstances(typeSymbol);

        TProduct product = ProduceResult(typeSymbol.AsDefinedType(), group.Value, derivations, conversions, includeUnitInstances, excludeUnitInstances);
        var foreignSymbols = groupForeignSymbols.Concat(derivationForeignSymbols).Concat(conversionForeignSymbols);

        return (product, foreignSymbols);
    }

    protected abstract TProduct ProduceResult(DefinedType type, TDefinition definition, IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions);

    protected abstract (Optional<TDefinition> Definition, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseGroup(INamedTypeSymbol typeSymbol);
}

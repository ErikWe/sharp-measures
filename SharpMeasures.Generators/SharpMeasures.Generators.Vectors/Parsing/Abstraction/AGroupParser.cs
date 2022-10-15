namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal abstract class AGroupParser<TDefinition, TProduct>
{
    public Optional<(INamedTypeSymbol, TProduct, IEnumerable<INamedTypeSymbol>)> Parse(Optional<INamedTypeSymbol> typeSymbol, CancellationToken token)
    {
        if (token.IsCancellationRequested || typeSymbol.HasValue is false)
        {
            return new Optional<(INamedTypeSymbol, TProduct, IEnumerable<INamedTypeSymbol>)>();
        }

        return Parse(typeSymbol.Value);
    }

    public Optional<(INamedTypeSymbol Symbol, TProduct Definition, IEnumerable<INamedTypeSymbol> ForeignSymbols)> Parse(INamedTypeSymbol typeSymbol)
    {
        (var group, var groupForeignSymbols) = ParseGroup(typeSymbol);

        if (group.HasValue is false)
        {
            return new Optional<(INamedTypeSymbol, TProduct, IEnumerable<INamedTypeSymbol>)>();
        }

        (var operations, var operationForeignSymbols) = CommonParsing.ParseOperations(typeSymbol);
        (var vectorOperations, var vectorOperationForeignSymbols) = CommonParsing.ParseVectorOperations(typeSymbol);
        (var conversions, var conversionForeignSymbols) = CommonParsing.ParseConversions(typeSymbol);

        var includeUnitInstances = CommonParsing.ParseIncludeUnitInstances(typeSymbol);
        var excludeUnitInstances = CommonParsing.ParseExcludeUnitInstances(typeSymbol);

        TProduct product = ProduceResult(typeSymbol.AsDefinedType(), group.Value, operations, vectorOperations, conversions, includeUnitInstances, excludeUnitInstances);
        var foreignSymbols = groupForeignSymbols.Concat(operationForeignSymbols).Concat(vectorOperationForeignSymbols).Concat(conversionForeignSymbols);

        return (typeSymbol, product, foreignSymbols);
    }

    protected abstract TProduct ProduceResult(DefinedType type, TDefinition definition, IEnumerable<RawQuantityOperationDefinition> operations, IEnumerable<RawVectorOperationDefinition> vectorOperations, IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions);

    protected abstract (Optional<TDefinition> Definition, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseGroup(INamedTypeSymbol typeSymbol);
}

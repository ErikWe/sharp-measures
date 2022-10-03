namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

internal abstract class AVectorParser<TDefinition, TProduct>
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
        (var vector, var vectorForeignSymbols) = ParseVector(typeSymbol);

        if (vector.HasValue is false)
        {
            return (new Optional<TProduct>(), Array.Empty<INamedTypeSymbol>());
        }

        (var operations, var operationForeignSymbols) = CommonParsing.ParseOperations(typeSymbol);
        (var vectorOperations, var vectorOperationForeignSymbols) = CommonParsing.ParseVectorOperations(typeSymbol);
        var processes = CommonParsing.ParseProcesses(typeSymbol);
        var constants = CommonParsing.ParseConstants(typeSymbol);
        (var conversions, var conversionForeignSymbols) = CommonParsing.ParseConversions(typeSymbol);

        var includeUnitInstances = CommonParsing.ParseIncludeUnitInstances(typeSymbol);
        var excludeUnitInstances = CommonParsing.ParseExcludeUnitInstances(typeSymbol);

        TProduct product = ProduceResult(typeSymbol.AsDefinedType(), vector.Value, operations, vectorOperations, processes, constants, conversions, includeUnitInstances, excludeUnitInstances);
        var foreignSymbols = vectorForeignSymbols.Concat(operationForeignSymbols).Concat(vectorOperationForeignSymbols).Concat(conversionForeignSymbols);

        return (product, foreignSymbols);
    }

    protected abstract TProduct ProduceResult(DefinedType type, TDefinition definition, IEnumerable<RawQuantityOperationDefinition> operations, IEnumerable<RawVectorOperationDefinition> vectorOperations, IEnumerable<RawQuantityProcessDefinition> processes, IEnumerable<RawVectorConstantDefinition> constants, IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions);

    protected abstract (Optional<TDefinition> Definition, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseVector(INamedTypeSymbol typeSymbol);
}

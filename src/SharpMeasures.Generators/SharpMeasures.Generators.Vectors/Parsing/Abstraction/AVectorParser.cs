namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal abstract class AVectorParser<TDefinition, TProduct>
{
    protected bool AlreadyInForeignAssembly { get; }

    protected AVectorParser(bool alreadyInForeignAssembly)
    {
        AlreadyInForeignAssembly = alreadyInForeignAssembly;
    }

    public Optional<(IEnumerable<AttributeData>, TProduct, IEnumerable<INamedTypeSymbol>)> Parse(Optional<INamedTypeSymbol> typeSymbol, CancellationToken token)
    {
        if (token.IsCancellationRequested || typeSymbol.HasValue is false)
        {
            return new Optional<(IEnumerable<AttributeData>, TProduct, IEnumerable<INamedTypeSymbol>)>();
        }

        return Parse(typeSymbol.Value);
    }

    public Optional<(IEnumerable<AttributeData> Attributes, TProduct Definition, IEnumerable<INamedTypeSymbol> ForeignSymbols)> Parse(INamedTypeSymbol typeSymbol)
    {
        var attributes = typeSymbol.GetAttributes();

        (var vector, var vectorForeignSymbols) = ParseVector(typeSymbol, attributes);

        if (vector.HasValue is false)
        {
            return new Optional<(IEnumerable<AttributeData>, TProduct, IEnumerable<INamedTypeSymbol>)>();
        }

        (var operations, var operationForeignSymbols) = CommonParsing.ParseOperations(typeSymbol, attributes, AlreadyInForeignAssembly);
        (var vectorOperations, var vectorOperationForeignSymbols) = CommonParsing.ParseVectorOperations(typeSymbol, attributes, AlreadyInForeignAssembly);
        var processes = CommonParsing.ParseProcesses(attributes);
        var constants = CommonParsing.ParseConstants(attributes);
        (var conversions, var conversionForeignSymbols) = CommonParsing.ParseConversions(typeSymbol, attributes, AlreadyInForeignAssembly);

        var includeUnitInstances = CommonParsing.ParseIncludeUnitInstances(attributes);
        var excludeUnitInstances = CommonParsing.ParseExcludeUnitInstances(attributes);

        var product = ProduceResult(typeSymbol.AsDefinedType(), vector.Value, operations, vectorOperations, processes, constants, conversions, includeUnitInstances, excludeUnitInstances);
        var foreignSymbols = vectorForeignSymbols.Concat(operationForeignSymbols).Concat(vectorOperationForeignSymbols).Concat(conversionForeignSymbols);

        return (attributes, product, foreignSymbols);
    }

    protected abstract TProduct ProduceResult(DefinedType type, TDefinition definition, IEnumerable<RawQuantityOperationDefinition> operations, IEnumerable<RawVectorOperationDefinition> vectorOperations, IEnumerable<RawQuantityProcessDefinition> processes, IEnumerable<RawVectorConstantDefinition> constants, IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions);

    protected abstract (Optional<TDefinition> Definition, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseVector(INamedTypeSymbol typeSymbol, IEnumerable<AttributeData> attributes);
}

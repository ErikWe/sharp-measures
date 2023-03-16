namespace SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal abstract class AScalarParser<TDefinition, TProduct>
{
    protected bool AlreadyInForeignAssembly { get; }

    protected AScalarParser(bool alreadyInForeignAssembly)
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

        (var scalar, var scalarForeignSymbols) = ParseScalar(typeSymbol, attributes);

        if (scalar.HasValue is false)
        {
            return new Optional<(IEnumerable<AttributeData>, TProduct, IEnumerable<INamedTypeSymbol>)>();
        }

        (var operations, var operationForeignSymbols) = ParseOperations(typeSymbol, attributes);
        var processes = ParseProcesses(attributes);
        var constants = ParseConstants(attributes);
        (var conversions, var conversionsForeignSymbols) = ParseConversions(typeSymbol, attributes);

        var includeUnitInstanceBases = ParseIncludeUnitBases(attributes);
        var excludeUnitInstanceBases = ParseExcludeUnitBases(attributes);

        var includeUnitInstances = ParseIncludeUnits(attributes);
        var excludeUnitInstances = ParseExcludeUnits(attributes);

        var product = ProduceResult(typeSymbol.AsDefinedType(), scalar.Value, operations, processes, constants, conversions, includeUnitInstanceBases, excludeUnitInstanceBases, includeUnitInstances, excludeUnitInstances);
        var foreignSymbols = scalarForeignSymbols.Concat(operationForeignSymbols).Concat(conversionsForeignSymbols);

        return (attributes, product, foreignSymbols);
    }

    protected abstract TProduct ProduceResult(DefinedType type, TDefinition definition, IEnumerable<RawQuantityOperationDefinition> operations, IEnumerable<RawQuantityProcessDefinition> processes, IEnumerable<RawScalarConstantDefinition> constants, IEnumerable<RawConvertibleQuantityDefinition> conversions,
        IEnumerable<RawIncludeUnitBasesDefinition> baseInclusions, IEnumerable<RawExcludeUnitBasesDefinition> baseExclusions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions);

    protected abstract (Optional<TDefinition>, IEnumerable<INamedTypeSymbol>) ParseScalar(INamedTypeSymbol typeSymbol, IEnumerable<AttributeData> attributes);

    private (IEnumerable<RawQuantityOperationDefinition>, IEnumerable<INamedTypeSymbol>) ParseOperations(INamedTypeSymbol typeSymbol, IEnumerable<AttributeData> attributes)
    {
        var symbolicOperations = QuantityOperationParser.Parser.ParseAllOccurrences(attributes);

        var rawOperations = symbolicOperations.Select(static (symbolicOperation) => RawQuantityOperationDefinition.FromSymbolic(symbolicOperation));
        var foreignSymbols = symbolicOperations.SelectMany((symbolicOperation) => symbolicOperation.ForeignSymbols(typeSymbol.ContainingAssembly.Name, AlreadyInForeignAssembly));

        return (rawOperations, foreignSymbols);
    }

    private static IEnumerable<RawQuantityProcessDefinition> ParseProcesses(IEnumerable<AttributeData> attributes) => QuantityProcessParser.Parser.ParseAllOccurrences(attributes);
    private static IEnumerable<RawScalarConstantDefinition> ParseConstants(IEnumerable<AttributeData> attributes) => ScalarConstantParser.Parser.ParseAllOccurrences(attributes);
    private (IEnumerable<RawConvertibleQuantityDefinition> Definitions, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseConversions(INamedTypeSymbol typeSymbol, IEnumerable<AttributeData> attributes)
    {
        var symbolicConversions = ConvertibleQuantityParser.Parser.ParseAllOccurrences(attributes);

        var rawConversions = symbolicConversions.Select(static (symbolicConversion) => RawConvertibleQuantityDefinition.FromSymbolic(symbolicConversion));
        var foreignSymbols = symbolicConversions.SelectMany((symbolicConversion) => symbolicConversion.ForeignSymbols(typeSymbol.ContainingAssembly.Name, AlreadyInForeignAssembly));

        return (rawConversions, foreignSymbols);
    }

    private static IEnumerable<RawIncludeUnitBasesDefinition> ParseIncludeUnitBases(IEnumerable<AttributeData> attributes) => IncludeUnitBasesParser.Parser.ParseAllOccurrences(attributes);
    private static IEnumerable<RawExcludeUnitBasesDefinition> ParseExcludeUnitBases(IEnumerable<AttributeData> attributes) => ExcludeUnitBasesParser.Parser.ParseAllOccurrences(attributes);
    private static IEnumerable<RawIncludeUnitsDefinition> ParseIncludeUnits(IEnumerable<AttributeData> attributes) => IncludeUnitsParser.Parser.ParseAllOccurrences(attributes);
    private static IEnumerable<RawExcludeUnitsDefinition> ParseExcludeUnits(IEnumerable<AttributeData> attributes) => ExcludeUnitsParser.Parser.ParseAllOccurrences(attributes);
}

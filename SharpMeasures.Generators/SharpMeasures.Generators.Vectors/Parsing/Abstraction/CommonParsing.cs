namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;
using System.Linq;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

internal static class CommonParsing
{
    public static (IEnumerable<RawQuantityOperationDefinition> Definitions, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseOperations(INamedTypeSymbol typeSymbol)
    {
        var symbolicOperations = QuantityOperationParser.Parser.ParseAllOccurrences(typeSymbol);

        var rawOperations = symbolicOperations.Select(static (symbolicOperation) => RawQuantityOperationDefinition.FromSymbolic(symbolicOperation));
        var foreignSymbols = symbolicOperations.SelectMany((symbolicOperation) => symbolicOperation.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: false));

        return (rawOperations, foreignSymbols);
    }

    public static (IEnumerable<RawVectorOperationDefinition> Definitions, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseVectorOperations(INamedTypeSymbol typeSymbol)
    {
        var symbolicOperations = VectorOperationParser.Parser.ParseAllOccurrences(typeSymbol);

        var rawOperations = symbolicOperations.Select(static (symbolicOperation) => RawVectorOperationDefinition.FromSymbolic(symbolicOperation));
        var foreignSymbols = symbolicOperations.SelectMany((symbolicOperation) => symbolicOperation.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: false));

        return (rawOperations, foreignSymbols);
    }

    public static IEnumerable<RawQuantityProcessDefinition> ParseProcesses(INamedTypeSymbol typeSymbol) => QuantityProcessParser.Parser.ParseAllOccurrences(typeSymbol);
    public static IEnumerable<RawVectorConstantDefinition> ParseConstants(INamedTypeSymbol typeSymbol) => VectorConstantParser.Parser.ParseAllOccurrences(typeSymbol);
    public static (IEnumerable<RawConvertibleQuantityDefinition> Definitions, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseConversions(INamedTypeSymbol typeSymbol)
    {
        var symbolicConversions = ConvertibleQuantityParser.Parser.ParseAllOccurrences(typeSymbol);

        var rawConversions = symbolicConversions.Select(static (symbolicConversion) => RawConvertibleQuantityDefinition.FromSymbolic(symbolicConversion));
        var foreignSymbols = symbolicConversions.SelectMany((symbolicConversion) => symbolicConversion.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: false));

        return (rawConversions, foreignSymbols);
    }

    public static IEnumerable<RawIncludeUnitsDefinition> ParseIncludeUnitInstances(INamedTypeSymbol typeSymbol) => IncludeUnitsParser.Parser.ParseAllOccurrences(typeSymbol);
    public static IEnumerable<RawExcludeUnitsDefinition> ParseExcludeUnitInstances(INamedTypeSymbol typeSymbol) => ExcludeUnitsParser.Parser.ParseAllOccurrences(typeSymbol);
}

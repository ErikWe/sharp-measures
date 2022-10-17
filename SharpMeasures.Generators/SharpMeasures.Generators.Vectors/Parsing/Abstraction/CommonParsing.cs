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

internal static class CommonParsing
{
    public static (IEnumerable<RawQuantityOperationDefinition> Definitions, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseOperations(INamedTypeSymbol typeSymbol, IEnumerable<AttributeData> attributes, bool alreadyInForeignAssembly)
    {
        var symbolicOperations = QuantityOperationParser.Parser.ParseAllOccurrences(attributes);

        var rawOperations = symbolicOperations.Select(static (symbolicOperation) => RawQuantityOperationDefinition.FromSymbolic(symbolicOperation));
        var foreignSymbols = symbolicOperations.SelectMany((symbolicOperation) => symbolicOperation.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly));

        return (rawOperations, foreignSymbols);
    }

    public static (IEnumerable<RawVectorOperationDefinition> Definitions, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseVectorOperations(INamedTypeSymbol typeSymbol, IEnumerable<AttributeData> attributes, bool alreadyInForeignAssembly)
    {
        var symbolicOperations = VectorOperationParser.Parser.ParseAllOccurrences(attributes);

        var rawOperations = symbolicOperations.Select(static (symbolicOperation) => RawVectorOperationDefinition.FromSymbolic(symbolicOperation));
        var foreignSymbols = symbolicOperations.SelectMany((symbolicOperation) => symbolicOperation.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly));

        return (rawOperations, foreignSymbols);
    }

    public static IEnumerable<RawQuantityProcessDefinition> ParseProcesses(IEnumerable<AttributeData> attributes) => QuantityProcessParser.Parser.ParseAllOccurrences(attributes);
    public static IEnumerable<RawVectorConstantDefinition> ParseConstants(IEnumerable<AttributeData> attributes) => VectorConstantParser.Parser.ParseAllOccurrences(attributes);
    public static (IEnumerable<RawConvertibleQuantityDefinition> Definitions, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseConversions(INamedTypeSymbol typeSymbol, IEnumerable<AttributeData> attributes, bool alreadyInForeignAssembly)
    {
        var symbolicConversions = ConvertibleQuantityParser.Parser.ParseAllOccurrences(attributes);

        var rawConversions = symbolicConversions.Select(static (symbolicConversion) => RawConvertibleQuantityDefinition.FromSymbolic(symbolicConversion));
        var foreignSymbols = symbolicConversions.SelectMany((symbolicConversion) => symbolicConversion.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly));

        return (rawConversions, foreignSymbols);
    }

    public static IEnumerable<RawIncludeUnitsDefinition> ParseIncludeUnitInstances(IEnumerable<AttributeData> attributes) => IncludeUnitsParser.Parser.ParseAllOccurrences(attributes);
    public static IEnumerable<RawExcludeUnitsDefinition> ParseExcludeUnitInstances(IEnumerable<AttributeData> attributes) => ExcludeUnitsParser.Parser.ParseAllOccurrences(attributes);
}

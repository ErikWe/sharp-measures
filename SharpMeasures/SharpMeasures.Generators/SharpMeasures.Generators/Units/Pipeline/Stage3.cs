namespace SharpMeasures.Generators.Units.Pipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Units.Extraction;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

internal static class Stage3
{
    public readonly record struct Result(IEnumerable<DocumentationFile> Documentation, INamedTypeSymbol TypeSymbol, NamedType Quantity, bool Biased);

    public static IncrementalValuesProvider<Result> Perform(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<Stage2.Result> provider)
    {
        IncrementalValuesProvider<ResultWithDiagnostics<Result?>> resultsWithDiagnostics
            = TypeSymbolProvider.Attach(provider, context.CompilationProvider, ExtractDeclaration, ExtractUnitsAndDiagnostics);

        IncrementalValuesProvider<Result> validResults = resultsWithDiagnostics.ExtractResult().WhereNotNull();

        context.ReportDiagnostics(resultsWithDiagnostics);
        return validResults;
    }

    private static TypeDeclarationSyntax ExtractDeclaration(Stage2.Result input) => input.Declaration;
    private static ResultWithDiagnostics<Result?> ExtractUnitsAndDiagnostics(Stage2.Result input, INamedTypeSymbol symbol)
    {
        AExtractor<GeneratedUnitParameters> units = GeneratedUnitExtractor.Extract(symbol);

        if (units.ValidDefinitions.Count is 0
            || units.ValidDefinitions[0] is not GeneratedUnitParameters { Quantity: INamedTypeSymbol quantity } parameters)
        {
            return new(null, units.Diagnostics);
        }

        Result result = new(input.Documentation, symbol, NamedType.FromSymbol(quantity), parameters.Biased);

        return new ResultWithDiagnostics<Result?>(result, units.Diagnostics);
    }
}

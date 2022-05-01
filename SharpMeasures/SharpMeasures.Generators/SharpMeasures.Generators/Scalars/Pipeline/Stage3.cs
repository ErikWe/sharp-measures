namespace SharpMeasures.Generators.Scalars.Pipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Scalars.Extraction;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

internal static class Stage3
{
    public readonly record struct Result(IEnumerable<DocumentationFile> Documentation, INamedTypeSymbol TypeSymbol, INamedTypeSymbol UnitSymbol,
        bool Biased);

    public static IncrementalValuesProvider<Result> Perform(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<Stage2.Result> provider)
    {
        IncrementalValuesProvider<ResultWithDiagnostics<Result?>> resultsWithDiagnostics
            = TypeSymbolProvider.Attach(provider, context.CompilationProvider, ExtractDeclaration, ExtractQuantityAndDiagnostics);

        IncrementalValuesProvider<Result> validResults = resultsWithDiagnostics.ExtractResult().WhereNotNull();

        context.ReportDiagnostics(resultsWithDiagnostics);
        return validResults;
    }

    private static TypeDeclarationSyntax ExtractDeclaration(Stage2.Result input) => input.Declaration;
    private static ResultWithDiagnostics<Result?> ExtractQuantityAndDiagnostics(Stage2.Result input, INamedTypeSymbol symbol)
    {
        AExtractor<GeneratedScalarQuantityParameters> quantities = GeneratedScalarQuantityExtractor.Extract(symbol);

        if (quantities.ValidDefinitions.Count is 0
            || quantities.ValidDefinitions[0] is not GeneratedScalarQuantityParameters { Unit: INamedTypeSymbol unit } parameters)
        {
            return new ResultWithDiagnostics<Result?>(null, quantities.Diagnostics);
        }

        Result result = new(input.Documentation, symbol, unit, parameters.Biased);

        return new ResultWithDiagnostics<Result?>(result, quantities.Diagnostics);
    }
}

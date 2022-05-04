namespace SharpMeasures.Generators.Units.Pipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Units.Extraction;
using SharpMeasures.Generators.Utility;

internal static class Stage3
{
    public readonly record struct Result(DocumentationFile Documentation, INamedTypeSymbol TypeSymbol, NamedType Quantity, bool Biased);

    public static IncrementalValuesProvider<Result> Attach(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<Stage2.Result> inputProvider)
    {
        var resultsWithDiagnostics
            = DeclarationSymbolProvider.AttachToValueType(inputProvider, context.CompilationProvider, ExtractDeclaration, ExtractUnitsAndDiagnostics);

        context.ReportDiagnostics(resultsWithDiagnostics);
        return resultsWithDiagnostics.ExtractResult().WhereNotNull();
    }

    private static TypeDeclarationSyntax ExtractDeclaration(Stage2.Result input) => input.Declaration;
    private static ResultWithDiagnostics<Result?> ExtractUnitsAndDiagnostics(Stage2.Result input, INamedTypeSymbol symbol)
    {
        AExtractor<GeneratedUnitParameters> units = GeneratedUnitExtractor.Extract(symbol);

        if (units.ValidDefinitions.Count is 0
            || units.ValidDefinitions[0] is not GeneratedUnitParameters { Quantity: INamedTypeSymbol quantity } parameters)
        {
            return new ResultWithDiagnostics<Result?>(null, units.Diagnostics);
        }

        Result result = new(input.Documentation, symbol, NamedType.FromSymbol(quantity), parameters.AllowBias);

        return new ResultWithDiagnostics<Result?>(result, units.Diagnostics);
    }
}

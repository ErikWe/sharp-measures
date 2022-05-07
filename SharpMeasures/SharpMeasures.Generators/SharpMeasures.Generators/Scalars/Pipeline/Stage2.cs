namespace SharpMeasures.Generators.Scalars.Pipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Scalars.Extraction;
using SharpMeasures.Generators.Utility;

internal static class Stage2
{
    public readonly record struct Result(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol, INamedTypeSymbol UnitSymbol, bool Biased,
        GenerateDocumentationState GenerateDocumentation);

    public static IncrementalValuesProvider<Result> Attach(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TypeDeclarationSyntax> inputProvider)
    {
        var resultsWithDiagnostics
            = DeclarationSymbolProvider.AttachToValueType(inputProvider, context.CompilationProvider, ExtractDeclaration, ExtractQuantityAndDiagnostics);

        context.ReportDiagnostics(resultsWithDiagnostics);
        return resultsWithDiagnostics.ExtractResult().WhereNotNull();
    }

    private static TypeDeclarationSyntax ExtractDeclaration(TypeDeclarationSyntax declaration) => declaration;
    private static ResultWithDiagnostics<Result?> ExtractQuantityAndDiagnostics(TypeDeclarationSyntax declaration, INamedTypeSymbol symbol)
    {
        AExtractor<GeneratedScalarQuantityParameters> quantities = GeneratedScalarQuantityExtractor.Extract(symbol);

        if (quantities.ValidDefinitions.Count is 0
            || quantities.ValidDefinitions[0] is not GeneratedScalarQuantityParameters { Unit: INamedTypeSymbol unit } parameters)
        {
            return new ResultWithDiagnostics<Result?>(null, quantities.Diagnostics);
        }

        GenerateDocumentationState generateDocumentation = (parameters.ParsingData.ExplicitGenerateDocumentation, parameters.GenerateDocumentation) switch
        {
            (false, _) => GenerateDocumentationState.Default,
            (true, true) => GenerateDocumentationState.ExplicitlyEnabled,
            (true, false) => GenerateDocumentationState.ExplicitlyDisabled
        };

        Result result = new(declaration, symbol, unit, parameters.Biased, generateDocumentation);

        return new ResultWithDiagnostics<Result?>(result, quantities.Diagnostics);
    }
}

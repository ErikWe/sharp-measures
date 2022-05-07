namespace SharpMeasures.Generators.Units.Pipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Units.Extraction;
using SharpMeasures.Generators.Utility;

internal static class Stage2
{
    public readonly record struct Result(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol, NamedType Quantity, bool Biased,
        GenerateDocumentationState GenerateDocumentation);

    public static IncrementalValuesProvider<Result> Attach(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TypeDeclarationSyntax> inputProvider)
    {
        var resultsWithDiagnostics
            = DeclarationSymbolProvider.AttachToValueType(inputProvider, context.CompilationProvider, ExtractDeclaration, ExtractUnitsAndDiagnostics);

        context.ReportDiagnostics(resultsWithDiagnostics);
        return resultsWithDiagnostics.ExtractResult().WhereNotNull();
    }

    private static TypeDeclarationSyntax ExtractDeclaration(TypeDeclarationSyntax declaration) => declaration;
    private static ResultWithDiagnostics<Result?> ExtractUnitsAndDiagnostics(TypeDeclarationSyntax declaration, INamedTypeSymbol symbol)
    {
        AExtractor<GeneratedUnitParameters> units = GeneratedUnitExtractor.Extract(symbol);

        if (units.ValidDefinitions.Count is 0
            || units.ValidDefinitions[0] is not GeneratedUnitParameters { Quantity: INamedTypeSymbol quantity } parameters)
        {
            return new ResultWithDiagnostics<Result?>(null, units.Diagnostics);
        }

        GenerateDocumentationState generateDocumentation = (parameters.ParsingData.ExplicitGenerateDocumentation, parameters.GenerateDocumentation) switch
        {
            (false, _) => GenerateDocumentationState.Default,
            (true, true) => GenerateDocumentationState.ExplicitlyEnabled,
            (true, false) => GenerateDocumentationState.ExplicitlyDisabled
        };

        Result result = new(declaration, symbol, NamedType.FromSymbol(quantity), parameters.AllowBias, generateDocumentation);

        return new ResultWithDiagnostics<Result?>(result, units.Diagnostics);
    }
}

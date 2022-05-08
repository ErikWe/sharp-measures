namespace SharpMeasures.Generators.Scalars.Pipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars.Extraction;
using SharpMeasures.Generators.Utility;

using System.Threading;

internal static class Stage2
{
    public readonly record struct Result(SharpMeasuresGenerator.DeclarationData Declaration, INamedTypeSymbol TypeSymbol, INamedTypeSymbol UnitSymbol, bool Biased,
        GenerateDocumentationState GenerateDocumentation);

    public static IncrementalValuesProvider<Result> ParseParameters(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<SharpMeasuresGenerator.Result> declarationAndSymbolProvider)
    {
        var parametersAndDiagnostics = declarationAndSymbolProvider.Select(ExtractParametersAndDiagnostics);

        context.ReportDiagnostics(parametersAndDiagnostics);
        return parametersAndDiagnostics.ExtractResult().WhereNotNull();
    }

    private static ResultWithDiagnostics<Result?> ExtractParametersAndDiagnostics(SharpMeasuresGenerator.Result declarationAndSymbol, CancellationToken _)
    {
        AExtractor<GeneratedScalarQuantityParameters> quantities = GeneratedScalarQuantityExtractor.Extract(declarationAndSymbol.TypeSymbol);

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

        Result result = new(declarationAndSymbol.Declaration, declarationAndSymbol.TypeSymbol, unit, parameters.Biased, generateDocumentation);

        return new ResultWithDiagnostics<Result?>(result, quantities.Diagnostics);
    }

    private readonly record struct IntermediateResult(SharpMeasuresGenerator.DeclarationData Declaration, INamedTypeSymbol TypeSymbol);
}

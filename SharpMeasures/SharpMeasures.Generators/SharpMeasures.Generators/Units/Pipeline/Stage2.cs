namespace SharpMeasures.Generators.Units.Pipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Units.Extraction;
using SharpMeasures.Generators.Utility;

using System.Threading;

internal static class Stage2
{
    public readonly record struct Result(SharpMeasuresGenerator.DeclarationData Declaration, INamedTypeSymbol TypeSymbol, INamedTypeSymbol QuantitySymbol,
        bool Biased, GenerateDocumentationState GenerateDocumentation);

    public static IncrementalValuesProvider<Result> ParseParameters(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<SharpMeasuresGenerator.Result> declarationAndSymbolProvider)
    {
        var parametersAndDiagnostics = declarationAndSymbolProvider.Select(ExtractParametersAndDiagnostics);

        context.ReportDiagnostics(parametersAndDiagnostics);
        return parametersAndDiagnostics.ExtractResult().WhereNotNull();
    }

    private static ResultWithDiagnostics<Result?> ExtractParametersAndDiagnostics(SharpMeasuresGenerator.Result input, CancellationToken _)
    {
        AExtractor<GeneratedUnitParameters> units = GeneratedUnitExtractor.Extract(input.TypeSymbol);

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

        Result result = new(input.Declaration, input.TypeSymbol, quantity, parameters.AllowBias, generateDocumentation);

        return new ResultWithDiagnostics<Result?>(result, units.Diagnostics);
    }
}

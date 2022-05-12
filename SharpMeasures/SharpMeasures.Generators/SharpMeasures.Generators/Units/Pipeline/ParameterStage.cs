namespace SharpMeasures.Generators.Units.Pipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Units.Extraction;
using SharpMeasures.Generators.Utility;

using System.Threading;

internal static class ParameterStage
{
    public readonly record struct Result(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol, GeneratedUnitDefinition Definition);

    public static IncrementalValuesProvider<Result> ParseGeneratedUnitParameters(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<DeclarationStage.Result> declarationAndSymbolProvider)
    {
        var parametersAndDiagnostics = declarationAndSymbolProvider.Select(ExtractParametersAndDiagnostics);

        context.ReportDiagnostics(parametersAndDiagnostics);
        return parametersAndDiagnostics.ExtractResult().WhereNotNull();
    }

    private static ResultWithDiagnostics<Result?> ExtractParametersAndDiagnostics(DeclarationStage.Result input, CancellationToken _)
    {
        AExtractor<GeneratedUnitDefinition> units = GeneratedUnitExtractor.Extract(input.TypeSymbol);

        if (units.ValidDefinitions.Count is 0
            || units.ValidDefinitions[0] is not GeneratedUnitDefinition { Quantity: INamedTypeSymbol } parameters)
        {
            return new ResultWithDiagnostics<Result?>(null, units.Diagnostics);
        }

        Result result = new(input.Declaration, input.TypeSymbol, parameters);

        return new ResultWithDiagnostics<Result?>(result, units.Diagnostics);
    }
}

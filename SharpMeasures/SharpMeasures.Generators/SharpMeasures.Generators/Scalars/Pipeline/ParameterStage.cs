namespace SharpMeasures.Generators.Scalars.Pipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Scalars.Extraction;
using SharpMeasures.Generators.Utility;

using System.Threading;

internal static class ParameterStage
{
    public readonly record struct Result(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol, GeneratedScalarDefinition Definition);

    public static IncrementalValuesProvider<Result> ParseGeneratedUnitParameters(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<DeclarationStage.Result> declarationAndSymbolProvider)
    {
        var parametersAndDiagnostics = declarationAndSymbolProvider.Select(ExtractParametersAndDiagnostics);

        context.ReportDiagnostics(parametersAndDiagnostics);
        return parametersAndDiagnostics.ExtractResult().WhereNotNull();
    }

    private static ResultWithDiagnostics<Result?> ExtractParametersAndDiagnostics(DeclarationStage.Result input, CancellationToken _)
    {
        AAttributeParser<GeneratedScalarDefinition> units = GeneratedScalarExtractor.Extract(input.TypeSymbol);

        if (units.Definitions.Count is 0 || units.Definitions[0] is not GeneratedScalarDefinition definition)
        {
            return new ResultWithDiagnostics<Result?>(null, units.Diagnostics);
        }

        Result result = new(input.Declaration, input.TypeSymbol, definition);

        return new ResultWithDiagnostics<Result?>(result, units.Diagnostics);
    }
}

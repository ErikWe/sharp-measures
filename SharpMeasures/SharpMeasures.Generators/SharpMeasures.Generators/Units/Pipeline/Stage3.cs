namespace SharpMeasures.Generators.Units.Pipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

internal static class Stage3
{
    public readonly record struct Result(INamedTypeSymbol TypeSymbol, NamedType Quantity, bool Biased, DocumentationFile Documentation);

    public static IncrementalValuesProvider<Result> Attach(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<Stage2.Result> inputProvider)
    {
        return Documentation.DocumentationProvider.Attach(context, inputProvider, ExtractData, ConstructResult);
    }

    private static Documentation.DocumentationProvider.InputData ExtractData(Stage2.Result input) => new(input.Declaration, input.GenerateDocumentation);

    private static Result ConstructResult(Stage2.Result input, DocumentationFile documentation)
        => new(input.TypeSymbol, input.Quantity, input.Biased, documentation);
}
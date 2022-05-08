namespace SharpMeasures.Generators.Units.Pipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Documentation;

internal static class Stage3
{
    public readonly record struct Result(SharpMeasuresGenerator.DeclarationData Declaration, INamedTypeSymbol TypeSymbol,
        INamedTypeSymbol QuantitySymbol, bool Biased, DocumentationFile Documentation);

    public static IncrementalValuesProvider<Result> AttachDocumentation(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<Stage2.Result> inputProvider)
    {
        return Documentation.DocumentationProvider.Attach(context, inputProvider, ExtractData, ConstructResult);
    }

    private static Documentation.DocumentationProvider.InputData ExtractData(Stage2.Result input)
        => new(input.Declaration.TypeDeclaration, input.GenerateDocumentation);

    private static Result ConstructResult(Stage2.Result input, DocumentationFile documentation)
        => new(input.Declaration, input.TypeSymbol, input.QuantitySymbol, input.Biased, documentation);
}
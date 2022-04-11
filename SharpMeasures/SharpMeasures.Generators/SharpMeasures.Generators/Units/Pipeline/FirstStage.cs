namespace SharpMeasures.Generators.Units.Pipeline;

using SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;

internal static class FirstStage
{
    public readonly record struct Result(MarkedDeclarationSyntaxProvider.OutputData Declaration);

    public static IncrementalValuesProvider<Result> Perform(IncrementalGeneratorInitializationContext context)
        => MarkedDeclarationSyntaxProvider.Attach(context.SyntaxProvider, OutputTransform,
            new[] { typeof(GeneratedUnitAttribute), typeof(GeneratedBiasedUnitAttribute) });

    private static Result OutputTransform(MarkedDeclarationSyntaxProvider.OutputData declaration) => new(declaration);
}

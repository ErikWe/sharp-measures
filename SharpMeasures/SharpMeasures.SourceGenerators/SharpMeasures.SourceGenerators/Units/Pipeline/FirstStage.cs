namespace SharpMeasures.SourceGenerators.Units.Pipeline;

using SharpMeasures.Attributes;
using SharpMeasures.SourceGenerators.Providers;

using Microsoft.CodeAnalysis;

internal static class FirstStage
{
    public readonly record struct Result(MarkedDeclarationSyntaxProvider.OutputData Declaration);

    public static IncrementalValuesProvider<Result> Perform(IncrementalGeneratorInitializationContext context)
        => MarkedDeclarationSyntaxProvider.Attach(context.SyntaxProvider, OutputTransform, new[] { typeof(UnitAttribute), typeof(BiasedUnitAttribute) });

    private static Result OutputTransform(MarkedDeclarationSyntaxProvider.OutputData declaration) => new(declaration);
}

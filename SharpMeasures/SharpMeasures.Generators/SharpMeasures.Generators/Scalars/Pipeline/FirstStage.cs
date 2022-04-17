namespace SharpMeasures.Generators.Scalars.Pipeline;

using SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class FirstStage
{
    public readonly record struct Result(TypeDeclarationSyntax Declaration);

    public static IncrementalValuesProvider<Result> Perform(IncrementalGeneratorInitializationContext context)
        => MarkedDeclarationSyntaxProvider.Attach<GeneratedScalarQuantityAttribute, Result>(context.SyntaxProvider, OutputTransform);

    private static Result OutputTransform(TypeDeclarationSyntax declaration) => new(declaration);
}

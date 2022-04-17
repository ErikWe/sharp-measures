namespace SharpMeasures.Generators.Units.Pipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Providers;

internal static class Stage1
{
    public readonly record struct Result(TypeDeclarationSyntax Declaration);
    
    public static IncrementalValuesProvider<Result> Perform(IncrementalGeneratorInitializationContext context)
        => MarkedDeclarationSyntaxProvider.Attach<GeneratedUnitAttribute, Result>(context.SyntaxProvider, OutputTransform);

    private static Result OutputTransform(TypeDeclarationSyntax declaration) => new(declaration);
}

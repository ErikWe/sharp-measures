namespace SharpMeasures.Generators.Scalars.Pipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Documentation;

internal static class Stage2
{
    public readonly record struct Result(TypeDeclarationSyntax Declaration, DocumentationFile Documentation);

    public static IncrementalValuesProvider<Result> Attach(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<TypeDeclarationSyntax> inputProvider)
    {
        return Documentation.DocumentationProvider.Attach(context, inputProvider, ConstructResult);
    }

    private static Result ConstructResult(TypeDeclarationSyntax declaratiom, DocumentationFile documentation) => new(declaratiom, documentation);
}
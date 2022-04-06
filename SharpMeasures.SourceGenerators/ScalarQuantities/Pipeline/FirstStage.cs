namespace ErikWe.SharpMeasures.SourceGenerators.ScalarQuantities.Pipeline;

using ErikWe.SharpMeasures.Attributes;
using ErikWe.SharpMeasures.SourceGenerators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class FirstStage
{
    public readonly record struct Result(TypeDeclarationSyntax TypeDeclaration, AttributeSyntax Attribute);

    public static IncrementalValuesProvider<Result> Perform(IncrementalGeneratorInitializationContext context)
        => MarkedDeclarationSyntaxProvider.Attach<ScalarQuantityAttribute, Result>(context.SyntaxProvider, OutputTransform);

    private static Result OutputTransform(TypeDeclarationSyntax typeDeclaration, AttributeSyntax attribute) => new(typeDeclaration, attribute);
}

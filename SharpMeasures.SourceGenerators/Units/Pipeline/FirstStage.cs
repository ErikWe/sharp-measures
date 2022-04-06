namespace ErikWe.SharpMeasures.SourceGenerators.Units.Pipeline;

using ErikWe.SharpMeasures.Attributes;
using ErikWe.SharpMeasures.SourceGenerators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;

internal static class FirstStage
{
    public readonly record struct Result(TypeDeclarationSyntax TypeDeclaration, AttributeSyntax Attribute);

    public static IncrementalValuesProvider<Result> Perform(IncrementalGeneratorInitializationContext context)
        => MarkedDeclarationSyntaxProvider.Attach(context.SyntaxProvider, OutputTransform, new Type[] { typeof(UnitAttribute), typeof(BiasedUnitAttribute) });

    private static Result OutputTransform(TypeDeclarationSyntax typeDeclaration, AttributeSyntax attribute) => new(typeDeclaration, attribute);
}

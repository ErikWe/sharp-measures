namespace ErikWe.SharpMeasures.SourceGenerators.ScalarQuantities.Pipeline;

using ErikWe.SharpMeasures.SourceGenerators.Documentation;
using ErikWe.SharpMeasures.SourceGenerators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;

internal static class ThirdStage
{
    public readonly record struct Result(TypeDeclarationSyntax TypeDeclaration, AttributeSyntax Attribute, INamedTypeSymbol TypeSymbol, IEnumerable<DocumentationFile> Documentation);

    public static IncrementalValuesProvider<Result> Perform(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<SecondStage.Result> provider)
        => TypeSymbolProvider.Attach(provider, context.CompilationProvider, InputTransform, OutputTransform)
            .WhereNotNull();

    private static TypeDeclarationSyntax InputTransform(SecondStage.Result input) => input.TypeDeclaration;
    private static Result? OutputTransform(SecondStage.Result input, INamedTypeSymbol? symbol)
        => symbol is not null ? new(input.TypeDeclaration, input.Attribute, symbol, input.Documentation) : null;
}

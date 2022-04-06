namespace ErikWe.SharpMeasures.SourceGenerators.ScalarQuantities.Pipeline;

using ErikWe.SharpMeasures.SourceGenerators.Documentation;
using ErikWe.SharpMeasures.SourceGenerators.Providers;
using ErikWe.SharpMeasures.SourceGenerators.Providers.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;

internal static class SecondStage
{
    public readonly record struct Result(MarkedDeclarationSyntaxProvider.OutputData Declaration, IEnumerable<DocumentationFile> Documentation);

    public static IncrementalValuesProvider<Result> Perform(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<FirstStage.Result> firstStage)
        => DocumentationDependenciesProvider.AttachWithFilterStage(context.AdditionalTextsProvider, firstStage,
            InputTransform, OutputTransform, "Scalars");

    private static TypeDeclarationSyntax InputTransform(FirstStage.Result input) => input.Declaration.TypeDeclaration;
    private static Result OutputTransform(FirstStage.Result input, IEnumerable<DocumentationFile> documentation) => new(input.Declaration, documentation);
}

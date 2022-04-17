namespace SharpMeasures.Generators.Scalars.Pipeline;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Providers.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;

internal static class SecondStage
{
    public readonly record struct Result(TypeDeclarationSyntax Declaration, IEnumerable<DocumentationFile> Documentation);

    public static IncrementalValuesProvider<Result> Perform(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<FirstStage.Result> firstStage)
        => DocumentationDependenciesProvider.AttachWithFilterStage(context.AdditionalTextsProvider, firstStage,
            InputTransform, OutputTransform, "Scalars");

    private static TypeDeclarationSyntax InputTransform(FirstStage.Result input) => input.Declaration;
    private static Result OutputTransform(FirstStage.Result input, IEnumerable<DocumentationFile> documentation) => new(input.Declaration, documentation);
}
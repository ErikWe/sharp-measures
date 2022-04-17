namespace SharpMeasures.Generators.Units.Pipeline;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Providers.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;

internal static class Stage2
{
    public readonly record struct Result(TypeDeclarationSyntax Declaration, IEnumerable<DocumentationFile> Documentation);

    public static IncrementalValuesProvider<Result> Perform(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<Stage1.Result> firstStage)
        => DocumentationDependenciesProvider.AttachWithFilterStage(context.AdditionalTextsProvider, firstStage,
            InputTransform, OutputTransform, "Units");

    private static TypeDeclarationSyntax InputTransform(Stage1.Result input) => input.Declaration;
    private static Result OutputTransform(Stage1.Result input, IEnumerable<DocumentationFile> documentation) => new(input.Declaration, documentation);
}
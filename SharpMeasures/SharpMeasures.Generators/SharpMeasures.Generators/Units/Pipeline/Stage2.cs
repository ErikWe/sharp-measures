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
        IncrementalValuesProvider<TypeDeclarationSyntax> firstStage)
        => DocumentationDependenciesProvider.AttachWithFilterStage(context.AdditionalTextsProvider, firstStage, ConstructResult, "Units");

    private static Result ConstructResult(TypeDeclarationSyntax declaration, IEnumerable<DocumentationFile> documentation) => new(declaration, documentation);
}
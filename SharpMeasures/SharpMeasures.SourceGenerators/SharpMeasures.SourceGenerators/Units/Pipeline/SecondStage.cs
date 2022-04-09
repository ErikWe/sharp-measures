﻿namespace SharpMeasures.SourceGeneration.Units.Pipeline;

using SharpMeasures.SourceGeneration.Documentation;
using SharpMeasures.SourceGeneration.Providers;
using SharpMeasures.SourceGeneration.Providers.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;

internal static class SecondStage
{
    public readonly record struct Result(MarkedDeclarationSyntaxProvider.OutputData Declaration, IEnumerable<DocumentationFile> Documentation);

    public static IncrementalValuesProvider<Result> Perform(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<FirstStage.Result> firstStage)
        => DocumentationDependenciesProvider.AttachWithFilterStage(context.AdditionalTextsProvider, firstStage,
            InputTransform, OutputTransform, "Units");

    private static TypeDeclarationSyntax InputTransform(FirstStage.Result input) => input.Declaration.Type;
    private static Result OutputTransform(FirstStage.Result input, IEnumerable<DocumentationFile> documentation) => new(input.Declaration, documentation);
}

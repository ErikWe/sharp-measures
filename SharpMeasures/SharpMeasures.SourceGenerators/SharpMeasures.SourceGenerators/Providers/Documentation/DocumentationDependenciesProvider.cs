namespace SharpMeasures.SourceGenerators.Providers.Documentation;

using SharpMeasures.SourceGenerators.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

internal static class DocumentationDependenciesProvider
{
    public delegate TypeDeclarationSyntax DInputTransform<TIn>(TIn input);
    public delegate TOut DOutputTransform<TIn, TOut>(TIn input, List<DocumentationFile> documentation);

    public static IncrementalValuesProvider<TOut> Attach<TIn, TOut>(IncrementalValuesProvider<AdditionalText> fileProvider, IncrementalValuesProvider<TIn> declarationProvider,
        DInputTransform<TIn> inputTransform, DOutputTransform<TIn, TOut> outputTransform)
    {
        return declarationProvider.Combine(fileProvider.Collect()).Select(extractDependencies);

        TOut extractDependencies((TIn input, ImmutableArray<AdditionalText> files) data, CancellationToken token)
            => outputTransform(data.input, DependencyExtractor.Extract(inputTransform(data.input).Identifier.Text, data.files, token));
    }

    public static IncrementalValuesProvider<TOut> AttachWithFilterStage<TIn, TOut>(IncrementalValuesProvider<AdditionalText> fileProvider,
        IncrementalValuesProvider<TIn> declarationProvider, DInputTransform<TIn> inputTransform, DOutputTransform<TIn, TOut> outputTransform, string directoryName)
        => Attach(DocumentationCandidateProvider.Attach(fileProvider, directoryName), declarationProvider, inputTransform, outputTransform);
}

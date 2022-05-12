namespace SharpMeasures.Generators.Units.Pipeline.ComparablePipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

using System.Threading;

internal static class DocumentationStage
{
    public readonly record struct Result(DefinedType Unit, NamedType Quantity, DocumentationFile Documentation);

    public static IncrementalValuesProvider<Result> AppendDocumentation(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<ReductionStage.Result> inputProvider)
    {
        var withDocumentation = Documentation.DocumentationProvider.Construct()
    }
}

namespace SharpMeasures.Generators.Units.Pipeline.MiscPipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

using System.Threading;

internal static class Stage4
{
    public readonly record struct Result(DefinedType TypeDefinition, NamedType Quantity, bool Biased, DocumentationFile Documentation);

    public static IncrementalValuesProvider<Result> Attach(IncrementalValuesProvider<Stage3.Result> inputProvider) => inputProvider.Select(DiscardSymbol);

    private static Result DiscardSymbol(Stage3.Result input, CancellationToken _)
    {
        return new(DefinedType.FromSymbol(input.TypeSymbol), input.Quantity, input.Biased, input.Documentation);
    }
}

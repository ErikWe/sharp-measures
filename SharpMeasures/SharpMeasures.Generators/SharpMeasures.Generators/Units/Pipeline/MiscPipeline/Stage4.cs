namespace SharpMeasures.Generators.Units.Pipeline.MiscPipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

using System.Threading;

internal static class Stage4
{
    public readonly record struct Result(DocumentationFile Documentation, DefinedType TypeDefinition, NamedType Quantity, bool Biased);

    public static IncrementalValuesProvider<Result> Attach(IncrementalValuesProvider<Stage3.Result> inputProvider) => inputProvider.Select(DiscardSymbol);

    private static Result DiscardSymbol(Stage3.Result input, CancellationToken _)
    {
        return new(input.Documentation, DefinedType.FromSymbol(input.TypeSymbol), input.Quantity, input.Biased);
    }
}

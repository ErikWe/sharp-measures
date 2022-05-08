namespace SharpMeasures.Generators.Units.Pipeline.MiscPipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

using System.Threading;

internal static class Stage4
{
    public readonly record struct Result(DefinedType TypeDefinition, NamedType Quantity, bool Biased, DocumentationFile Documentation);

    public static IncrementalValuesProvider<Result> MinimizeData(IncrementalValuesProvider<Stage3.Result> inputProvider)
        => inputProvider.Select(MinimizeData);

    private static Result MinimizeData(Stage3.Result input, CancellationToken _)
    {
        return new(DefinedType.FromSymbol(input.TypeSymbol), NamedType.FromSymbol(input.QuantitySymbol), input.Biased, input.Documentation);
    }
}

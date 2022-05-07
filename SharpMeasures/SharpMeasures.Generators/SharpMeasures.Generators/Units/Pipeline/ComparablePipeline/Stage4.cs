namespace SharpMeasures.Generators.Units.Pipeline.ComparablePipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

using System.Threading;

internal static class Stage4
{
    public readonly record struct Result(DefinedType TypeDefinition, NamedType Quantity, DocumentationFile Documentation);

    public static IncrementalValuesProvider<Result> Attach(IncrementalValuesProvider<Stage3.Result> inputProvider)
        => inputProvider.Select(DiscardBiasedUnits).WhereNotNull();

    private static Result? DiscardBiasedUnits(Stage3.Result input, CancellationToken _)
    {
        if (input.Biased)
        {
            return null;
        }

        return new Result(DefinedType.FromSymbol(input.TypeSymbol), input.Quantity, input.Documentation);
    }
}

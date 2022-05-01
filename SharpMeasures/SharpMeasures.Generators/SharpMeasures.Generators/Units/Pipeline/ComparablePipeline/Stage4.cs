namespace SharpMeasures.Generators.Units.Pipeline.ComparablePipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Threading;

internal static class Stage4
{
    public readonly record struct Result(IEnumerable<DocumentationFile> Documentation, DefinedType TypeDefinition, NamedType Quantity);

    public static IncrementalValuesProvider<Result> Perform(IncrementalValuesProvider<Stage3.Result> provider)
        => provider.Select(DiscardBiasedUnits).WhereNotNull();

    private static Result? DiscardBiasedUnits(Stage3.Result input, CancellationToken _)
    {
        if (input.Biased)
        {
            return null;
        }

        return new Result(input.Documentation, DefinedType.FromSymbol(input.TypeSymbol), input.Quantity);
    }
}

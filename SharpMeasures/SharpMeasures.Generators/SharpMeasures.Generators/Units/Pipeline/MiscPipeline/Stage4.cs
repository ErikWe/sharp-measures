namespace SharpMeasures.Generators.Units.Pipeline.MiscPipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Threading;

internal static class Stage4
{
    public readonly record struct Result(IEnumerable<DocumentationFile> Documentation, DefinedType TypeDefinition, NamedType Quantity, bool Biased);

    public static IncrementalValuesProvider<Result> Perform(IncrementalValuesProvider<Stage3.Result> provider)
        => provider.Select(DiscardSymbol);

    private static Result DiscardSymbol(Stage3.Result input, CancellationToken _)
    {
        return new(input.Documentation, input.TypeDefinition, input.Quantity, input.Biased);
    }
}

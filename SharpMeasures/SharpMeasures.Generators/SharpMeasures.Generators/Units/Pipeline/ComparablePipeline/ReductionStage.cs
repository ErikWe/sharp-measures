namespace SharpMeasures.Generators.Units.Pipeline.ComparablePipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Utility;

using System.Threading;

internal static class ReductionStage
{
    public readonly record struct Result(DefinedType Unit, CacheableGeneratedUnitDefinition Definition);

    public static IncrementalValuesProvider<Result> ReduceData(IncrementalValuesProvider<ParameterStage.Result> inputProvider)
    {
        return inputProvider.Select(DiscardBiasedAndReduceData).WhereNotNull();
    }

    private static Result? DiscardBiasedAndReduceData(ParameterStage.Result input, CancellationToken _)
    {
        if (input.Definition.AllowBias)
        {
            return null;
        }

        return new(DefinedType.FromSymbol(input.TypeSymbol), input.Definition.ToCacheable());
    }
}

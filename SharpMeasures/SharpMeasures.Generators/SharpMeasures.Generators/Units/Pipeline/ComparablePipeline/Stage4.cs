namespace SharpMeasures.Generators.Units.Pipeline.ComparablePipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Utility;

using System.Threading;

internal static class Stage4
{
    public static IncrementalValuesProvider<DataModel> MinimizeData(IncrementalValuesProvider<Stage3.Result> inputProvider)
        => inputProvider.Select(DiscardBiasedAndMinimizeData).WhereNotNull();

    private static DataModel? DiscardBiasedAndMinimizeData(Stage3.Result input, CancellationToken _)
    {
        if (input.Biased)
        {
            return null;
        }

        return new(DefinedType.FromSymbol(input.TypeSymbol), NamedType.FromSymbol(input.QuantitySymbol), input.Documentation);
    }
}

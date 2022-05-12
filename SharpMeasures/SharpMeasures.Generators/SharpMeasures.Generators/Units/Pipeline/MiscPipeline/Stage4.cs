namespace SharpMeasures.Generators.Units.Pipeline.MiscPipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Utility;

using System.Threading;

internal static class Stage4
{
    public static IncrementalValuesProvider<DataModel> MinimizeData(IncrementalValuesProvider<Stage3.Result> inputProvider)
        => inputProvider.Select(MinimizeData);

    private static DataModel MinimizeData(Stage3.Result input, CancellationToken _)
    {
        return new(DefinedType.FromSymbol(input.TypeSymbol), NamedType.FromSymbol(input.QuantitySymbol), input.Biased, input.Documentation);
    }
}

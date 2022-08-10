namespace SharpMeasures.Generators.Vectors.Pipelines.IndividualVector.Conversions;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class IndividualVectorConversionsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<IndividualVectorDataModel> inputProvider)
    {
        var reduced = inputProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel Reduce(IndividualVectorDataModel input, CancellationToken _)
    {
        return new(input.Vector.Type, input.Vector.Definition.Dimension, input.Vector.Conversions, input.VectorPopulation.VectorGroups, input.Documentation);
    }
}

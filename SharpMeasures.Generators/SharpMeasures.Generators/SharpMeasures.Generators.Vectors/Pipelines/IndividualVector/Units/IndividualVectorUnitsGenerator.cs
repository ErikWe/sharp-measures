namespace SharpMeasures.Generators.Vectors.Pipelines.IndividualVector.Units;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class IndividualVectorUnitsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<IndividualVectorDataModel> inputProvider)
    {
        var reduced = inputProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel Reduce(IndividualVectorDataModel input, CancellationToken _)
    {
        return new(input.Vector.Type, input.Vector.Definition.Dimension, input.Vector.Definition.Scalar?.Type.AsNamedType(), input.Vector.Definition.Unit,
            input.Vector.Definition.Unit.Definition.Quantity, input.Vector.IncludedUnits, input.Vector.Constants, input.Documentation);
    }
}

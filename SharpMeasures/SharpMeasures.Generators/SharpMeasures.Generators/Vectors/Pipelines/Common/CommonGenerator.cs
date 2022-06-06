namespace SharpMeasures.Generators.Vectors.Pipelines.Common;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class CommonGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Vectors.DataModel> inputProvider)
    {
        var reduced = inputProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel Reduce(Vectors.DataModel input, CancellationToken _)
    {
        return new(input.Vector.VectorType, input.Vector.VectorDefinition.Dimension, input.Scalar?.ScalarType.AsNamedType(), input.Scalar?.Square,
            input.Unit.UnitType.AsNamedType(), input.Unit.QuantityType, input.Vector.VectorDefinition.DefaultUnitName, input.Vector.VectorDefinition.DefaultUnitSymbol,
            input.Documentation);
    }
}

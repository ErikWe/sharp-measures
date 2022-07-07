namespace SharpMeasures.Generators.Scalars.Pipelines.Maths;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class MathsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Scalars.DataModel> inputProvider)
    {
        var reducedUnbiased = inputProvider.Select(Reduce);

        context.RegisterSourceOutput(reducedUnbiased, Execution.Execute);
    }

    private static DataModel Reduce(Scalars.DataModel input, CancellationToken _)
    {
        return new(input.ScalarData.ScalarType, input.ScalarDefinition.Unit.Type.AsNamedType(), input.ScalarDefinition.ImplementSum,
            input.ScalarDefinition.ImplementDifference, input.ScalarDefinition.Difference.Type.AsNamedType(),
            input.ScalarDefinition.Reciprocal?.Type.AsNamedType(), input.ScalarDefinition.Square?.Type.AsNamedType(),
            input.ScalarDefinition.Cube?.Type.AsNamedType(), input.ScalarDefinition.SquareRoot?.Type.AsNamedType(),
            input.ScalarDefinition.CubeRoot?.Type.AsNamedType(), input.Documentation);
    }
}

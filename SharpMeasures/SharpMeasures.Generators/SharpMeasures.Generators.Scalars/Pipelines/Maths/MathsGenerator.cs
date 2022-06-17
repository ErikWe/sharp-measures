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
        return new(input.ScalarData.ScalarType, input.ScalarDefinition.Unit.UnitType.AsNamedType(), input.ScalarDefinition.ImplementSum,
            input.ScalarDefinition.ImplementDifference, input.ScalarDefinition.Difference.ScalarType.AsNamedType(),
            input.ScalarDefinition.Reciprocal?.ScalarType.AsNamedType(), input.ScalarDefinition.Square?.ScalarType.AsNamedType(),
            input.ScalarDefinition.Cube?.ScalarType.AsNamedType(), input.ScalarDefinition.SquareRoot?.ScalarType.AsNamedType(),
            input.ScalarDefinition.CubeRoot?.ScalarType.AsNamedType(), input.Documentation);
    }
}

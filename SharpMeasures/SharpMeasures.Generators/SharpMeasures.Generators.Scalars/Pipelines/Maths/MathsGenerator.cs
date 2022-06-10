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
        return new(input.Scalar.ScalarType, input.Unit.UnitType.AsNamedType(), input.Scalar.ScalarDefinition.Reciprocal, input.Scalar.ScalarDefinition.Square,
            input.Scalar.ScalarDefinition.Cube, input.Scalar.ScalarDefinition.SquareRoot, input.Scalar.ScalarDefinition.CubeRoot, input.Documentation);
    }
}

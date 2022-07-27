namespace SharpMeasures.Generators.Scalars.Pipelines.Maths;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class MathsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Scalars.DataModel> inputProvider)
    {
        var reduced = inputProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel Reduce(Scalars.DataModel input, CancellationToken _)
    {
        return new(input.Scalar.Type, input.Scalar.Definition.Unit.Type.AsNamedType(), input.Scalar.Definition.ImplementSum, input.Scalar.Definition.ImplementDifference,
            input.Scalar.Definition.Difference.Type.AsNamedType(), input.Scalar.Definition.Reciprocal?.Type.AsNamedType(), input.Scalar.Definition.Square?.Type.AsNamedType(),
            input.Scalar.Definition.Cube?.Type.AsNamedType(), input.Scalar.Definition.SquareRoot?.Type.AsNamedType(), input.Scalar.Definition.CubeRoot?.Type.AsNamedType(),
            input.Documentation);
    }
}

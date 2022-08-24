namespace SharpMeasures.Generators.Scalars.Pipelines.Maths;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class MathsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Scalars.DataModel> modelProvider)
    {
        var reduced = modelProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel Reduce(Scalars.DataModel model, CancellationToken _)
    {
        return new(model.Scalar.Type, model.Scalar.Unit, model.Scalar.ImplementSum, model.Scalar.ImplementDifference, model.Scalar.Difference, model.Scalar.Reciprocal, model.Scalar.Square, model.Scalar.Cube,
            model.Scalar.SquareRoot, model.Scalar.CubeRoot, model.Documentation);
    }
}

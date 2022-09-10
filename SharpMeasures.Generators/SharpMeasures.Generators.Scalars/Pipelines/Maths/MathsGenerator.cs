namespace SharpMeasures.Generators.Scalars.Pipelines.Maths;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class MathsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<Scalars.DataModel>> modelProvider)
    {
        var reduced = modelProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static Optional<DataModel> Reduce(Optional<Scalars.DataModel> model, CancellationToken _)
    {
        if (model.HasValue is false)
        {
            return new Optional<DataModel>();
        }

        return new DataModel(model.Value.Scalar.Type, model.Value.Scalar.Unit, model.Value.Scalar.ImplementSum, model.Value.Scalar.ImplementDifference, model.Value.Scalar.Difference, model.Value.Scalar.Reciprocal, model.Value.Scalar.Square, model.Value.Scalar.Cube,
            model.Value.Scalar.SquareRoot, model.Value.Scalar.CubeRoot, model.Value.Documentation);
    }
}

namespace SharpMeasures.Generators.Scalars.SourceBuilding.Maths;

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

        return new DataModel(model.Value.Scalar.Type, model.Value.Scalar.Unit, model.Value.Scalar.ImplementSum, model.Value.Scalar.ImplementDifference, model.Value.Scalar.Difference, model.Value.SourceBuildingContext);
    }
}

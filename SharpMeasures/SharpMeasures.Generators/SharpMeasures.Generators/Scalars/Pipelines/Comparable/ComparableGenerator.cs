namespace SharpMeasures.Generators.Scalars.Pipelines.Comparable;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class ComparableGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Scalars.DataModel> inputProvider)
    {
        var reduced = inputProvider.Select(ReduceToDataModel);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel ReduceToDataModel(Scalars.DataModel input, CancellationToken _)
    {
        return new(input.Scalar.ScalarType, input.Unit.UnitType.AsNamedType(), input.Documentation);
    }
}

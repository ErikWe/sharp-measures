namespace SharpMeasures.Generators.Scalars.Pipelines.Misc;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class MiscGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Scalars.DataModel> inputProvider)
    {
        var reduced = inputProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel Reduce(Scalars.DataModel input, CancellationToken _)
    {
        return new(input.Scalar.ScalarType, input.Unit.UnitType.AsNamedType(), input.Unit.QuantityType, input.Scalar.ScalarDefinition.Biased,
            input.Scalar.ScalarDefinition.DefaultUnitName, input.Scalar.ScalarDefinition.DefaultUnitSymbol, input.Documentation);
    }
}

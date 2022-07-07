namespace SharpMeasures.Generators.Scalars.Pipelines.Common;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.SourceBuilding;

using System.Threading;

internal static class CommonGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Scalars.DataModel> inputProvider)
    {
        var reduced = inputProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel Reduce(Scalars.DataModel input, CancellationToken _)
    {
        return new(input.ScalarData.ScalarType, input.ScalarDefinition.Unit.Type.AsNamedType(), input.ScalarDefinition.Unit.QuantityType,
            input.ScalarDefinition.UseUnitBias, input.ScalarDefinition.DefaultUnitName, input.ScalarDefinition.DefaultUnitSymbol, input.Documentation,
            SourceBuildingUtility.ToParameterName(input.ScalarDefinition.Unit.Type.Name));
    }
}

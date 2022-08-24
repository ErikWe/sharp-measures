namespace SharpMeasures.Generators.Units.Pipelines.Common;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.SourceBuilding;

using System.Threading;

internal static class CommonGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Units.DataModel> modelProvider)
    {
        var reduced = modelProvider.Select(ReduceToDataModel);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel ReduceToDataModel(Units.DataModel model, CancellationToken _)
    {
        return new(model.Unit.Type, model.Unit.Definition.Quantity, model.Unit.Definition.BiasTerm, SourceBuildingUtility.ToParameterName(model.Unit.Definition.Quantity.Name), model.Documentation);
    }
}

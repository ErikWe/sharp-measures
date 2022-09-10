namespace SharpMeasures.Generators.Units.Pipelines.Common;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.SourceBuilding;

using System.Threading;

internal static class CommonGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<Units.DataModel>> modelProvider)
    {
        var reduced = modelProvider.Select(ReduceToDataModel);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static Optional<DataModel> ReduceToDataModel(Optional<Units.DataModel> model, CancellationToken _)
    {
        if (model.HasValue is false)
        {
            return new Optional<DataModel>();
        }

        return new DataModel(model.Value.Unit.Type, model.Value.Unit.Definition.Quantity, model.Value.Unit.Definition.BiasTerm, SourceBuildingUtility.ToParameterName(model.Value.Unit.Definition.Quantity.Name), model.Value.Documentation);
    }
}

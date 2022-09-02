namespace SharpMeasures.Generators.Scalars.Pipelines.Common;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.SourceBuilding;

using System.Threading;

internal static class CommonGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Scalars.DataModel> modelProvider)
    {
        var reduced = modelProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel Reduce(Scalars.DataModel model, CancellationToken _)
    {
        var unit = model.UnitPopulation.Units[model.Scalar.Unit];

        string unitParameterName = SourceBuildingUtility.ToParameterName(model.Scalar.Unit.Name);

        return new(model.Scalar.Type, model.Scalar.Unit, unit.Definition.Quantity, unitParameterName, model.Scalar.UseUnitBias, model.Scalar.DefaultUnitInstanceName, model.Scalar.DefaultUnitInstanceSymbol, model.Documentation);
    }
}

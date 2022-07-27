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
        string unitParameterName = SourceBuildingUtility.ToParameterName(input.Scalar.Definition.Unit.Type.Name);

        return new(input.Scalar.Type, input.Scalar.Definition.Unit.Type.AsNamedType(), input.Scalar.Definition.Unit.Definition.Quantity, unitParameterName,
            input.Scalar.Definition.UseUnitBias, input.Scalar.Definition.DefaultUnit, input.Scalar.Definition.DefaultUnitSymbol, input.Documentation);
    }
}

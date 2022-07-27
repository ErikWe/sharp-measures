namespace SharpMeasures.Generators.Vectors.Pipelines.IndividualVector.Common;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.SourceBuilding;

using System.Threading;

internal static class IndividualVectorCommonGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<IndividualVectorDataModel> inputProvider)
    {
        var reduced = inputProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel Reduce(IndividualVectorDataModel input, CancellationToken _)
    {
        return new(input.Vector.Type, input.Vector.Definition.Dimension, input.Vector.Definition.Scalar?.Type.AsNamedType(), input.Vector.Definition.Scalar?.Definition.Square,
            input.Vector.Definition.Unit.Type.AsNamedType(), input.Vector.Definition.Unit.Definition.Quantity,
            SourceBuildingUtility.ToParameterName(input.Vector.Definition.Unit.Type.Name), input.Vector.Definition.DefaultUnit, input.Vector.Definition.DefaultUnitSymbol,
            input.Documentation);
    }
}

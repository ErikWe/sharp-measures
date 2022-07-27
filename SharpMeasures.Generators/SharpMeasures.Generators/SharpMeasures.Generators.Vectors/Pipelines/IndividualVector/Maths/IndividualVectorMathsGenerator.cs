namespace SharpMeasures.Generators.Vectors.Pipelines.IndividualVector.Maths;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class IndividualVectorMathsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<IndividualVectorDataModel> inputProviderProvider)
    {
        var reduced = inputProviderProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel Reduce(IndividualVectorDataModel input, CancellationToken _)
    {
        return new(input.Vector.Type, input.Vector.Definition.Dimension, input.Vector.Definition.ImplementSum, input.Vector.Definition.ImplementDifference,
            input.Vector.Definition.Difference.Type.AsNamedType(), input.Vector.Definition.Scalar?.Type.AsNamedType(), input.Vector.Definition.Scalar?.Definition.Square,
            input.Vector.Definition.Unit.Type.AsNamedType(), input.Vector.Definition.Unit.Definition.Quantity, input.Documentation);
    }
}

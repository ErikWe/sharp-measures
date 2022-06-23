namespace SharpMeasures.Generators.Vectors.Pipelines.Maths;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class MathsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Vectors.DataModel> vectorProvider,
        IncrementalValuesProvider<ResizedDataModel> resizedVectorProvider)
    {
        var reducedVectors = vectorProvider.Select(ReduceToDataModel);
        var reducedResizedVectors = resizedVectorProvider.Select(ReduceToDataModel);

        context.RegisterSourceOutput(reducedVectors, Execution.Execute);
        context.RegisterSourceOutput(reducedResizedVectors, Execution.Execute);
    }

    private static DataModel ReduceToDataModel(IDataModel input, CancellationToken _)
    {
        return new(input.VectorType, input.Dimension, input.ImplementSum, input.ImplementDifference, input.Difference, input.Scalar?.ScalarType.AsNamedType(), 
            input.Scalar?.Square, input.Unit.UnitType.AsNamedType(), input.Unit.QuantityType, input.Documentation);
    }
}

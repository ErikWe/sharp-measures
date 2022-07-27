namespace SharpMeasures.Generators.Scalars.Pipelines.Vectors;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class VectorsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Scalars.DataModel> inputProvider)
    {
        var reducedAndFiltered = inputProvider.Select(Reduce).WhereNotNull();

        context.RegisterSourceOutput(reducedAndFiltered, Execution.Execute);
    }

    private static DataModel? Reduce(Scalars.DataModel input, CancellationToken _)
    {
        if (input.Scalar.Definition.VectorGroup?.RegisteredMembersByDimension.Count is null or 0)
        {
            return null;
        }

        return new(input.Scalar.Type, input.Scalar.Definition.VectorGroup, input.Documentation);
    }
}

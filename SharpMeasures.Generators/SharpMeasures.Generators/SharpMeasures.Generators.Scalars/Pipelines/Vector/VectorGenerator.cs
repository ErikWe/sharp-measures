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
        if (input.Scalar.Definition.VectorGroup is null)
        {
            return null;
        }

        if (input.VectorPopulation.VectorGroups.TryGetValue(input.Scalar.Definition.VectorGroup.Type.AsNamedType(), out var vectorGroup) is false)
        {
            return null;
        }

        if (vectorGroup.MembersByDimension.Count is 0)
        {
            return null;
        }

        return new(input.Scalar.Type, vectorGroup, input.Documentation);
    }
}

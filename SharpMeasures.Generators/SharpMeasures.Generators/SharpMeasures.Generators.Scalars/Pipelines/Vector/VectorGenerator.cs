namespace SharpMeasures.Generators.Scalars.Pipelines.Vectors;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class VectorsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<BaseDataModel> inputProvider)
    {
        var reducedAndFiltered = inputProvider.Select(Reduce).WhereNotNull();

        context.RegisterSourceOutput(reducedAndFiltered, Execution.Execute);
    }

    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<SpecializedDataModel> inputProvider)
    {
        var reducedAndFiltered = inputProvider.Select(Reduce).WhereNotNull();

        context.RegisterSourceOutput(reducedAndFiltered, Execution.Execute);
    }

    private static DataModel? Reduce<TScalarType>(ADataModel<TScalarType> input, CancellationToken _) where TScalarType : IScalarType
    {
        if (input.Scalar.Definition.Vectors.Count is 0)
        {
            return null;
        }

        return new(input.Scalar.Type, input.Scalar.Definition.Vectors, input.Documentation);
    }
}

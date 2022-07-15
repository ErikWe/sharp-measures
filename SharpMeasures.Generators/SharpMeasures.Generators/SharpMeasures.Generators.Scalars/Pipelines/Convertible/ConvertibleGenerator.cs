namespace SharpMeasures.Generators.Scalars.Pipelines.Convertible;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class ConvertibleGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<BaseDataModel> inputProvider)
    {
        var reduced = inputProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<SpecializedDataModel> inputProvider)
    {
        var reduced = inputProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel Reduce<TScalarType>(ADataModel<TScalarType> input, CancellationToken _) where TScalarType : IScalarType
    {
        return new(input.Scalar.Type, input.Inheritance.ConvertibleScalars, input.Documentation);
    }
}

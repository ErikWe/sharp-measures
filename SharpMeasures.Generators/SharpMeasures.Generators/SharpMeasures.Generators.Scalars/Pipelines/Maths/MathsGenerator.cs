namespace SharpMeasures.Generators.Scalars.Pipelines.Maths;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class MathsGenerator
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
        return new(input.Scalar.Type, input.Inheritance.BaseScalar.Unit.Type.AsNamedType(), input.Inheritance.ImplementSum, input.Inheritance.ImplementDifference,
            input.Inheritance.Difference.Type.AsNamedType(), input.Inheritance.Reciprocal?.Type.AsNamedType(), input.Inheritance.Square?.Type.AsNamedType(),
            input.Inheritance.Cube?.Type.AsNamedType(), input.Inheritance.SquareRoot?.Type.AsNamedType(), input.Inheritance.CubeRoot?.Type.AsNamedType(), input.Documentation);
    }
}

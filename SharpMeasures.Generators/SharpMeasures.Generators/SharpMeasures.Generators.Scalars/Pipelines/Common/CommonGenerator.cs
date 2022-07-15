namespace SharpMeasures.Generators.Scalars.Pipelines.Common;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.SourceBuilding;

using System.Threading;

internal static class CommonGenerator
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
        string unitParameterName = SourceBuildingUtility.ToParameterName(input.Inheritance.BaseScalar.Unit.Type.Name);

        return new(input.Scalar.Type, input.Inheritance.BaseScalar.Unit.Type.AsNamedType(), input.Inheritance.BaseScalar.Unit.Definition.Quantity, unitParameterName,
            input.Inheritance.BaseScalar.UseUnitBias, input.Inheritance.DefaultUnit, input.Inheritance.DefaultUnitSymbol, input.Documentation);
    }
}

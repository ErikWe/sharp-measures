namespace SharpMeasures.Generators.Vectors.Pipelines.Common;

using Microsoft.CodeAnalysis;

using SharpMeasures.Equatables;

using System.Threading;
using System.Collections.Generic;
using System.Collections.Immutable;

internal static class CommonGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Vectors.DataModel> vectorProvider,
        IncrementalValuesProvider<ResizedDataModel> resizedVectorProvider)
    {
        var reduced = vectorProvider.Select(ReduceToDataModel);

        var rootModels = reduced.Collect().Select(ExposeRootDataModels);

        var associatedReduced = resizedVectorProvider.Combine(rootModels).Select(ReduceThroughAssociatedDataModel).WhereNotNull();

        context.RegisterSourceOutput(reduced, Execution.Execute);
        context.RegisterSourceOutput(associatedReduced, Execution.Execute);
    }

    private static DataModel ReduceToDataModel(Vectors.DataModel input, CancellationToken _)
    {
        return new(input.Vector.VectorType, input.Vector.VectorDefinition.Dimension, input.Scalar?.ScalarType.AsNamedType(), input.Scalar?.Square,
            input.Unit.UnitType.AsNamedType(), input.Unit.QuantityType, input.Vector.VectorDefinition.DefaultUnitName, input.Vector.VectorDefinition.DefaultUnitSymbol,
            input.Documentation);
    }

    private static ReadOnlyEquatableDictionary<NamedType, DataModel> ExposeRootDataModels(ImmutableArray<DataModel> dataModels, CancellationToken _)
    {
        Dictionary<NamedType, DataModel> dictionary = new(dataModels.Length);

        foreach (DataModel dataModel in dataModels)
        {
            dictionary.Add(dataModel.Vector.AsNamedType(), dataModel);
        }

        return new(dictionary);
    }

    private static DataModel? ReduceThroughAssociatedDataModel((ResizedDataModel Model, ReadOnlyEquatableDictionary<NamedType, DataModel> Dictionary) input,
        CancellationToken _)
    {
        if (input.Dictionary.TryGetValue(input.Model.AssociatedRootVector, out var associatedModel) is false)
        {
            return null;
        }

       return new(input.Model.Vector.VectorType, input.Model.Vector.VectorDefinition.Dimension, associatedModel.Scalar, associatedModel.SquaredScalar,
            associatedModel.Unit, associatedModel.UnitQuantity, associatedModel.DefaultUnitName, associatedModel.DefaultUnitSymbol, input.Model.Documentation);
    }
}

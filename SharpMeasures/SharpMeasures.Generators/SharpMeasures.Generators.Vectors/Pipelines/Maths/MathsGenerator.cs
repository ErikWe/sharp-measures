namespace SharpMeasures.Generators.Vectors.Pipelines.Maths;

using Microsoft.CodeAnalysis;

using SharpMeasures.Equatables;

using System.Threading;
using System.Collections.Generic;
using System.Collections.Immutable;

internal static class MathsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Vectors.DataModel> generatedVectorProvider,
        IncrementalValuesProvider<ResizedDataModel> resizedVectorProvider)
    {
        var reducedGeneratedVectors = generatedVectorProvider.Select(ReduceToDataModel);

        var rootModels = reducedGeneratedVectors.Collect().Select(ExposeRootDataModels);

        var reducedResizedVectors = resizedVectorProvider.Combine(rootModels).Select(ReduceThroughRootDataModel).WhereNotNull();

        context.RegisterSourceOutput(reducedGeneratedVectors, Execution.Execute);
        context.RegisterSourceOutput(reducedResizedVectors, Execution.Execute);
    }

    private static DataModel ReduceToDataModel(Vectors.DataModel input, CancellationToken _)
    {
        return new(input.VectorData.VectorType, input.VectorDefinition.Dimension, input.VectorDefinition.Scalar?.ScalarType.AsNamedType(),
            input.VectorDefinition.Scalar?.Square, input.VectorDefinition.Unit.UnitType.AsNamedType(), input.VectorDefinition.Unit.QuantityType, input.Documentation);
    }

    private static ReadOnlyEquatableDictionary<NamedType, DataModel> ExposeRootDataModels(ImmutableArray<DataModel> dataModels, CancellationToken _)
    {
        Dictionary<NamedType, DataModel> dictionary = new(dataModels.Length);

        foreach (DataModel dataModel in dataModels)
        {
            dictionary.Add(dataModel.Vector.AsNamedType(), dataModel);
        }

        return dictionary.AsReadOnlyEquatable();
    }

    private static DataModel? ReduceThroughRootDataModel((ResizedDataModel Model, ReadOnlyEquatableDictionary<NamedType, DataModel> Dictionary) input,
        CancellationToken _)
    {
        if (input.Dictionary.TryGetValue(input.Model.VectorDefinition.VectorGroup.Root.VectorType, out var associatedModel) is false)
        {
            return null;
        }

       return new(input.Model.VectorData.VectorType, input.Model.VectorDefinition.Dimension, associatedModel.Scalar, associatedModel.SquaredScalar,
            associatedModel.Unit, associatedModel.UnitQuantity, input.Model.Documentation);
    }
}

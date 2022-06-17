﻿namespace SharpMeasures.Generators.Scalars.Pipelines.Vectors;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class VectorsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Scalars.DataModel> inputProvider)
    {
        var reduced = inputProvider.Select(ReduceToDataModel).WhereNotNull();

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel? ReduceToDataModel(Scalars.DataModel input, CancellationToken _)
    {
        if (input.ScalarDefinition.VectorGroup?.VectorsByDimension.Count is null or 0)
        {
            return null;
        }

        return new(input.ScalarData.ScalarType, input.ScalarDefinition.VectorGroup, input.Documentation);
    }
}
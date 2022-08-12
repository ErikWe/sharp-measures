﻿namespace SharpMeasures.Generators.Vectors.Pipelines.IndividualVector.Units;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class IndividualVectorUnitsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<IndividualVectorDataModel> inputProvider)
    {
        var filteredAndReduced = inputProvider.Select(Reduce).WhereNotNull();

        context.RegisterSourceOutput(filteredAndReduced, Execution.Execute);
    }

    private static DataModel? Reduce(IndividualVectorDataModel input, CancellationToken _)
    {
        if (input.Vector.IncludedUnits.Count is 0 && input.Vector.Constants.Count is 0)
        {
            return null;
        }

        return new(input.Vector.Type, input.Vector.Definition.Dimension, input.Vector.Definition.Scalar?.Type.AsNamedType(), input.Vector.Definition.Unit,
            input.Vector.Definition.Unit.Definition.Quantity, input.Vector.IncludedUnits, input.Vector.Constants, input.Documentation);
    }
}

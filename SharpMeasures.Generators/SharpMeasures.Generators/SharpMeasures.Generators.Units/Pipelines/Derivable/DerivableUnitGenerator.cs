﻿namespace SharpMeasures.Generators.Units.Pipelines.Derivable;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class DerivableUnitGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Units.DataModel> inputProvider)
    {
        var reduced = inputProvider.Select(ReduceToDataModel);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel ReduceToDataModel(Units.DataModel input, CancellationToken _)
    {
        return new(input.Unit.Type, input.Unit.Definition.Quantity.Type.AsNamedType(), input.Unit.UnitDerivations, input.Documentation);
    }
}

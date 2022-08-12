namespace SharpMeasures.Generators.Units.Pipelines.Derivable;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class DerivableUnitGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Units.DataModel> inputProvider)
    {
        var filteredAndReduced = inputProvider.Select(ReduceToDataModel).WhereNotNull();

        context.RegisterSourceOutput(filteredAndReduced, Execution.Execute);
    }

    private static DataModel? ReduceToDataModel(Units.DataModel input, CancellationToken _)
    {
        if (input.Unit.UnitDerivations.Any() is false)
        {
            return null;
        }

        return new(input.Unit.Type, input.Unit.Definition.Quantity.Type.AsNamedType(), input.Unit.UnitDerivations, input.Documentation);
    }
}

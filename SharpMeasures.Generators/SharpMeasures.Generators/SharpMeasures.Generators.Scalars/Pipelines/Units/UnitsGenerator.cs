namespace SharpMeasures.Generators.Scalars.Pipelines.Units;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class UnitsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Scalars.DataModel> inputProvider)
    {
        var filteredAndReduced = inputProvider.Select(Reduce).WhereNotNull();

        context.RegisterSourceOutput(filteredAndReduced, Execution.Execute);
    }

    private static DataModel? Reduce(Scalars.DataModel input, CancellationToken _)
    {
        if (input.Scalar.IncludedBases.Count is 0 && input.Scalar.IncludedUnits.Count is 0 && input.Scalar.Constants.Count is 0)
        {
            return null;
        }

        return new(input.Scalar.Type, input.Scalar.Definition.Unit.Type.AsNamedType(), input.Scalar.Definition.Unit.Definition.Quantity, input.Scalar.IncludedBases, input.Scalar.IncludedUnits,
            input.Scalar.Constants, input.Documentation);
    }
}

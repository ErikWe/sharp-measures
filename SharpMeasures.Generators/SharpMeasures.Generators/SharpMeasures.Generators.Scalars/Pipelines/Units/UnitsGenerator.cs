namespace SharpMeasures.Generators.Scalars.Pipelines.Units;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class UnitsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Scalars.DataModel> inputProvider)
    {
        var reduced = inputProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel Reduce(Scalars.DataModel input, CancellationToken _)
    {
        return new(input.Scalar.Type, input.Scalar.Definition.Unit.Type.AsNamedType(), input.Scalar.Definition.Unit.Definition.Quantity,
            input.Scalar.IncludedBases, input.Scalar.IncludedUnits, input.Scalar.Constants, input.Documentation);
    }
}

namespace SharpMeasures.Generators.Scalars.Pipelines.Derivations;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class DerivationsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<Scalars.DataModel>> modelProvider)
    {
        var reduced = modelProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static Optional<DataModel> Reduce(Optional<Scalars.DataModel> model, CancellationToken _)
    {
        if (model.HasValue is false || model.Value.Scalar.Derivations.Count is 0 && model.Value.Scalar.InheritedDerivations.Count is 0)
        {
            return new Optional<DataModel>();
        }

        return new DataModel(model.Value.Scalar.Type, model.Value.Scalar.Derivations.Concat(model.Value.Scalar.InheritedDerivations).ToList(), model.Value.ScalarPopulation, model.Value.Documentation);
    }
}

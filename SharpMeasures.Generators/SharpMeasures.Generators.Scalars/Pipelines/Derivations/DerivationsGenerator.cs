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
        if (model.HasValue is false)
        {
            return new Optional<DataModel>();
        }

        var operatorImplementations = model.Value.ScalarPopulation.OperatorImplementationsByQuantity[model.Value.Scalar.Type.AsNamedType()].ToList();

        if (model.Value.Scalar.DefinedDerivations.Count is 0 && model.Value.Scalar.InheritedDerivations.Count is 0 && operatorImplementations.Count is 0)
        {
            return new Optional<DataModel>();
        }

        return new DataModel(model.Value.Scalar.Type, model.Value.Scalar.DefinedDerivations.Concat(model.Value.Scalar.InheritedDerivations).ToList(), model.Value.ScalarPopulation.OperatorImplementationsByQuantity[model.Value.Scalar.Type.AsNamedType()].ToList(), model.Value.ScalarPopulation, model.Value.Documentation);
    }
}

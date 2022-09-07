namespace SharpMeasures.Generators.Scalars.Pipelines.Derivations;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class DerivationsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Scalars.DataModel> modelProvider)
    {
        var filteredAndReduced = modelProvider.Select(Reduce).WhereNotNull();

        context.RegisterSourceOutput(filteredAndReduced, Execution.Execute);
    }

    private static DataModel? Reduce(Scalars.DataModel model, CancellationToken _)
    {
        if (model.Scalar.DefinedDerivations.Count is 0 && model.Scalar.InheritedDerivations.Count is 0)
        {
            return null;
        }

        return new(model.Scalar.Type, model.Scalar.DefinedDerivations.Concat(model.Scalar.InheritedDerivations).ToList(), model.ScalarPopulation.OperatorImplementationsByQuantity[model.Scalar.Type.AsNamedType()].ToList(), model.ScalarPopulation, model.Documentation);
    }
}

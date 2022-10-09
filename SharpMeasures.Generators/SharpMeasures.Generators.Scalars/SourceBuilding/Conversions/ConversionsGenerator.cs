namespace SharpMeasures.Generators.Scalars.SourceBuilding.Conversions;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class ConversionsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<Scalars.DataModel>> modelProvider)
    {
        var reduced = modelProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static Optional<DataModel> Reduce(Optional<Scalars.DataModel> model, CancellationToken _)
    {
        if (model.HasValue is false || model.Value.Scalar.Conversions.Count is 0 && model.Value.Scalar.OriginalQuantity is null)
        {
            return new Optional<DataModel>();
        }

        return new DataModel(model.Value.Scalar.Type, model.Value.Scalar.Conversions, model.Value.Scalar.InheritedConversions, model.Value.Scalar.SpecializationForwardsConversionBehaviour, model.Value.Scalar.SpecializationBackwardsConversionBehaviour, model.Value.ScalarPopulation, model.Value.SourceBuildingContext);
    }
}

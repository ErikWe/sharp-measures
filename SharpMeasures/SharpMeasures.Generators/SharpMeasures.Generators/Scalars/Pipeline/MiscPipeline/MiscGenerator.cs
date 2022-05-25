namespace SharpMeasures.Generators.Scalars.Pipeline.MiscPipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Utility;

using System.Threading;

internal static class MiscGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<DocumentationStage.Result> inputProvider)
    {
        var reducedUnbiased = inputProvider.Select(Reduce);

        context.RegisterSourceOutput(reducedUnbiased, Execution.Execute);
    }

    private static DataModel Reduce(DocumentationStage.Result input, CancellationToken _)
    {
        NamedType unitQuantity = input.Definition.UnitDefinition?.Quantity is not null
            ? NamedType.FromSymbol(input.Definition.UnitDefinition.Quantity)
            : NamedType.Empty;

        return new(DefinedType.FromSymbol(input.TypeSymbol), NamedType.FromSymbol(input.Definition.Unit!), unitQuantity, input.Definition.Biased, input.Documentation);
    }
}

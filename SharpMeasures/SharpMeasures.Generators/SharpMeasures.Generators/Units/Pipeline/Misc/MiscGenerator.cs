namespace SharpMeasures.Generators.Units.Pipeline.Misc;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Utility;

using System.Threading;

internal static class MiscGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<DocumentationStage.Result> inputProvider)
    {
        var reduced = inputProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel Reduce(DocumentationStage.Result input, CancellationToken _)
    {
        return new(DefinedType.FromSymbol(input.TypeSymbol), NamedType.FromSymbol(input.Definition.Quantity!), input.Definition.AllowBias, input.Documentation);
    }
}

namespace SharpMeasures.Generators.Scalars.Pipeline.StandardMathsPipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Utility;

using System.Threading;

internal static class StandardMathsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<DocumentationStage.Result> inputProvider)
    {
        var reducedUnbiased = inputProvider.Select(Reduce);

        context.RegisterSourceOutput(reducedUnbiased, Execution.Execute);
    }

    private static DataModel Reduce(DocumentationStage.Result input, CancellationToken _)
    {
        return new(DefinedType.FromSymbol(input.TypeSymbol), NamedType.FromSymbol(input.Definition.Unit!), input.Documentation);
    }
}

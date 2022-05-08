namespace SharpMeasures.Generators.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Scalars.Pipeline;
using SharpMeasures.Generators.Scalars.SourceBuilding;

using System.Text;

internal static class ScalarQuantityGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<SharpMeasuresGenerator.Result> declarationAndSymbolProvider)
    {
        var firstStage = Stage1.ExtractRelevantDeclarations(context, declarationAndSymbolProvider);
        var secondStage = Stage2.ParseParameters(context, firstStage);
        var thirdStage = Stage3.AttachDocumentation(context, secondStage);
        var fourthStage = Stage4.Attach(thirdStage);
        var fifthStage = Stage5.Attach(fourthStage);

        context.RegisterSourceOutput(fifthStage, Execute);
    }

    private static void Execute(SourceProductionContext context, Stage5.Result result)
    {
        string source = SourceComposer.Compose(result, context.CancellationToken);

        context.AddSource($"{result.TypeDefinition.Name}.g.cs", SourceText.From(source, Encoding.UTF8));
    }
}

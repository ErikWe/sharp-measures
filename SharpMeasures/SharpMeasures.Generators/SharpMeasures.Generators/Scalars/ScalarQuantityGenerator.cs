namespace SharpMeasures.Generators.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Scalars.Pipeline;
using SharpMeasures.Generators.Scalars.SourceBuilding;

using System.Text;

[Generator]
public class ScalarQuantityGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<TypeDeclarationSyntax> firstStage = Stage1.Attach(context);
        IncrementalValuesProvider<Stage2.Result> secondStage = Stage2.Attach(context, firstStage);
        IncrementalValuesProvider<Stage3.Result> thirdStage = Stage3.Attach(context, secondStage);
        IncrementalValuesProvider<Stage4.Result> fourthStage = Stage4.Attach(thirdStage);
        IncrementalValuesProvider<Stage5.Result> fifthStage = Stage5.Attach(fourthStage);

        context.RegisterSourceOutput(fifthStage, Execute);
    }

    private static void Execute(SourceProductionContext context, Stage5.Result result)
    {
        string source = SourceComposer.Compose(result, context.CancellationToken);
        string documentedSource = result.Documentation.ResolveTextAndReportDiagnostics(context, source);

        context.AddSource($"{result.TypeDefinition.Name}.g.cs", SourceText.From(documentedSource, Encoding.UTF8));
    }
}

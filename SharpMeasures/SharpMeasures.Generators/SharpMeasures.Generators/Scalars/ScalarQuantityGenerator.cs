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
        IncrementalValuesProvider<TypeDeclarationSyntax> firstStage = Stage1.Perform(context);
        IncrementalValuesProvider<Stage2.Result> secondStage = Stage2.Perform(context, firstStage);
        IncrementalValuesProvider<Stage3.Result> thirdStage = Stage3.Perform(context, secondStage);
        IncrementalValuesProvider<Stage4.Result> fourthStage = Stage4.Perform(thirdStage);
        IncrementalValuesProvider<Stage5.Result> fifthStage = Stage5.Perform(fourthStage);

        context.RegisterSourceOutput(fifthStage, Execute);
    }

    private static void Execute(SourceProductionContext context, Stage5.Result result)
    {
        string source = SourceComposer.Compose(result, context.CancellationToken);

        context.AddSource($"{result.TypeDefinition.Name}.g.cs", SourceText.From(source, Encoding.UTF8));
    }
}

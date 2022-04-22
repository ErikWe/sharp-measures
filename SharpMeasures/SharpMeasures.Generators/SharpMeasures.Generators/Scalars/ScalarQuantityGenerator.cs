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
        IncrementalValuesProvider<ThirdStage.Result> thirdStage = ThirdStage.Perform(context, secondStage);
        IncrementalValuesProvider<FourthStage.Result> fourthStage = FourthStage.Perform(thirdStage);
        IncrementalValuesProvider<FifthStage.Result> fifthStage = FifthStage.Perform(fourthStage);

        context.RegisterSourceOutput(fifthStage, Execute);
    }

    private static void Execute(SourceProductionContext context, FifthStage.Result result)
    {
        string source = SourceComposer.Compose(result, context.CancellationToken);

        context.AddSource($"{result.TypeDefinition.Name}.g.cs", SourceText.From(source, Encoding.UTF8));
    }
}

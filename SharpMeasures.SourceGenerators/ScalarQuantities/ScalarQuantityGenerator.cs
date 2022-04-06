namespace ErikWe.SharpMeasures.SourceGenerators.ScalarQuantities;

using ErikWe.SharpMeasures.SourceGenerators.ScalarQuantities.Pipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using System.Text;

[Generator]
public class ScalarQuantityGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<FirstStage.Result> firstStage = FirstStage.Perform(context);
        IncrementalValuesProvider<SecondStage.Result> secondStage = SecondStage.Perform(context, firstStage);
        IncrementalValuesProvider<ThirdStage.Result> thirdStage = ThirdStage.Perform(context, secondStage);
        IncrementalValuesProvider<FourthStage.Result> fourthStage = FourthStage.Perform(thirdStage);
        context.RegisterSourceOutput(fourthStage, Execute);
    }

    private static void Execute(SourceProductionContext context, FourthStage.Result result)
    {
        string source = SourceBuilder.Build(result, context.CancellationToken);

        context.AddSource($"{result.TypeDeclaration.Identifier.Text}.g.cs", SourceText.From(source, Encoding.UTF8));
    }
}

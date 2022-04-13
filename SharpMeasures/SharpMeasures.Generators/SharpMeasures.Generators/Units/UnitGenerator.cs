namespace SharpMeasures.Generators.Units;

using SharpMeasures.Generators.Units.Pipeline;
using SharpMeasures.Generators.Units.SourceBuilding;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using System.Text;

[Generator]
public class UnitGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<FirstStage.Result> firstStage = FirstStage.Perform(context);
        IncrementalValuesProvider<SecondStage.Result> secondStage = SecondStage.Perform(context, firstStage);
        IncrementalValuesProvider<ThirdStage.Result> thirdStage = ThirdStage.Perform(secondStage);
        IncrementalValuesProvider<FourthStage.Result> fourthStage = FourthStage.Perform(thirdStage);
        context.RegisterSourceOutput(fourthStage, Execute);
    }

    private static void Execute(SourceProductionContext context, FourthStage.Result result)
    {
        string source = SourceComposer.Compose(result, context.CancellationToken);

        context.AddSource($"{result.Declaration.Type.Identifier.Text}.g.cs", SourceText.From(source, Encoding.UTF8));
    }
}
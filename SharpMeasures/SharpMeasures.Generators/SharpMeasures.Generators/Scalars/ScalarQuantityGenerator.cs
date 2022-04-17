namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Scalars.Pipeline;
using SharpMeasures.Generators.Scalars.SourceBuilding;

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
        IncrementalValuesProvider<FifthStage.Result> fifthStage = FifthStage.Perform(fourthStage);

        context.RegisterSourceOutput(fifthStage, Execute);
    }

    private static void Execute(SourceProductionContext context, FifthStage.Result result)
    {
        string source = SourceComposer.Compose(result, context.CancellationToken);

        context.AddSource($"{result.TypeDefinition.Name}.g.cs", SourceText.From(source, Encoding.UTF8));
    }
}

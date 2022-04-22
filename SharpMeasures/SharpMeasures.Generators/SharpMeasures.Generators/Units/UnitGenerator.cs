namespace SharpMeasures.Generators.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Units.Pipeline;
using SharpMeasures.Generators.Units.Pipeline.ComparablePipeline;
using SharpMeasures.Generators.Units.Pipeline.DefinitionsPipeline;
using SharpMeasures.Generators.Units.Pipeline.DerivablePipeline;
using SharpMeasures.Generators.Units.Pipeline.MiscPipeline;

[Generator]
public class UnitGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<TypeDeclarationSyntax> firstStage = Stage1.Perform(context);
        IncrementalValuesProvider<Stage2.Result> secondStage = Stage2.Perform(context, firstStage);
        IncrementalValuesProvider<Stage3.Result> thirdStage = Stage3.Perform(context, secondStage);

        ComparableGenerator.Initialize(context, thirdStage);
        DefinitionsGenerator.Initialize(context, thirdStage);
        DerivableGenerator.Initialize(context, thirdStage);
        MiscGenerator.Initialize(context, thirdStage);
    }
}
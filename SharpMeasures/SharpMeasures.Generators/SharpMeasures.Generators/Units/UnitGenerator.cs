namespace SharpMeasures.Generators.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Pipeline;
using SharpMeasures.Generators.Units.Pipeline.ComparablePipeline;
using SharpMeasures.Generators.Units.Pipeline.UnitDefinitionsPipeline;
using SharpMeasures.Generators.Units.Pipeline.DerivableUnitPipeline;
using SharpMeasures.Generators.Units.Pipeline.MiscPipeline;

internal static class UnitGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<SharpMeasuresGenerator.Result> declarationAndSymbolProvider)
    {
        var firstStage = Stage1.ExtractRelevantDeclarations(context, declarationAndSymbolProvider);
        var secondStage = Stage2.ParseParameters(context, firstStage);
        var thirdStage = Stage3.AttachDocumentation(context, secondStage);

        ComparableGenerator.Initialize(context, thirdStage);
        UnitDefinitionsGenerator.Initialize(context, thirdStage);
        DerivableUnitGenerator.Initialize(context, thirdStage);
        MiscGenerator.Initialize(context, thirdStage);
    }
}
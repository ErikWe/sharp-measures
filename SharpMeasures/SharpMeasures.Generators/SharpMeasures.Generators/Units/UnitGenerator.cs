namespace SharpMeasures.Generators.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Pipeline;
using SharpMeasures.Generators.Units.Pipeline.ComparablePipeline;
using SharpMeasures.Generators.Units.Pipeline.UnitDefinitionsPipeline;
using SharpMeasures.Generators.Units.Pipeline.DerivableUnitPipeline;
using SharpMeasures.Generators.Units.Pipeline.MiscPipeline;

[Generator]
public class UnitGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var declarationsWithSymbols = DeclarationStage.ExtractRelevantPartialDeclarationsWithSymbols(context);
        var withParameters = ParameterStage.ParseGeneratedUnitParameters(context, declarationsWithSymbols);

        ComparableGenerator.Initialize(context, withParameters);
        UnitDefinitionsGenerator.Initialize(context, withParameters);
        DerivableUnitGenerator.Initialize(context, withParameters);
        MiscGenerator.Initialize(context, withParameters);
    }
}
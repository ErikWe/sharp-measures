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
        var withDocumentation = DocumentationStage.AppendDocumentation(context, withParameters);

        ComparableGenerator.Initialize(context, withDocumentation);
        UnitDefinitionsGenerator.Initialize(context, withDocumentation);
        DerivableUnitGenerator.Initialize(context, withDocumentation);
        MiscGenerator.Initialize(context, withDocumentation);
    }
}
namespace SharpMeasures.Generators.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars.Pipeline;
using SharpMeasures.Generators.Scalars.Pipeline.ComparablePipeline;
using SharpMeasures.Generators.Scalars.Pipeline.MiscPipeline;
using SharpMeasures.Generators.Scalars.Pipeline.StandardMathsPipeline;

[Generator]
public class ScalarQuantityGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var declarationsWithSymbols = DeclarationStage.ExtractRelevantPartialDeclarationsWithSymbols(context);
        var withParameters = ParameterStage.ParseGeneratedUnitParameters(context, declarationsWithSymbols);
        var withDocumentation = DocumentationStage.AppendDocumentation(context, withParameters);

        ComparableGenerator.Initialize(context, withDocumentation);
        MiscGenerator.Initialize(context, withDocumentation);
        StandardMathsGenerator.Initialize(context, withDocumentation);

        -> Todo:
        Units + ToString
        bases
        square root / square etc
        toVector
    }
}

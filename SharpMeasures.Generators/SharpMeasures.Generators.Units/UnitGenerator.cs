namespace SharpMeasures.Generators.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Units.Documentation;
using SharpMeasures.Generators.Units.Parsing;
using SharpMeasures.Generators.Units.SourceBuilding.Common;
using SharpMeasures.Generators.Units.SourceBuilding.Derivable;
using SharpMeasures.Generators.Units.SourceBuilding.UnitInstances;

using System.Threading;

public static class UnitGenerator
{
    public static void Generate(IncrementalGeneratorInitializationContext context, UnitValidationResult validationResult, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider)
    {
        var reduced = validationResult.UnitProvider.Combine(unitPopulationProvider, documentationDictionaryProvider, globalAnalyzerConfigProvider).Select(ReduceToDataModel);

        CommonGenerator.Initialize(context, reduced);
        DerivableUnitGenerator.Initialize(context, reduced);
        UnitInstancesGenerator.Initialize(context, reduced);
    }

    private static Optional<DataModel> ReduceToDataModel((Optional<UnitType> Unit, IUnitPopulation UnitPopulation, DocumentationDictionary DocumentationDictionary, GlobalAnalyzerConfig Config) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.Unit.HasValue is false)
        {
            return new Optional<DataModel>();
        }

        SourceBuildingContext sourceBuildingContext = new(input.Config.GeneratedFileHeaderLevel, GetDocumentationStrategy(input.Unit.Value, input.DocumentationDictionary, input.Config));
        return new DataModel(input.Unit.Value, input.UnitPopulation, sourceBuildingContext);
    }

    private static IDocumentationStrategy GetDocumentationStrategy(UnitType unit, DocumentationDictionary documentationDictionary, GlobalAnalyzerConfig config)
    {
        if (config.GenerateDocumentation is false)
        {
            return EmptyDocumentation.Instance;
        }

        DefaultDocumentation defaultDocumentation = new(unit);

        documentationDictionary.TryGetValue(unit.Type.QualifiedName, out var documentationFile);

        return new FileDocumentation(config.PrintDocumentationTags, documentationFile, defaultDocumentation);
    }
}

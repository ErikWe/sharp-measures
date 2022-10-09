namespace SharpMeasures.Generators.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Scalars.Parsing;
using SharpMeasures.Generators.Scalars.SourceBuilding.Common;
using SharpMeasures.Generators.Scalars.SourceBuilding.Conversions;
using SharpMeasures.Generators.Scalars.SourceBuilding.Maths;
using SharpMeasures.Generators.Scalars.SourceBuilding.Operations;
using SharpMeasures.Generators.Scalars.SourceBuilding.Processes;
using SharpMeasures.Generators.Scalars.SourceBuilding.Units;
using SharpMeasures.Generators.Scalars.SourceBuilding.Vectors;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System.Threading;

public static class ScalarGenerator
{
    public static void Generate(IncrementalGeneratorInitializationContext context, ScalarResolutionResult resolutionResult, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<IResolvedVectorPopulation> vectorPopulationProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider)
    {
        Generate(context, resolutionResult.ScalarBaseProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, documentationDictionaryProvider, globalAnalyzerConfigProvider);
        Generate(context, resolutionResult.ScalarSpecializationProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, documentationDictionaryProvider, globalAnalyzerConfigProvider);
    }

    private static void Generate(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<ResolvedScalarType>> scalars, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<IResolvedVectorPopulation> vectorPopulationProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider)
    {
        var reduced = scalars.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, documentationDictionaryProvider, globalAnalyzerConfigProvider).Select(ReduceToDataModel);

        CommonGenerator.Initialize(context, reduced);
        ConversionsGenerator.Initialize(context, reduced);
        OperationsGenerator.Initialize(context, reduced);
        MathsGenerator.Initialize(context, reduced);
        ProcessesGenerator.Initialize(context, reduced);
        UnitsGenerator.Initialize(context, reduced);
        VectorsGenerator.Initialize(context, reduced);
    }

    private static Optional<DataModel> ReduceToDataModel((Optional<ResolvedScalarType> Scalar, IUnitPopulation UnitPopulation, IResolvedScalarPopulation ScalarPopulation, IResolvedVectorPopulation VectorPopulation, DocumentationDictionary DocumentationDictionary, GlobalAnalyzerConfig Config) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.Scalar.HasValue is false)
        {
            return new Optional<DataModel>();
        }

        SourceBuildingContext sourceBuildingContext = new(input.Config.GeneratedFileHeaderContent, GetDocumentationStrategy(input.Scalar.Value, input.UnitPopulation, input.DocumentationDictionary, input.Config));
        return new DataModel(input.Scalar.Value, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation, sourceBuildingContext);
    }

    private static IDocumentationStrategy GetDocumentationStrategy(ResolvedScalarType scalar, IUnitPopulation unitPopulation, DocumentationDictionary documentationDictionary, GlobalAnalyzerConfig config)
    {
        var generateDocumentation = scalar.GenerateDocumentation ?? config.GenerateDocumentationByDefault;

        if (generateDocumentation is false)
        {
            return EmptyDocumentation.Instance;
        }

        DefaultDocumentation defaultDocumentation = new(scalar, unitPopulation);

        if (documentationDictionary.TryGetValue(scalar.Type.Name, out DocumentationFile documentationFile))
        {
            return new FileDocumentation(documentationFile, defaultDocumentation);
        }

        return defaultDocumentation;
    }
}

namespace SharpMeasures.Generators.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Documentation;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.SourceBuilding.Groups.MemberFactory;
using SharpMeasures.Generators.Vectors.SourceBuilding.Vectors.Common;
using SharpMeasures.Generators.Vectors.SourceBuilding.Vectors.Conversions;
using SharpMeasures.Generators.Vectors.SourceBuilding.Vectors.Maths;
using SharpMeasures.Generators.Vectors.SourceBuilding.Vectors.Operations;
using SharpMeasures.Generators.Vectors.SourceBuilding.Vectors.Processes;
using SharpMeasures.Generators.Vectors.SourceBuilding.Vectors.Units;

using System.Threading;

public static class VectorGenerator
{
    public static void Generate(IncrementalGeneratorInitializationContext context, VectorResolutionResult resolutionResult, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<IResolvedVectorPopulation> vectorPopulationProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider)
    {
        Generate(context, resolutionResult.GroupBaseProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, documentationDictionaryProvider, globalAnalyzerConfigProvider);
        Generate(context, resolutionResult.GroupSpecializationProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, documentationDictionaryProvider, globalAnalyzerConfigProvider);
        Generate(context, resolutionResult.GroupMemberProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, documentationDictionaryProvider, globalAnalyzerConfigProvider);
        Generate(context, resolutionResult.VectorBaseProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, documentationDictionaryProvider, globalAnalyzerConfigProvider);
        Generate(context, resolutionResult.VectorSpecializationProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, documentationDictionaryProvider, globalAnalyzerConfigProvider);
    }

    private static void Generate(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<ResolvedGroupType>> vectors, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<IResolvedVectorPopulation> vectorPopulationProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider)
    {
        var reduced = vectors.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, documentationDictionaryProvider, globalAnalyzerConfigProvider).Select(ReduceToDataModel);

        GroupMemberFactoryGenerator.Initialize(context, reduced);
    }

    private static void Generate(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<ResolvedVectorType>> vectors, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<IResolvedVectorPopulation> vectorPopulationProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider)
    {
        var reduced = vectors.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, documentationDictionaryProvider, globalAnalyzerConfigProvider).Select(ReduceToDataModel);

        VectorCommonGenerator.Initialize(context, reduced);
        VectorConversionsGenerator.Initialize(context, reduced);
        VectorMathsGenerator.Initialize(context, reduced);
        VectorOperationsGenerator.Initialize(context, reduced);
        VectorProcessesGenerator.Initialize(context, reduced);
        VectorUnitsGenerator.Initialize(context, reduced);
    }

    private static Optional<VectorDataModel> ReduceToDataModel((Optional<ResolvedVectorType> Vector, IUnitPopulation UnitPopulation, IResolvedScalarPopulation ScalarPopulation, IResolvedVectorPopulation VectorPopulation, DocumentationDictionary DocumentationDictionary, GlobalAnalyzerConfig Config) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.Vector.HasValue is false)
        {
            return new Optional<VectorDataModel>();
        }

        VectorSourceBuildingContext sourceBuildingContext = new(input.Config.GeneratedFileHeaderContent, GetDocumentationStrategy(input.Vector.Value, input.UnitPopulation, input.ScalarPopulation, input.DocumentationDictionary, input.Config));
        return new VectorDataModel(input.Vector.Value, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation, sourceBuildingContext);
    }

    private static Optional<GroupDataModel> ReduceToDataModel((Optional<ResolvedGroupType> Group, IUnitPopulation UnitPopulation, IResolvedScalarPopulation ScalarPopulation, IResolvedVectorPopulation VectorPopulation, DocumentationDictionary DocumentationDictionary, GlobalAnalyzerConfig Config) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.Group.HasValue is false)
        {
            return new Optional<GroupDataModel>();
        }

        GroupSourceBuildingContext sourceBuildingContext = new(input.Config.GeneratedFileHeaderContent, GetDocumentationStrategy(input.Group.Value, input.UnitPopulation, input.ScalarPopulation, input.DocumentationDictionary, input.Config));
        return new GroupDataModel(input.Group.Value, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation, sourceBuildingContext);
    }

    private static IVectorDocumentationStrategy GetDocumentationStrategy(ResolvedVectorType vector, IUnitPopulation unitPopulation, IResolvedScalarPopulation scalarPopulation, DocumentationDictionary documentationDictionary, GlobalAnalyzerConfig config)
    {
        if (config.GenerateDocumentation is false)
        {
            return EmptyDocumentation.Instance;
        }

        DefaultVectorDocumentation defaultDocumentation = new(vector, unitPopulation, scalarPopulation);

        documentationDictionary.TryGetValue(vector.Type.QualifiedName, out DocumentationFile documentationFile);

        return new VectorFileDocumentation(config.PrintDocumentationTags, vector.Dimension, documentationFile, defaultDocumentation);
    }

    private static IGroupDocumentationStrategy GetDocumentationStrategy(ResolvedGroupType group, IUnitPopulation unitPopulation, IResolvedScalarPopulation scalarPopulation, DocumentationDictionary documentationDictionary, GlobalAnalyzerConfig config)
    {
        if (config.GenerateDocumentation is false)
        {
            return EmptyDocumentation.Instance;
        }

        DefaultGroupDocumentation defaultDocumentation = new(group, unitPopulation, scalarPopulation);

        documentationDictionary.TryGetValue(group.Type.QualifiedName, out DocumentationFile documentationFile);

        return new GroupFileDocumentation(config.PrintDocumentationTags, documentationFile, defaultDocumentation);
    }
}

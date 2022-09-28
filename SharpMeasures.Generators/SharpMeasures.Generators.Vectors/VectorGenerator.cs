namespace SharpMeasures.Generators.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Documentation;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Pipelines.Groups.MemberFactory;
using SharpMeasures.Generators.Vectors.Pipelines.Vectors.Derivations;
using SharpMeasures.Generators.Vectors.Pipelines.Vectors.Common;
using SharpMeasures.Generators.Vectors.Pipelines.Vectors.Conversions;
using SharpMeasures.Generators.Vectors.Pipelines.Vectors.Maths;
using SharpMeasures.Generators.Vectors.Pipelines.Vectors.Processes;
using SharpMeasures.Generators.Vectors.Pipelines.Vectors.Units;

using System.Threading;

public static class VectorGenerator
{
    public static void Generate(IncrementalGeneratorInitializationContext context, VectorResolutionResult resolutionResult, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<IResolvedVectorPopulation> vectorPopulationProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        Generate(context, resolutionResult.GroupBaseProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider, documentationDictionaryProvider);
        Generate(context, resolutionResult.GroupSpecializationProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider, documentationDictionaryProvider);
        Generate(context, resolutionResult.GroupMemberProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider, documentationDictionaryProvider);
        Generate(context, resolutionResult.VectorBaseProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider, documentationDictionaryProvider);
        Generate(context, resolutionResult.VectorSpecializationProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider, documentationDictionaryProvider);
    }

    private static void Generate(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<ResolvedGroupType>> vectors, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<IResolvedVectorPopulation> vectorPopulationProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfigProvider.Select(ExtractDefaultGenerateDocumentation);

        var reduced = vectors.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(ReduceToDataModel) .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionaryProvider).Flatten().Select(AppendDocumentation);

        GroupMemberFactoryGenerator.Initialize(context, reduced);
    }

    private static void Generate(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<ResolvedVectorType>> vectors, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<IResolvedVectorPopulation> vectorPopulationProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfigProvider.Select(ExtractDefaultGenerateDocumentation);

        var reduced = vectors.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(ReduceToDataModel) .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionaryProvider).Flatten().Select(AppendDocumentation);

        VectorCommonGenerator.Initialize(context, reduced);
        VectorConversionsGenerator.Initialize(context, reduced);
        VectorDerivationsGenerator.Initialize(context, reduced);
        VectorMathsGenerator.Initialize(context, reduced);
        VectorProcessesGenerator.Initialize(context, reduced);
        VectorUnitsGenerator.Initialize(context, reduced);
    }

    private static Optional<GroupDataModel> ReduceToDataModel((Optional<ResolvedGroupType> Group, IUnitPopulation UnitPopulation, IResolvedScalarPopulation ScalarPopulation, IResolvedVectorPopulation VectorPopulation) input, CancellationToken _)
    {
        if (input.Group.HasValue is false)
        {
            return new Optional<GroupDataModel>();
        }

        return new GroupDataModel(input.Group.Value, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    private static Optional<VectorDataModel> ReduceToDataModel((Optional<ResolvedVectorType> Vector, IUnitPopulation UnitPopulation, IResolvedScalarPopulation ScalarPopulation, IResolvedVectorPopulation VectorPopulation) input, CancellationToken _)
    {
        if (input.Vector.HasValue is false)
        {
            return new Optional<VectorDataModel>();
        }

        return new VectorDataModel(input.Vector.Value, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    private static (Optional<GroupDataModel> Model, bool GenerateDocumentation) InterpretGenerateDocumentation((Optional<GroupDataModel> Model, bool Default) data, CancellationToken _)
    {
        if (data.Model.HasValue is false)
        {
            return (data.Model, false);
        }

        return (data.Model, data.Model.Value.Group.GenerateDocumentation ?? data.Default);
    }

    private static (Optional<VectorDataModel> Model, bool GenerateDocumentation) InterpretGenerateDocumentation((Optional<VectorDataModel> Model, bool Default) data, CancellationToken _)
    {
        if (data.Model.HasValue is false)
        {
            return (data.Model, false);
        }

        return (data.Model, data.Model.Value.Vector.GenerateDocumentation ?? data.Default);
    }

    private static Optional<GroupDataModel> AppendDocumentation((Optional<GroupDataModel> Model, bool GenerateDocumentation, DocumentationDictionary DocumentationDictionary) input, CancellationToken _)
    {
        if (input.GenerateDocumentation is false || input.Model.HasValue is false)
        {
            return input.Model;
        }

        DefaultGroupDocumentation defaultDocumentation = new(input.Model.Value);

        if (input.DocumentationDictionary.TryGetValue(input.Model.Value.Group.Type.Name, out DocumentationFile documentationFile))
        {
            return input.Model.Value with
            {
                Documentation = new GroupFileDocumentation(documentationFile, defaultDocumentation)
            };
        }

        return input.Model.Value with
        {
            Documentation = defaultDocumentation
        };
    }

    private static Optional<VectorDataModel> AppendDocumentation((Optional<VectorDataModel> Model, bool GenerateDocumentation, DocumentationDictionary DocumentationDictionary) input, CancellationToken _)
    {
        if (input.GenerateDocumentation is false)
        {
            return input.Model;
        }

        DefaultVectorDocumentation defaultDocumentation = new(input.Model.Value);

        if (input.DocumentationDictionary.TryGetValue(input.Model.Value.Vector.Type.Name, out DocumentationFile documentationFile))
        {
            return input.Model.Value with
            {
                Documentation = new VectorFileDocumentation(input.Model.Value, documentationFile, defaultDocumentation)
            };
        }

        return input.Model.Value with
        {
            Documentation = defaultDocumentation
        };
    }

    private static bool ExtractDefaultGenerateDocumentation(GlobalAnalyzerConfig config, CancellationToken _) => config.GenerateDocumentationByDefault;
}

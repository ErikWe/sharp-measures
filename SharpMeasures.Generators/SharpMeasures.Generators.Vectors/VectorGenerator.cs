namespace SharpMeasures.Generators.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Documentation;
using SharpMeasures.Generators.Vectors.Pipelines.Vector.Common;
using SharpMeasures.Generators.Vectors.Pipelines.Vector.Conversions;
using SharpMeasures.Generators.Vectors.Pipelines.Vector.Maths;
using SharpMeasures.Generators.Vectors.Pipelines.Vector.Units;

using System.Threading;

public interface IVectorGenerator
{
    public abstract void Generate(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<IResolvedVectorPopulation> vectorPopulationProvider,
        IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider);
}

internal class VectorGenerator : IVectorGenerator
{
    private IncrementalValuesProvider<Optional<ResolvedGroupType>> GroupBaseProvider { get; }
    private IncrementalValuesProvider<Optional<ResolvedGroupType>> GroupSpecializationProvider { get; }
    private IncrementalValuesProvider<Optional<ResolvedVectorType>> GroupMemberProvider { get; }

    private IncrementalValuesProvider<Optional<ResolvedVectorType>> VectorBaseProvider { get; }
    private IncrementalValuesProvider<Optional<ResolvedVectorType>> VectorSpecializationProvider { get; }
    
    internal VectorGenerator(IncrementalValuesProvider<Optional<ResolvedGroupType>> groupBaseProvider, IncrementalValuesProvider<Optional<ResolvedGroupType>> groupSpecializationProvider,
        IncrementalValuesProvider<Optional<ResolvedVectorType>> groupMemberProvider, IncrementalValuesProvider<Optional<ResolvedVectorType>> vectorBaseProvider, IncrementalValuesProvider<Optional<ResolvedVectorType>> vectorSpecializationProvider)
    {
        GroupBaseProvider = groupBaseProvider;
        GroupSpecializationProvider = groupSpecializationProvider;
        GroupMemberProvider = groupMemberProvider;

        VectorBaseProvider = vectorBaseProvider;
        VectorSpecializationProvider = vectorSpecializationProvider;
    }

    public void Generate(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<IResolvedVectorPopulation> vectorPopulationProvider,
        IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        Generate(GroupBaseProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider, documentationDictionaryProvider);
        Generate(GroupSpecializationProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider, documentationDictionaryProvider);
        Generate(context, GroupMemberProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider, documentationDictionaryProvider);
        Generate(context, VectorBaseProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider, documentationDictionaryProvider);
        Generate(context, VectorSpecializationProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider, documentationDictionaryProvider);
    }

    private static void Generate(IncrementalValuesProvider<Optional<ResolvedGroupType>> vectors,
        IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<IResolvedVectorPopulation> vectorPopulationProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider,
        IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfigProvider.Select(ExtractDefaultGenerateDocumentation);

        var reduced = vectors.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(ReduceToDataModel)
            .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionaryProvider).Flatten().Select(AppendDocumentation);
    }

    private static void Generate(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<ResolvedVectorType>> vectors,
        IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<IResolvedVectorPopulation> vectorPopulationProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider,
        IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfigProvider.Select(ExtractDefaultGenerateDocumentation);

        var reduced = vectors.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(ReduceToDataModel)
            .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionaryProvider).Flatten().Select(AppendDocumentation);

        VectorCommonGenerator.Initialize(context, reduced);
        VectorConversionsGenerator.Initialize(context, reduced);
        VectorMathsGenerator.Initialize(context, reduced);
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

        return (data.Model, data.Model.Value.VectorGroup.GenerateDocumentation ?? data.Default);
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

        DefaultGroupDocumentation defaultDocumentation = new();

        if (input.DocumentationDictionary.TryGetValue(input.Model.Value.VectorGroup.Type.Name, out DocumentationFile documentationFile))
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

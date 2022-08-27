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
        IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider,
        IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider);
}

internal class VectorGenerator : IVectorGenerator
{
    public IncrementalValueProvider<IResolvedVectorPopulation> VectorPopulationProvider { get; }

    private IncrementalValuesProvider<ResolvedGroupType> GroupBaseProvider { get; }
    private IncrementalValuesProvider<ResolvedGroupType> GroupSpecializationProvider { get; }
    private IncrementalValuesProvider<ResolvedVectorType> GroupMemberProvider { get; }

    private IncrementalValuesProvider<ResolvedVectorType> VectorBaseProvider { get; }
    private IncrementalValuesProvider<ResolvedVectorType> VectorSpecializationProvider { get; }
    
    internal VectorGenerator(IncrementalValueProvider<IResolvedVectorPopulation> vectorPopulationProvider, IncrementalValuesProvider<ResolvedGroupType> groupBaseProvider,
        IncrementalValuesProvider<ResolvedGroupType> groupSpecializationProvider, IncrementalValuesProvider<ResolvedVectorType> groupMemberProvider,
        IncrementalValuesProvider<ResolvedVectorType> vectorBaseProvider, IncrementalValuesProvider<ResolvedVectorType> vectorSpecializationProvider)
    {
        VectorPopulationProvider = vectorPopulationProvider;

        GroupBaseProvider = groupBaseProvider;
        GroupSpecializationProvider = groupSpecializationProvider;
        GroupMemberProvider = groupMemberProvider;

        VectorBaseProvider = vectorBaseProvider;
        VectorSpecializationProvider = vectorSpecializationProvider;
    }

    public void Generate(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        Generate(GroupBaseProvider, unitPopulationProvider, scalarPopulationProvider, VectorPopulationProvider, globalAnalyzerConfigProvider,
            documentationDictionaryProvider);

        Generate(GroupSpecializationProvider, unitPopulationProvider, scalarPopulationProvider, VectorPopulationProvider, globalAnalyzerConfigProvider,
            documentationDictionaryProvider);

        Generate(context, GroupMemberProvider, unitPopulationProvider, scalarPopulationProvider, VectorPopulationProvider, globalAnalyzerConfigProvider,
            documentationDictionaryProvider);

        Generate(context, VectorBaseProvider, unitPopulationProvider, scalarPopulationProvider, VectorPopulationProvider, globalAnalyzerConfigProvider,
            documentationDictionaryProvider);

        Generate(context, VectorSpecializationProvider, unitPopulationProvider, scalarPopulationProvider, VectorPopulationProvider, globalAnalyzerConfigProvider,
            documentationDictionaryProvider);
    }

    private static void Generate(IncrementalValuesProvider<ResolvedGroupType> vectors,
        IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<IResolvedVectorPopulation> vectorPopulationProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider,
        IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfigProvider.Select(ExtractDefaultGenerateDocumentation);

        var reduced = vectors.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(ReduceToDataModel)
            .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionaryProvider).Flatten().Select(AppendDocumentation);
    }

    private static void Generate(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<ResolvedVectorType> vectors,
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

    private static GroupDataModel ReduceToDataModel
        ((ResolvedGroupType Group, IUnitPopulation UnitPopulation, IResolvedScalarPopulation ScalarPopulation, IResolvedVectorPopulation VectorPopulation) input, CancellationToken _)
    {
        return new(input.Group, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    private static VectorDataModel ReduceToDataModel
        ((ResolvedVectorType Vector, IUnitPopulation UnitPopulation, IResolvedScalarPopulation ScalarPopulation, IResolvedVectorPopulation VectorPopulation) input, CancellationToken _)
    {
        return new(input.Vector, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    private static (GroupDataModel Model, bool GenerateDocumentation) InterpretGenerateDocumentation((GroupDataModel Model, bool Default) data, CancellationToken _)
    {
        return (data.Model, data.Model.VectorGroup.GenerateDocumentation ?? data.Default);
    }

    private static (VectorDataModel Model, bool GenerateDocumentation) InterpretGenerateDocumentation((VectorDataModel Model, bool Default) data, CancellationToken _)
    {
        return (data.Model, data.Model.Vector.GenerateDocumentation ?? data.Default);
    }

    private static GroupDataModel AppendDocumentation((GroupDataModel Model, bool GenerateDocumentation, DocumentationDictionary DocumentationDictionary) input, CancellationToken _)
    {
        if (input.GenerateDocumentation is false)
        {
            return input.Model;
        }

        DefaultGroupDocumentation defaultDocumentation = new();

        if (input.DocumentationDictionary.TryGetValue(input.Model.VectorGroup.Type.Name, out DocumentationFile documentationFile))
        {
            return input.Model with
            {
                Documentation = new GroupFileDocumentation(documentationFile, defaultDocumentation)
            };
        }

        return input.Model with
        {
            Documentation = defaultDocumentation
        };
    }

    private static VectorDataModel AppendDocumentation
        ((VectorDataModel Model, bool GenerateDocumentation, DocumentationDictionary DocumentationDictionary) input, CancellationToken _)
    {
        if (input.GenerateDocumentation is false)
        {
            return input.Model;
        }

        DefaultVectorDocumentation defaultDocumentation = new(input.Model);

        if (input.DocumentationDictionary.TryGetValue(input.Model.Vector.Type.Name, out DocumentationFile documentationFile))
        {
            return input.Model with
            {
                Documentation = new VectorFileDocumentation(input.Model, documentationFile, defaultDocumentation)
            };
        }

        return input.Model with
        {
            Documentation = defaultDocumentation
        };
    }

    private static bool ExtractDefaultGenerateDocumentation(GlobalAnalyzerConfig config, CancellationToken _) => config.GenerateDocumentationByDefault;
}

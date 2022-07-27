namespace SharpMeasures.Generators.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Documentation;
using SharpMeasures.Generators.Vectors.Pipelines.IndividualVector.Common;
using SharpMeasures.Generators.Vectors.Pipelines.IndividualVector.Conversions;
using SharpMeasures.Generators.Vectors.Pipelines.IndividualVector.Maths;
using SharpMeasures.Generators.Vectors.Pipelines.IndividualVector.Units;

using System;
using System.Threading;

public interface IVectorGenerator
{
    public abstract void Perform(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider,
        IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider);
}

internal class VectorGenerator : IVectorGenerator
{
    private IncrementalValuesProvider<VectorGroupType> VectorGroupBaseProvider { get; }
    private IncrementalValuesProvider<VectorGroupType> VectorGroupSpecializationProvider { get; }
    private IncrementalValuesProvider<VectorGroupMemberType> VectorGroupMemberProvider { get; }

    private IncrementalValuesProvider<IndividualVectorType> IndividualVectorBaseProvider { get; }
    private IncrementalValuesProvider<IndividualVectorType> IndividualVectorSpecializationProvider { get; }
    
    internal VectorGenerator(IncrementalValuesProvider<VectorGroupType> vectorGroupBaseProvider,
        IncrementalValuesProvider<VectorGroupType> vectorGroupSpecializationProvider,
        IncrementalValuesProvider<VectorGroupMemberType> vectorGroupMemberProvider,
        IncrementalValuesProvider<IndividualVectorType> individualVectorBaseProvider,
        IncrementalValuesProvider<IndividualVectorType> individualVectorSpecializationProvider)
    {
        VectorGroupBaseProvider = vectorGroupBaseProvider;
        VectorGroupSpecializationProvider = vectorGroupSpecializationProvider;
        VectorGroupMemberProvider = vectorGroupMemberProvider;

        IndividualVectorBaseProvider = individualVectorBaseProvider;
        IndividualVectorSpecializationProvider = individualVectorSpecializationProvider;
    }

    public void Perform(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider,
        IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        Perform(context, VectorGroupBaseProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider,
            documentationDictionaryProvider);

        Perform(context, VectorGroupSpecializationProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider,
            documentationDictionaryProvider);

        Perform(context, VectorGroupMemberProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider,
            documentationDictionaryProvider);

        Perform(context, IndividualVectorBaseProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider,
            documentationDictionaryProvider);

        Perform(context, IndividualVectorSpecializationProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider,
            documentationDictionaryProvider);
    }

    private static void Perform(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<VectorGroupType> vectors,
        IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider,
        IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfigProvider.Select(ExtractDefaultGenerateDocumentation);

        var reduced = vectors.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(ReduceToDataModel)
            .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionaryProvider).Flatten().Select(AppendDocumentation);
    }

    private static void Perform(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<IndividualVectorType> vectors,
        IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider,
        IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfigProvider.Select(ExtractDefaultGenerateDocumentation);

        var reduced = vectors.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(ReduceToDataModel)
            .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionaryProvider).Flatten().Select(AppendDocumentation);

        IndividualVectorCommonGenerator.Initialize(context, reduced);
        IndividualVectorConversionsGenerator.Initialize(context, reduced);
        IndividualVectorMathsGenerator.Initialize(context, reduced);
        IndividualVectorUnitsGenerator.Initialize(context, reduced);
    }

    private static void Perform(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<VectorGroupMemberType> members,
        IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider,
        IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        Perform(context, members.Select(static (member, _) => member as IndividualVectorType), unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider,
            globalAnalyzerConfigProvider, documentationDictionaryProvider);
    }

    private static VectorGroupDataModel ReduceToDataModel
        ((VectorGroupType VectorGroup, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken _)
    {
        return new(input.VectorGroup, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    private static IndividualVectorDataModel ReduceToDataModel
        ((IndividualVectorType IndividualVector, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken _)
    {
        return new(input.IndividualVector, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    private static (VectorGroupDataModel Model, bool GenerateDocumentation) InterpretGenerateDocumentation((VectorGroupDataModel Model, bool Default) data, CancellationToken _)
    {
        return (data.Model, data.Model.Vector.Definition.GenerateDocumentation ?? data.Default);
    }

    private static (IndividualVectorDataModel Model, bool GenerateDocumentation) InterpretGenerateDocumentation((IndividualVectorDataModel Model, bool Default) data, CancellationToken _)
    {
        return (data.Model, data.Model.Vector.Definition.GenerateDocumentation ?? data.Default);
    }

    private static VectorGroupDataModel AppendDocumentation
        ((VectorGroupDataModel Model, bool GenerateDocumentation, DocumentationDictionary DocumentationDictionary) input, CancellationToken _)
    {
        return AppendDocumentation<VectorGroupDataModel, IVectorGroupType, IVectorGroupDocumentationStrategy>(input.Model, input.GenerateDocumentation,
            input.DocumentationDictionary, static (model) => new DefaultVectorGroupDocumentation(model),
            static (file, documentation) => new VectorGroupFileDocumentation(file, documentation));
    }

    private static IndividualVectorDataModel AppendDocumentation
        ((IndividualVectorDataModel Model, bool GenerateDocumentation, DocumentationDictionary DocumentationDictionary) input, CancellationToken _)
    {
        return AppendDocumentation<IndividualVectorDataModel, IIndividualVectorType, IIndividualVectorDocumentationStrategy>(input.Model, input.GenerateDocumentation,
            input.DocumentationDictionary, static (model) => new DefaultIndividualVectorDocumentation(model),
            (file, documentation) => new IndividualVectorFileDocumentation(input.Model.Vector.Definition.Dimension, file, documentation));
    }

    private static TDataModel AppendDocumentation<TDataModel, TVectorType, TDocumentation>
        (TDataModel model, bool generateDocumentation, DocumentationDictionary documentationDictionary, Func<TDataModel, TDocumentation> defaultDocumentationDelegate,
        Func<DocumentationFile, TDocumentation, TDocumentation> fileDocumentationDelegate)
        where TDataModel : ADataModel<TVectorType, TDocumentation>
        where TVectorType : IVectorGroupType
    {
        if (generateDocumentation)
        {
            TDocumentation defaultDocumentation = defaultDocumentationDelegate(model);

            if (documentationDictionary.TryGetValue(model.Vector.Type.Name, out DocumentationFile documentationFile))
            {
                model = model with
                {
                    Documentation = fileDocumentationDelegate(documentationFile, defaultDocumentation)
                };
            }
            else
            {
                model = model with
                {
                    Documentation = defaultDocumentation
                };
            }
        }

        return model;
    }

    private static bool ExtractDefaultGenerateDocumentation(GlobalAnalyzerConfig config, CancellationToken _) => config.GenerateDocumentationByDefault;
}

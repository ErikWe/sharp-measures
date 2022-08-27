namespace SharpMeasures.Generators.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units.Documentation;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Pipelines.Common;
using SharpMeasures.Generators.Units.Pipelines.Derivable;
using SharpMeasures.Generators.Units.Pipelines.UnitDefinitions;

using System.Threading;

public interface IUnitGenerator
{
    public abstract void Generate(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider);
}

internal class UnitGenerator : IUnitGenerator
{
    private IncrementalValueProvider<IUnitPopulationWithData> UnitPopulationProvider { get; }

    private IncrementalValuesProvider<UnitType> UnitProvider { get; }

    internal UnitGenerator(IncrementalValueProvider<IUnitPopulationWithData> unitPopulationProvider, IncrementalValuesProvider<UnitType> unitProvider)
    {
        UnitPopulationProvider = unitPopulationProvider;

        UnitProvider = unitProvider;
    }

    public void Generate(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfigProvider.Select(ExtractDefaultGenerateDocumentation);

        var minimized = UnitProvider.Combine(UnitPopulationProvider, scalarPopulationProvider).Select(ReduceToDataModel)
            .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionaryProvider).Flatten()
            .Select(AppendDocumentationFile);

        CommonGenerator.Initialize(context, minimized);
        DerivableUnitGenerator.Initialize(context, minimized);
        UnitDefinitionsGenerator.Initialize(context, minimized);
    }

    private static DataModel ReduceToDataModel((UnitType Unit, IUnitPopulationWithData UnitPopulation, IResolvedScalarPopulation ScalarPopulation) input, CancellationToken _)
    {
        return new(input.Unit, input.UnitPopulation);
    }

    private static (DataModel Model, bool GenerateDocumentation) InterpretGenerateDocumentation((DataModel Model, bool Default) data, CancellationToken _)
    {
        return (data.Model, data.Model.Unit.Definition.GenerateDocumentation ?? data.Default);
    }

    private static DataModel AppendDocumentationFile((DataModel Model, bool GenerateDocumentation, DocumentationDictionary DocumentationDictionary) input, CancellationToken _)
    {
        if (input.GenerateDocumentation is false)
        {
            return input.Model;
        }

        DefaultDocumentation defaultDocumentation = new(input.Model);

        if (input.DocumentationDictionary.TryGetValue(input.Model.Unit.Type.Name, out DocumentationFile documentationFile))
        {
            return input.Model with
            {
                Documentation = new FileDocumentation(documentationFile, defaultDocumentation)
            };
        }

        return input.Model with
        {
            Documentation = defaultDocumentation
        };
    }

    private static bool ExtractDefaultGenerateDocumentation(GlobalAnalyzerConfig config, CancellationToken _) => config.GenerateDocumentationByDefault;
}

namespace SharpMeasures.Generators.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units.Documentation;
using SharpMeasures.Generators.Units.Pipelines.Common;
using SharpMeasures.Generators.Units.Pipelines.Derivable;
using SharpMeasures.Generators.Units.Pipelines.UnitInstances;

using System.Threading;

public interface IUnitGenerator
{
    public abstract void Generate(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider);
}

internal sealed class UnitGenerator : IUnitGenerator
{
    private IncrementalValuesProvider<Optional<UnitType>> UnitProvider { get; }

    internal UnitGenerator(IncrementalValuesProvider<Optional<UnitType>> unitProvider)
    {
        UnitProvider = unitProvider;
    }

    public void Generate(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfigProvider.Select(ExtractDefaultGenerateDocumentation);

        var minimized = UnitProvider.Combine(unitPopulationProvider, scalarPopulationProvider).Select(ReduceToDataModel)
            .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionaryProvider).Flatten()
            .Select(AppendDocumentationFile);

        CommonGenerator.Initialize(context, minimized);
        DerivableUnitGenerator.Initialize(context, minimized);
        UnitInstancesGenerator.Initialize(context, minimized);
    }

    private static Optional<DataModel> ReduceToDataModel((Optional<UnitType> Unit, IUnitPopulation UnitPopulation, IResolvedScalarPopulation ScalarPopulation) input, CancellationToken _)
    {
        if (input.Unit.HasValue is false)
        {
            return new Optional<DataModel>();
        }

        return new DataModel(input.Unit.Value, input.UnitPopulation);
    }

    private static (Optional<DataModel> Model, bool GenerateDocumentation) InterpretGenerateDocumentation((Optional<DataModel> Model, bool Default) data, CancellationToken _)
    {
        if (data.Model.HasValue is false)
        {
            return (new Optional<DataModel>(), false);
        }

        return (data.Model, data.Model.Value.Unit.Definition.GenerateDocumentation ?? data.Default);
    }

    private static Optional<DataModel> AppendDocumentationFile((Optional<DataModel> Model, bool GenerateDocumentation, DocumentationDictionary DocumentationDictionary) input, CancellationToken _)
    {
        if (input.GenerateDocumentation is false || input.Model.HasValue is false)
        {
            return input.Model;
        }

        DefaultDocumentation defaultDocumentation = new(input.Model.Value);

        if (input.DocumentationDictionary.TryGetValue(input.Model.Value.Unit.Type.Name, out DocumentationFile documentationFile))
        {
            return input.Model.Value with
            {
                Documentation = new FileDocumentation(documentationFile, defaultDocumentation)
            };
        }

        return input.Model.Value with
        {
            Documentation = defaultDocumentation
        };
    }

    private static bool ExtractDefaultGenerateDocumentation(GlobalAnalyzerConfig config, CancellationToken _) => config.GenerateDocumentationByDefault;
}

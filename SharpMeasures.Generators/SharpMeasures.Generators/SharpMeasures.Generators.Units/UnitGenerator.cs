namespace SharpMeasures.Generators.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units.Documentation;
using SharpMeasures.Generators.Units.Pipelines.Common;
using SharpMeasures.Generators.Units.Pipelines.Derivable;
using SharpMeasures.Generators.Units.Pipelines.UnitDefinitions;

using System.Threading;

public class UnitGenerator
{
    private IncrementalValuesProvider<UnitType> UnitProvider { get; }

    internal UnitGenerator(IncrementalValuesProvider<UnitType> unitProvider)
    {
        UnitProvider = unitProvider;
    }

    public void Perform(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider,
        IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfigProvider.Select(ExtractDefaultGenerateDocumentation);

        var minimized = UnitProvider.Combine(unitPopulationProvider, scalarPopulationProvider).Select(ReduceToDataModel)
            .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionaryProvider).Flatten()
            .Select(AppendDocumentationFile);

        CommonGenerator.Initialize(context, minimized);
        DerivableUnitGenerator.Initialize(context, minimized);
        UnitDefinitionsGenerator.Initialize(context, minimized);
    }

    private static DataModel ReduceToDataModel((UnitType Unit, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation) input, CancellationToken _)
    {
        return new(input.Unit, input.UnitPopulation);
    }

    private static (DataModel Model, bool GenerateDocumentation) InterpretGenerateDocumentation((DataModel Model, bool Default) data, CancellationToken _)
    {
        return (data.Model, data.Model.Unit.Definition.GenerateDocumentation ?? data.Default);
    }

    private static DataModel AppendDocumentationFile
        ((DataModel Model, bool GenerateDocumentation, DocumentationDictionary DocumentationDictionary) input, CancellationToken _)
    {
        if (input.GenerateDocumentation)
        {
            DefaultDocumentation defaultDocumentation = new(input.Model);

            if (input.DocumentationDictionary.TryGetValue(input.Model.Unit.Type.Name, out DocumentationFile documentationFile))
            {
                input.Model = input.Model with
                {
                    Documentation = new FileDocumentation(documentationFile, defaultDocumentation)
                };
            }
            else
            {
                input.Model = input.Model with
                {
                    Documentation = defaultDocumentation
                };
            }
        }

        return input.Model;
    }

    private static bool ExtractDefaultGenerateDocumentation(GlobalAnalyzerConfig config, CancellationToken _) => config.GenerateDocumentationByDefault;
}

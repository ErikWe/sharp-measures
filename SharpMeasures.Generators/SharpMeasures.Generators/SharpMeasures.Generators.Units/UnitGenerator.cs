namespace SharpMeasures.Generators.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units.Diagnostics;
using SharpMeasures.Generators.Units.Documentation;
using SharpMeasures.Generators.Units.Parsing;
using SharpMeasures.Generators.Units.Pipelines.Common;
using SharpMeasures.Generators.Units.Pipelines.Derivable;
using SharpMeasures.Generators.Units.Pipelines.UnitDefinitions;
using SharpMeasures.Generators.Units.Refinement.SharpMeasuresUnit;

using System.Threading;

public class UnitGenerator
{
    public IncrementalValueProvider<UnitPopulation> PopulationProvider { get; }
    private IncrementalValuesProvider<ParsedUnit> UnitProvider { get; }

    internal UnitGenerator(IncrementalValueProvider<UnitPopulation> populationProvider, IncrementalValuesProvider<ParsedUnit> unitProvider)
    {
        PopulationProvider = populationProvider;
        UnitProvider = unitProvider;
    }

    public void Perform(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<ScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfigProvider.Select(ExtractDefaultGenerateDocumentation);

        var minimized = UnitProvider.Combine(PopulationProvider, scalarPopulationProvider).Select(ReduceToDataModel).ReportDiagnostics(context)
            .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionaryProvider).Flatten()
            .Select(AppendDocumentationFile);

        CommonGenerator.Initialize(context, minimized);
        DerivableUnitGenerator.Initialize(context, minimized);
        UnitDefinitionsGenerator.Initialize(context, minimized);
    }

    private static IOptionalWithDiagnostics<DataModel> ReduceToDataModel
        ((ParsedUnit Unit, UnitPopulation UnitPopulation, ScalarPopulation ScalarPopulation) input, CancellationToken _)
    {
        SharpMeasuresUnitRefinementContext context = new(input.Unit.UnitType, input.ScalarPopulation);
        SharpMeasuresUnitRefiner refiner = new(SharpMeasuresUnitDiagnostics.Instance);

        var processedSharpMeasuresUnit = refiner.Process(context, input.Unit.UnitDefinition);

        if (processedSharpMeasuresUnit.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<DataModel>(processedSharpMeasuresUnit.Diagnostics);
        }

        DataModel model = new(processedSharpMeasuresUnit.Result, input.Unit, input.UnitPopulation);

        return OptionalWithDiagnostics.Result(model, processedSharpMeasuresUnit.Diagnostics);
    }

    private static (DataModel Model, bool GenerateDocumentation) InterpretGenerateDocumentation((DataModel Model, bool Default) data, CancellationToken _)
    {
        if (data.Model.UnitData.UnitDefinition.Locations.ExplicitlySetGenerateDocumentation)
        {
            return (data.Model, data.Model.UnitData.UnitDefinition.GenerateDocumentation);
        }

        return (data.Model, data.Default);
    }

    private static DataModel AppendDocumentationFile
        ((DataModel Model, bool GenerateDocumentation, DocumentationDictionary DocumentationDictionary) input, CancellationToken _)
    {
        if (input.GenerateDocumentation)
        {
            DefaultDocumentation defaultDocumentation = new(input.Model);

            if (input.DocumentationDictionary.TryGetValue(input.Model.UnitData.UnitType.Name, out DocumentationFile documentationFile))
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

    private readonly record struct SharpMeasuresUnitRefinementContext : ISharpMeasuresUnitRefinementContext
    {
        public DefinedType Type { get; }

        public ScalarPopulation ScalarPopulation { get; }

        public SharpMeasuresUnitRefinementContext(DefinedType type, ScalarPopulation scalarPopulation)
        {
            Type = type;
            ScalarPopulation = scalarPopulation;
        }
    }
}

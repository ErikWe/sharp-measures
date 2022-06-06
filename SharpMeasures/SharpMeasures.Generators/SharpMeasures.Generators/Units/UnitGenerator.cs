namespace SharpMeasures.Generators.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.AnalyzerConfig;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units.Parsing;
using SharpMeasures.Generators.Units.Pipelines.Common;
using SharpMeasures.Generators.Units.Pipelines.Derivable;
using SharpMeasures.Generators.Units.Pipelines.UnitDefinitions;
using SharpMeasures.Generators.Units.Processing;

using System.Threading;

internal static class UnitGenerator
{
    public static void Attach(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<ParsedUnit> units,
        IncrementalValueProvider<UnitPopulation> unitPopulation, IncrementalValueProvider<ScalarPopulation> scalarPopulation,
        IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfig, IncrementalValueProvider<DocumentationDictionary> documentationDictionary)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfig.Select(ExtractDefaultGenerateDocumentation);

        var minimized = units.Combine(unitPopulation, scalarPopulation).Select(ReduceToDataModel).ReportDiagnostics(context)
            .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionary).Flatten()
            .Select(AppendDocumentationFile).ReportDiagnostics(context);

        CommonGenerator.Initialize(context, minimized);
        DerivableUnitGenerator.Initialize(context, minimized);
        UnitDefinitionsGenerator.Initialize(context, minimized);
    }

    private static IOptionalWithDiagnostics<DataModel> ReduceToDataModel
        ((ParsedUnit Unit, UnitPopulation UnitPopulation, ScalarPopulation ScalarPopulation) input, CancellationToken _)
    {
        GeneratedUnitProcessingContext context = new(input.Unit.UnitType, input.ScalarPopulation);
        var processedGeneratedUnit = GeneratedUnitProcesser.Instance.Process(context, input.Unit.UnitDefinition);

        if (processedGeneratedUnit.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<DataModel>(processedGeneratedUnit.Diagnostics);
        }

        DataModel model = new(input.Unit, processedGeneratedUnit.Result.Quantity, input.UnitPopulation);

        return OptionalWithDiagnostics.Result(model, processedGeneratedUnit.Diagnostics);
    }

    private static (DataModel Model, bool GenerateDocumentation) InterpretGenerateDocumentation((DataModel Model, bool Default) data, CancellationToken _)
    {
        if (data.Model.Unit.UnitDefinition.Locations.ExplicitlySetGenerateDocumentation)
        {
            return (data.Model, data.Model.Unit.UnitDefinition.GenerateDocumentation);
        }

        return (data.Model, data.Default);
    }

    private static IResultWithDiagnostics<DataModel> AppendDocumentationFile
        ((DataModel Model, bool GenerateDocumentation, DocumentationDictionary DocumentationDictionary) input, CancellationToken _)
    {
        if (input.GenerateDocumentation)
        {
            if (input.DocumentationDictionary.TryGetValue(input.Model.Unit.UnitType.Name, out DocumentationFile documentationFile))
            {
                DataModel modifiedModel = input.Model with { Documentation = documentationFile };
                return ResultWithDiagnostics.Construct(modifiedModel);
            }

            Diagnostic diagnostics = DiagnosticConstruction.NoMatchingDocumentationFile(input.Model.Unit.UnitLocation.AsRoslynLocation(),
                input.Model.Unit.UnitType.Name);

            return ResultWithDiagnostics.Construct(input.Model, diagnostics);
        }

        return ResultWithDiagnostics.Construct(input.Model);
    }

    private static bool ExtractDefaultGenerateDocumentation(GlobalAnalyzerConfig config, CancellationToken _) => config.GenerateDocumentationByDefault;
}

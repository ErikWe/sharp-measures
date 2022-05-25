namespace SharpMeasures.Generators.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.AnalyzerConfig;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Parsing.Scalars;
using SharpMeasures.Generators.Units.Parsing;
using SharpMeasures.Generators.Units.Validation;

using System.Threading;

internal static class UnitGenerator
{
    public static void Attach(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<ParsedUnit> units,
        IncrementalValueProvider<NamedTypePopulation<UnitInterface>> unitPopulation, IncrementalValueProvider<NamedTypePopulation<ScalarInterface>> scalarPopulation,
        IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfig, IncrementalValueProvider<DocumentationDictionary> documentationDictionary)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfig.Select(ExtractDefaultGenerateDocumentation);

        var populations = unitPopulation.Combine(scalarPopulation);

        var minimized = units.Combine(populations).Select(ValidateAndReduceToDataModel).ReportDiagnostics(context).Combine(defaultGenerateDocumentation)
            .Select(InterpretGenerateDocumentation).Combine(documentationDictionary).Select(AppendDocumentationFile).ReportDiagnostics(context);
    }

    private static IOptionalWithDiagnostics<DataModel> ValidateAndReduceToDataModel
        ((ParsedUnit Unit, (NamedTypePopulation<UnitInterface> Unit, NamedTypePopulation<ScalarInterface> Scalar) Populations) data, CancellationToken _)
    {
        GeneratedUnitValidatorContext context = new(data.Unit.UnitType, data.Populations.Scalar);

        if (ExternalGeneratedUnitValidator.Instance.CheckValidity(context, data.Unit.UnitDefinition) is IValidityWithDiagnostics { IsInvalid: true } invalid)
        {
            return OptionalWithDiagnostics.Empty<DataModel>(invalid.Diagnostics);
        }

        var quantity = data.Populations.Scalar.Population[data.Unit.UnitDefinition.Quantity];

        return OptionalWithDiagnostics.Result(new DataModel(data.Unit, quantity, data.Populations.Unit));
    }

    private static (DataModel Model, bool GenerateDocumentation) InterpretGenerateDocumentation((DataModel Model, bool Default) data, CancellationToken _)
    {
        if (data.Model.Unit.UnitDefinition.ParsingData.ExplicitlySetGenerateDocumentation)
        {
            return (data.Model, data.Model.Unit.UnitDefinition.GenerateDocumentation);
        }

        return (data.Model, data.Default);
    }

    private static IResultWithDiagnostics<DataModel> AppendDocumentationFile
        (((DataModel Model, bool GenerateDocumentation) State, DocumentationDictionary DocumentationDictionary) data, CancellationToken _)
    {
        if (data.State.GenerateDocumentation)
        {
            if (data.DocumentationDictionary.TryGetValue(data.State.Model.Unit.UnitType.Name, out DocumentationFile documentationFile))
            {
                DataModel modifiedModel = data.State.Model with { Documentation = documentationFile };
                return ResultWithDiagnostics.WithoutDiagnostics(modifiedModel);
            }

            Diagnostic diagnostics = DiagnosticConstruction.NoMatchingDocumentationFile(data.State.Model.Unit.UnitLocation.AsRoslynLocation(),
                data.State.Model.Unit.UnitType.Name);

            return ResultWithDiagnostics.Construct(data.State.Model, diagnostics);
        }

        return ResultWithDiagnostics.WithoutDiagnostics(data.State.Model);
    }

    private static bool ExtractDefaultGenerateDocumentation(GlobalAnalyzerConfig config, CancellationToken _) => config.GenerateDocumentationByDefault;
}
namespace SharpMeasures.Generators.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.AnalyzerConfig;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars.Parsing;
using SharpMeasures.Generators.Scalars.Pipelines.Comparable;
using SharpMeasures.Generators.Scalars.Pipelines.DimensionalEquivalence;
using SharpMeasures.Generators.Scalars.Pipelines.Misc;
using SharpMeasures.Generators.Scalars.Pipelines.StandardMaths;
using SharpMeasures.Generators.Scalars.Pipelines.Units;
using SharpMeasures.Generators.Scalars.Pipelines.Vector;
using SharpMeasures.Generators.Scalars.Processing;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System.Threading;

internal static class ScalarQuantityGenerator
{
    public static void Attach(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<ParsedScalar> scalars,
        IncrementalValueProvider<NamedTypePopulation<UnitInterface>> unitPopulation, IncrementalValueProvider<NamedTypePopulation<ScalarInterface>> scalarPopulation,
        IncrementalValueProvider<VectorPopulation> vectorPopulation, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfig,
        IncrementalValueProvider<DocumentationDictionary> documentationDictionary)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfig.Select(ExtractDefaultGenerateDocumentation);

        var minimized = scalars.Combine(unitPopulation, scalarPopulation, vectorPopulation).Select(ReduceToDataModel).ReportDiagnostics(context)
            .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionary).Flatten().Select(AppendDocumentationFile)
            .ReportDiagnostics(context);

        ComparableGenerator.Initialize(context, minimized);
        DimensionalEquivalenceGenerator.Initialize(context, minimized);
        MiscGenerator.Initialize(context, minimized);
        StandardMathsGenerator.Initialize(context, minimized);
        UnitsGenerator.Initialize(context, minimized);
        VectorGenerator.Initialize(context, minimized);
    }

    private static IOptionalWithDiagnostics<DataModel> ReduceToDataModel
        ((ParsedScalar Scalar, NamedTypePopulation<UnitInterface> UnitPopulation, NamedTypePopulation<ScalarInterface> ScalarPopulation,
        VectorPopulation VectorPopulation) input, CancellationToken _)
    {
        GeneratedScalarProcessingContext context = new(input.Scalar.ScalarType, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
        var processedGeneratedScalar = GeneratedScalarProcesser.Instance.Process(context, input.Scalar.ScalarDefinition);

        if (processedGeneratedScalar.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<DataModel>(processedGeneratedScalar.Diagnostics);
        }

        DataModel model = new(input.Scalar, processedGeneratedScalar.Result.Unit, processedGeneratedScalar.Result.Vectors, input.ScalarPopulation, input.VectorPopulation);

        return OptionalWithDiagnostics.Result(model, processedGeneratedScalar.Diagnostics);
    }

    private static (DataModel Model, bool GenerateDocumentation) InterpretGenerateDocumentation((DataModel Model, bool Default) data, CancellationToken _)
    {
        if (data.Model.Scalar.ScalarDefinition.Locations.ExplicitlySetGenerateDocumentation)
        {
            return (data.Model, data.Model.Scalar.ScalarDefinition.GenerateDocumentation);
        }

        return (data.Model, data.Default);
    }

    private static IResultWithDiagnostics<DataModel> AppendDocumentationFile
        ((DataModel Model, bool GenerateDocumentation, DocumentationDictionary DocumentationDictionary) input, CancellationToken _)
    {
        if (input.GenerateDocumentation)
        {
            if (input.DocumentationDictionary.TryGetValue(input.Model.Scalar.ScalarType.Name, out DocumentationFile documentationFile))
            {
                DataModel modifiedModel = input.Model with { Documentation = documentationFile };
                return ResultWithDiagnostics.Construct(modifiedModel);
            }

            Diagnostic diagnostics = DiagnosticConstruction.NoMatchingDocumentationFile(input.Model.Scalar.ScalarLocation.AsRoslynLocation(),
                input.Model.Scalar.ScalarType.Name);

            return ResultWithDiagnostics.Construct(input.Model, diagnostics);
        }

        return ResultWithDiagnostics.Construct(input.Model);
    }

    private static bool ExtractDefaultGenerateDocumentation(GlobalAnalyzerConfig config, CancellationToken _) => config.GenerateDocumentationByDefault;
}

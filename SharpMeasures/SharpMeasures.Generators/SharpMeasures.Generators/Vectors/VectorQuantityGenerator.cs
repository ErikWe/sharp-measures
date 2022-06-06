namespace SharpMeasures.Generators.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.AnalyzerConfig;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Pipelines.Common;
using SharpMeasures.Generators.Vectors.Processing;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

using System.Threading;

internal static class VectorQuantityGenerator
{
    public static void Attach(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<ParsedVector> vectors,
        IncrementalValueProvider<UnitPopulation> unitPopulation, IncrementalValueProvider<ScalarPopulation> scalarPopulation,
        IncrementalValueProvider<VectorPopulation> vectorPopulation, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfig,
        IncrementalValueProvider<DocumentationDictionary> documentationDictionary)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfig.Select(ExtractDefaultGenerateDocumentation);

        var minimized = vectors.Combine(unitPopulation, scalarPopulation, vectorPopulation).Select(ReduceToDataModel).ReportDiagnostics(context)
            .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionary).Flatten().Select(AppendDocumentationFile)
            .ReportDiagnostics(context);

        CommonGenerator.Initialize(context, minimized);
    }

    private static IOptionalWithDiagnostics<DataModel> ReduceToDataModel
        ((ParsedVector Vector, UnitPopulation UnitPopulation, ScalarPopulation ScalarPopulation,
        VectorPopulation VectorPopulation) input, CancellationToken _)
    {
        GeneratedVectorProcessingContext context = new(input.Vector.VectorType, input.UnitPopulation, input.ScalarPopulation);
        var processedGeneratedScalar = GeneratedVectorProcesser.Instance.Process(context, input.Vector.VectorDefinition);

        if (processedGeneratedScalar.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<DataModel>(processedGeneratedScalar.Diagnostics);
        }

        DataModel model = new(input.Vector, processedGeneratedScalar.Result.Unit, processedGeneratedScalar.Result.Scalar);

        return OptionalWithDiagnostics.Result(model, processedGeneratedScalar.Diagnostics);
    }

    private static (DataModel Model, bool GenerateDocumentation) InterpretGenerateDocumentation((DataModel Model, bool Default) data, CancellationToken _)
    {
        if (data.Model.Vector.VectorDefinition.Locations.ExplicitlySetGenerateDocumentation)
        {
            return (data.Model, data.Model.Vector.VectorDefinition.GenerateDocumentation);
        }

        return (data.Model, data.Default);
    }

    private static IResultWithDiagnostics<DataModel> AppendDocumentationFile
        ((DataModel Model, bool GenerateDocumentation, DocumentationDictionary DocumentationDictionary) input, CancellationToken _)
    {
        if (input.GenerateDocumentation)
        {
            if (input.DocumentationDictionary.TryGetValue(input.Model.Vector.VectorType.Name, out DocumentationFile documentationFile))
            {
                DataModel modifiedModel = input.Model with { Documentation = documentationFile };
                return ResultWithDiagnostics.Construct(modifiedModel);
            }

            Diagnostic diagnostics = DiagnosticConstruction.NoMatchingDocumentationFile(input.Model.Vector.VectorLocation.AsRoslynLocation(),
                input.Model.Vector.VectorType.Name);

            return ResultWithDiagnostics.Construct(input.Model, diagnostics);
        }

        return ResultWithDiagnostics.Construct(input.Model);
    }

    private static bool ExtractDefaultGenerateDocumentation(GlobalAnalyzerConfig config, CancellationToken _) => config.GenerateDocumentationByDefault;
}

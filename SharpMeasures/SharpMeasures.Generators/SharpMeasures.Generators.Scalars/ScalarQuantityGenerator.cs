namespace SharpMeasures.Generators.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Providers.AnalyzerConfig;
using SharpMeasures.Generators.Scalars.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing;
using SharpMeasures.Generators.Scalars.Pipelines.Common;
using SharpMeasures.Generators.Scalars.Pipelines.DimensionalEquivalence;
using SharpMeasures.Generators.Scalars.Pipelines.Maths;
using SharpMeasures.Generators.Scalars.Pipelines.Units;
using SharpMeasures.Generators.Scalars.Pipelines.Vectors;
using SharpMeasures.Generators.Scalars.Refinement;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System.Threading;

public class ScalarGenerator
{
    public IncrementalValueProvider<ScalarPopulation> PopulationProvider { get; }
    private IncrementalValuesProvider<ParsedScalar> ScalarProvider { get; }

    internal ScalarGenerator(IncrementalValueProvider<ScalarPopulation> populationProvider, IncrementalValuesProvider<ParsedScalar> scalarProvider)
    {
        PopulationProvider = populationProvider;
        ScalarProvider = scalarProvider;
    }

    public void Perform(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<UnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<VectorPopulation> vectorPopulationProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider,
        IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfigProvider.Select(ExtractDefaultGenerateDocumentation);

        var minimized = ScalarProvider.Combine(unitPopulationProvider, PopulationProvider, vectorPopulationProvider).Select(ReduceToDataModel).ReportDiagnostics(context)
            .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionaryProvider).Flatten()
            .Select(AppendDocumentationFile).ReportDiagnostics(context);

        CommonGenerator.Initialize(context, minimized);
        DimensionalEquivalenceGenerator.Initialize(context, minimized);
        MathsGenerator.Initialize(context, minimized);
        UnitsGenerator.Initialize(context, minimized);
        VectorsGenerator.Initialize(context, minimized);
    }

    private static IOptionalWithDiagnostics<DataModel> ReduceToDataModel
        ((ParsedScalar Scalar, UnitPopulation UnitPopulation, ScalarPopulation ScalarPopulation,
        VectorPopulation VectorPopulation) input, CancellationToken _)
    {
        GeneratedScalarRefinementContext context = new(input.Scalar.ScalarType, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
        GeneratedScalarRefiner refiner = new(GeneratedScalarDiagnostics.Instance);

        var processedGeneratedScalar = refiner.Process(context, input.Scalar.ScalarDefinition);

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

    private readonly record struct GeneratedScalarRefinementContext : IGeneratedScalarRefinementContext
    {
        public DefinedType Type { get; }

        public UnitPopulation UnitPopulation { get; }
        public ScalarPopulation ScalarPopulation { get; }
        public VectorPopulation VectorPopulation { get; }

        public GeneratedScalarRefinementContext(DefinedType type, UnitPopulation unitPopulation, ScalarPopulation scalarPopulation, VectorPopulation vectorPopulation)
        {
            Type = type;

            UnitPopulation = unitPopulation;
            ScalarPopulation = scalarPopulation;
            VectorPopulation = vectorPopulation;
        }
    }
}

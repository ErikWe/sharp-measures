namespace SharpMeasures.Generators.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars.Diagnostics;
using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Scalars.Parsing;
using SharpMeasures.Generators.Scalars.Pipelines.Common;
using SharpMeasures.Generators.Scalars.Pipelines.DimensionalEquivalence;
using SharpMeasures.Generators.Scalars.Pipelines.Maths;
using SharpMeasures.Generators.Scalars.Pipelines.Units;
using SharpMeasures.Generators.Scalars.Pipelines.Vectors;
using SharpMeasures.Generators.Scalars.Refinement.SharpMeasuresScalar;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Populations;

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
            .Select(AppendDocumentation);

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
        SharpMeasuresScalarRefinementContext context = new(input.Scalar.ScalarType, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
        SharpMeasuresScalarRefiner refiner = new(SharpMeasuresScalarDiagnostics.Instance);

        var processedSharpMeasuresScalar = refiner.Process(context, input.Scalar.ScalarDefinition);

        if (processedSharpMeasuresScalar.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<DataModel>(processedSharpMeasuresScalar.Diagnostics);
        }

        DataModel model = new(processedSharpMeasuresScalar.Result, input.Scalar, input.ScalarPopulation, input.VectorPopulation);

        return OptionalWithDiagnostics.Result(model, processedSharpMeasuresScalar.Diagnostics);
    }

    private static (DataModel Model, bool GenerateDocumentation) InterpretGenerateDocumentation((DataModel Model, bool Default) data, CancellationToken _)
    {
        if (data.Model.ScalarData.ScalarDefinition.Locations.ExplicitlySetGenerateDocumentation)
        {
            return (data.Model, data.Model.ScalarData.ScalarDefinition.GenerateDocumentation);
        }

        return (data.Model, data.Default);
    }

    private static DataModel AppendDocumentation
        ((DataModel Model, bool GenerateDocumentation, DocumentationDictionary DocumentationDictionary) input, CancellationToken _)
    {
        if (input.GenerateDocumentation)
        {
            DefaultDocumentation defaultDocumentation = new(input.Model);

            if (input.DocumentationDictionary.TryGetValue(input.Model.ScalarData.ScalarType.Name, out DocumentationFile documentationFile))
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

    private readonly record struct SharpMeasuresScalarRefinementContext : ISharpMeasuresScalarRefinementContext
    {
        public DefinedType Type { get; }

        public UnitPopulation UnitPopulation { get; }
        public ScalarPopulation ScalarPopulation { get; }
        public VectorPopulation VectorPopulation { get; }

        public SharpMeasuresScalarRefinementContext(DefinedType type, UnitPopulation unitPopulation, ScalarPopulation scalarPopulation, VectorPopulation vectorPopulation)
        {
            Type = type;

            UnitPopulation = unitPopulation;
            ScalarPopulation = scalarPopulation;
            VectorPopulation = vectorPopulation;
        }
    }
}

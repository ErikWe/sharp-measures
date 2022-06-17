namespace SharpMeasures.Generators.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Quantities.Parsing;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Diagnostics;
using SharpMeasures.Generators.Vectors.Documentation;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Pipelines.Common;
using SharpMeasures.Generators.Vectors.Pipelines.Maths;
using SharpMeasures.Generators.Vectors.Pipelines.Units;
using SharpMeasures.Generators.Vectors.Refinement.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Refinement.ResizedSharpMeasuresVector;

using System.Threading;

public class VectorGenerator
{
    public IncrementalValueProvider<VectorPopulation> PopulationProvider { get; }
    private IncrementalValueProvider<VectorPopulationData> PopulationDataProvider { get; }
    private IncrementalValueProvider<UnitInclusionPopulation> UnitInclusionPopulationProvider { get; }

    private IncrementalValuesProvider<ParsedVector> VectorProvider { get; }
    private IncrementalValuesProvider<ParsedResizedVector> ResizedVectorProvider { get; }

    internal VectorGenerator(IncrementalValueProvider<VectorPopulation> populationProvider, IncrementalValueProvider<VectorPopulationData> populationDataProvider,
        IncrementalValueProvider<UnitInclusionPopulation> unitInclusionPopulationProvider, IncrementalValuesProvider<ParsedVector> vectorProvider,
        IncrementalValuesProvider<ParsedResizedVector> resizedVectorProvider)
    {
        PopulationProvider = populationProvider;
        PopulationDataProvider = populationDataProvider;
        UnitInclusionPopulationProvider = unitInclusionPopulationProvider;

        VectorProvider = vectorProvider;
        ResizedVectorProvider = resizedVectorProvider;
    }

    public void Perform(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<UnitPopulation> unitPopulation,
        IncrementalValueProvider<ScalarPopulation> scalarPopulation, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfig,
        IncrementalValueProvider<DocumentationDictionary> documentationDictionary)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfig.Select(ExtractDefaultGenerateDocumentation);

        var minimized = VectorProvider.Combine(unitPopulation, scalarPopulation, PopulationProvider, PopulationDataProvider).Select(ReduceToDataModel)
            .ReportDiagnostics(context).Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionary)
            .Flatten().Select(AppendDocumentationFile);

        var minimizedResized = ResizedVectorProvider.Combine(unitPopulation, scalarPopulation, PopulationProvider, PopulationDataProvider)
            .Select(ReduceToDataModel).ReportDiagnostics(context).Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation)
            .Combine(documentationDictionary).Flatten().Select(AppendDocumentationFile);

        CommonGenerator.Initialize(context, minimized, minimizedResized);
        MathsGenerator.Initialize(context, minimized, minimizedResized);
        UnitsGenerator.Initialize(context, minimized, minimizedResized, UnitInclusionPopulationProvider);
    }

    private static IOptionalWithDiagnostics<DataModel> ReduceToDataModel((ParsedVector Vector, UnitPopulation UnitPopulation, ScalarPopulation ScalarPopulation,
        VectorPopulation VectorPopulation, VectorPopulationData VectorPopulationData) input, CancellationToken _)
    {
        SharpMeasuresVectorRefinementContext context = new(input.Vector.VectorType, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
        SharpMeasuresVectorRefiner refiner = new(SharpMeasuresVectorDiagnostics.Instance);

        var processedVector = refiner.Process(context, input.Vector.VectorDefinition);

        if (processedVector.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<DataModel>(processedVector.Diagnostics);
        }

        DataModel model = new(processedVector.Result, input.Vector, input.VectorPopulation, input.VectorPopulationData);

        return OptionalWithDiagnostics.Result(model, processedVector.Diagnostics);
    }

    private static IOptionalWithDiagnostics<ResizedDataModel> ReduceToDataModel((ParsedResizedVector Vector, UnitPopulation UnitPopulation,
        ScalarPopulation ScalarPopulation, VectorPopulation VectorPopulation, VectorPopulationData VectorPopulationData) input, CancellationToken _)
    {
        ResizedSharpMeasuresVectorRefinementContext context
            = new(input.Vector.VectorType, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation, input.VectorPopulationData);

        ResizedSharpMeasuresVectorRefiner refiner = new(ResizedSharpMeasuresVectorDiagnostics.Instance);

        var processedResizedVector = refiner.Process(context, input.Vector.VectorDefinition);

        if (processedResizedVector.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<ResizedDataModel>(processedResizedVector.Diagnostics);
        }

        ResizedDataModel model = new(processedResizedVector.Result, input.Vector, input.VectorPopulation, input.VectorPopulationData);

        return OptionalWithDiagnostics.Result(model, processedResizedVector.Diagnostics);
    }

    private static (TDataModel Model, bool GenerateDocumentation) InterpretGenerateDocumentation<TDataModel>((TDataModel Model, bool Default) data, CancellationToken _)
        where TDataModel : IDataModel<TDataModel>
    {
        if (data.Model.ExplicitlySetGenerateDocumentation)
        {
            return (data.Model, data.Model.GenerateDocumentation);
        }

        return (data.Model, data.Default);
    }

    private static TDataModel AppendDocumentationFile<TDataModel>
        ((TDataModel Model, bool GenerateDocumentation, DocumentationDictionary DocumentationDictionary) input, CancellationToken _)
        where TDataModel : IDataModel<TDataModel>
    {
        if (input.GenerateDocumentation)
        {
            DefaultDocumentation<TDataModel> defaultDocumentation = new(input.Model);

            if (input.DocumentationDictionary.TryGetValue(input.Model.VectorType.Name, out DocumentationFile documentationFile))
            {
                input.Model = input.Model.WithDocumentation(new FileDocumentation(input.Model.Dimension, documentationFile, defaultDocumentation));
            }
            else
            {
                input.Model = input.Model.WithDocumentation(defaultDocumentation);
            }
        }

        return input.Model;
    }

    private static bool ExtractDefaultGenerateDocumentation(GlobalAnalyzerConfig config, CancellationToken _) => config.GenerateDocumentationByDefault;

    private readonly record struct SharpMeasuresVectorRefinementContext : ISharpMeasuresVectorRefinementContext
    {
        public DefinedType Type { get; }

        public UnitPopulation UnitPopulation { get; }
        public ScalarPopulation ScalarPopulation { get; }
        public VectorPopulation VectorPopulation { get; }

        public SharpMeasuresVectorRefinementContext(DefinedType type, UnitPopulation unitPopulation, ScalarPopulation scalarPopulation, VectorPopulation vectorPopulation)
        {
            Type = type;

            UnitPopulation = unitPopulation;
            ScalarPopulation = scalarPopulation;
            VectorPopulation = vectorPopulation;
        }
    }

    private readonly record struct ResizedSharpMeasuresVectorRefinementContext : IResizedSharpMeasuresVectorRefinementContext
    {
        public DefinedType Type { get; }

        public UnitPopulation UnitPopulation { get; }
        public ScalarPopulation ScalarPopulation { get; }
        public VectorPopulation VectorPopulation { get; }

        public VectorPopulationData VectorPopulationData { get; }

        public ResizedSharpMeasuresVectorRefinementContext(DefinedType type, UnitPopulation unitPopulation, ScalarPopulation scalarPopulation, VectorPopulation vectorPopulation,
            VectorPopulationData vectorPopulationData)
        {
            Type = type;

            UnitPopulation = unitPopulation;
            ScalarPopulation = scalarPopulation;
            VectorPopulation = vectorPopulation;

            VectorPopulationData = vectorPopulationData;
        }
    }
}

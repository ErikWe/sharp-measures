﻿namespace SharpMeasures.Generators.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Providers.AnalyzerConfig;
using SharpMeasures.Generators.Vectors.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Pipelines.Common;
using SharpMeasures.Generators.Vectors.Pipelines.Maths;
using SharpMeasures.Generators.Vectors.Pipelines.Units;
using SharpMeasures.Generators.Vectors.Refinement;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

using System.Threading;

public class VectorGenerator
{
    public IncrementalValueProvider<VectorPopulation> PopulationProvider { get; }
    private IncrementalValuesProvider<ParsedVector> VectorProvider { get; }
    private IncrementalValuesProvider<ParsedResizedVector> ResizedVectorProvider { get; }

    internal VectorGenerator(IncrementalValueProvider<VectorPopulation> populationProvider, IncrementalValuesProvider<ParsedVector> vectorProvider,
        IncrementalValuesProvider<ParsedResizedVector> resizedVectorProvider)
    {
        PopulationProvider = populationProvider;
        VectorProvider = vectorProvider;
        ResizedVectorProvider = resizedVectorProvider;
    }

    public void Perform(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<UnitPopulation> unitPopulation,
        IncrementalValueProvider<ScalarPopulation> scalarPopulation, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfig,
        IncrementalValueProvider<DocumentationDictionary> documentationDictionary)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfig.Select(ExtractDefaultGenerateDocumentation);

        var minimized = VectorProvider.Combine(unitPopulation, scalarPopulation, PopulationProvider).Select(ReduceToDataModel).ReportDiagnostics(context)
            .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionary).Flatten().Select(AppendDocumentationFile)
            .ReportDiagnostics(context);

        var minimizedResized = ResizedVectorProvider.Combine(unitPopulation, scalarPopulation, PopulationProvider).Select(ReduceToDataModel).ReportDiagnostics(context)
            .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionary).Flatten().Select(AppendDocumentationFile)
            .ReportDiagnostics(context);

        CommonGenerator.Initialize(context, minimized, minimizedResized);
        MathsGenerator.Initialize(context, minimized, minimizedResized);
        UnitsGenerator.Initialize(context, minimized, minimizedResized);
    }

    private static IOptionalWithDiagnostics<DataModel> ReduceToDataModel
        ((ParsedVector Vector, UnitPopulation UnitPopulation, ScalarPopulation ScalarPopulation, VectorPopulation VectorPopulation) input, CancellationToken _)
    {
        GeneratedVectorRefinementContext context = new(input.Vector.VectorType, input.UnitPopulation, input.ScalarPopulation);
        GeneratedVectorRefiner refiner = new(GeneratedVectorDiagnostics.Instance);

        var processedGeneratedVector = refiner.Process(context, input.Vector.VectorDefinition);

        if (processedGeneratedVector.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<DataModel>(processedGeneratedVector.Diagnostics);
        }

        DataModel model = new(input.Vector, processedGeneratedVector.Result.Unit, processedGeneratedVector.Result.Scalar);

        return OptionalWithDiagnostics.Result(model, processedGeneratedVector.Diagnostics);
    }

    private static IOptionalWithDiagnostics<ResizedDataModel> ReduceToDataModel
        ((ParsedResizedVector Vector, UnitPopulation UnitPopulation, ScalarPopulation ScalarPopulation, VectorPopulation VectorPopulation) input, CancellationToken _)
    {
        ResizedVectorRefinementContext context = new(input.Vector.VectorType, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
        ResizedVectorRefiner refiner = new(ResizedVectorDiagnostics.Instance);

        var processedResizedVector = refiner.Process(context, input.Vector.VectorDefinition);

        if (processedResizedVector.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<ResizedDataModel>(processedResizedVector.Diagnostics);
        }

        ResizedDataModel model = new(input.Vector, processedResizedVector.Result.AssociatedVector.VectorType, processedResizedVector.Result.Unit,
            processedResizedVector.Result.Scalar);

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

    private static IResultWithDiagnostics<TDataModel> AppendDocumentationFile<TDataModel>
        ((TDataModel Model, bool GenerateDocumentation, DocumentationDictionary DocumentationDictionary) input, CancellationToken _)
        where TDataModel : IDataModel<TDataModel>
    {
        if (input.GenerateDocumentation)
        {
            if (input.DocumentationDictionary.TryGetValue(input.Model.Identifier, out DocumentationFile documentationFile))
            {
                TDataModel modifiedModel = input.Model.WithDocumentation(documentationFile);
                return ResultWithDiagnostics.Construct(modifiedModel);
            }

            Diagnostic diagnostics = DiagnosticConstruction.NoMatchingDocumentationFile(input.Model.IdentifierLocation, input.Model.Identifier);

            return ResultWithDiagnostics.Construct(input.Model, diagnostics);
        }

        return ResultWithDiagnostics.Construct(input.Model);
    }

    private static bool ExtractDefaultGenerateDocumentation(GlobalAnalyzerConfig config, CancellationToken _) => config.GenerateDocumentationByDefault;

    private readonly record struct GeneratedVectorRefinementContext : IGeneratedVectorRefinementContext
    {
        public DefinedType Type { get; }

        public UnitPopulation UnitPopulation { get; }
        public ScalarPopulation ScalarPopulation { get; }

        public GeneratedVectorRefinementContext(DefinedType type, UnitPopulation unitPopulation, ScalarPopulation scalarPopulation)
        {
            Type = type;

            UnitPopulation = unitPopulation;
            ScalarPopulation = scalarPopulation;
        }
    }

    private readonly record struct ResizedVectorRefinementContext : IResizedVectorRefinementContext
    {
        public DefinedType Type { get; }

        public UnitPopulation UnitPopulation { get; }
        public ScalarPopulation ScalarPopulation { get; }
        public VectorPopulation VectorPopulation { get; }

        public ResizedVectorRefinementContext(DefinedType type, UnitPopulation unitPopulation, ScalarPopulation scalarPopulation, VectorPopulation vectorPopulation)
        {
            Type = type;

            UnitPopulation = unitPopulation;
            ScalarPopulation = scalarPopulation;
            VectorPopulation = vectorPopulation;
        }
    }
}

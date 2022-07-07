namespace SharpMeasures.Generators.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars.Diagnostics;
using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Scalars.Pipelines.Common;
using SharpMeasures.Generators.Scalars.Pipelines.DimensionalEquivalence;
using SharpMeasures.Generators.Scalars.Pipelines.Maths;
using SharpMeasures.Generators.Scalars.Pipelines.Units;
using SharpMeasures.Generators.Scalars.Pipelines.Vectors;
using SharpMeasures.Generators.Scalars.Refinement.SharpMeasuresScalar;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System.Threading;

public class ScalarGenerator
{
    private IncrementalValueProvider<ScalarPopulation> PopulationProvider { get; }
    private IncrementalValuesProvider<BaseScalarType> BaseScalarProvider { get; }
    private IncrementalValuesProvider<SpecializedScalarType> SpecializedScalarProvider { get; }

    internal ScalarGenerator(IncrementalValueProvider<ScalarPopulation> populationProvider, IncrementalValuesProvider<BaseScalarType> baseScalarProvider,
        IncrementalValuesProvider<SpecializedScalarType> specializedScalarProvider)
    {
        PopulationProvider = populationProvider;
        BaseScalarProvider = baseScalarProvider;
        SpecializedScalarProvider = specializedScalarProvider;
    }

    public IncrementalValueProvider<IScalarPopulation> GetPopulation() => PopulationProvider.Select(static (population, _) => population as IScalarPopulation);

    public void Perform(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider,
        IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfigProvider.Select(ExtractDefaultGenerateDocumentation);

        var minimized = BaseScalarProvider.Combine(unitPopulationProvider, PopulationProvider, vectorPopulationProvider).Select(ReduceToDataModel).ReportDiagnostics(context)
            .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionaryProvider).Flatten()
            .Select(AppendDocumentation);

        CommonGenerator.Initialize(context, minimized);
        DimensionalEquivalenceGenerator.Initialize(context, minimized);
        MathsGenerator.Initialize(context, minimized);
        UnitsGenerator.Initialize(context, minimized);
        VectorsGenerator.Initialize(context, minimized);
    }

    private static IOptionalWithDiagnostics<DataModel> ReduceToDataModel
        ((BaseScalarType Scalar, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken _)
    {
        SharpMeasuresScalarRefinementContext context = new(input.Scalar.Type, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
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

            if (input.DocumentationDictionary.TryGetValue(input.Model.ScalarData.Type.Name, out DocumentationFile documentationFile))
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

        public IUnitPopulation UnitPopulation { get; }
        public IScalarPopulation ScalarPopulation { get; }
        public IVectorPopulation VectorPopulation { get; }

        public SharpMeasuresScalarRefinementContext(DefinedType type, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
        {
            Type = type;

            UnitPopulation = unitPopulation;
            ScalarPopulation = scalarPopulation;
            VectorPopulation = vectorPopulation;
        }
    }
}

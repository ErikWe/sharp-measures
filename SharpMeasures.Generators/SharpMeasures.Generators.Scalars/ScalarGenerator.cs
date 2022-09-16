namespace SharpMeasures.Generators.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Scalars.Pipelines.Common;
using SharpMeasures.Generators.Scalars.Pipelines.Conversions;
using SharpMeasures.Generators.Scalars.Pipelines.Derivations;
using SharpMeasures.Generators.Scalars.Pipelines.Maths;
using SharpMeasures.Generators.Scalars.Pipelines.Units;
using SharpMeasures.Generators.Scalars.Pipelines.Vectors;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System.Threading;

public interface IScalarGenerator
{
    public abstract void Generate(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<IResolvedVectorPopulation> vectorPopulationProvider,
        IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider);
}

internal class ScalarGenerator : IScalarGenerator
{
    private IncrementalValuesProvider<Optional<ResolvedScalarType>> ScalarBaseProvider { get; }
    private IncrementalValuesProvider<Optional<ResolvedScalarType>> ScalarSpecializationProvider { get; }

    internal ScalarGenerator(IncrementalValuesProvider<Optional<ResolvedScalarType>> scalarBaseProvider, IncrementalValuesProvider<Optional<ResolvedScalarType>> scalarSpecializationProvider)
    {
        ScalarBaseProvider = scalarBaseProvider;
        ScalarSpecializationProvider = scalarSpecializationProvider;
    }

    public void Generate(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<IResolvedVectorPopulation> vectorPopulationProvider,
        IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        Generate(context, ScalarBaseProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider, documentationDictionaryProvider);
        Generate(context, ScalarSpecializationProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider, documentationDictionaryProvider);
    }

    private static void Generate(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<ResolvedScalarType>> scalars,
        IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<IResolvedVectorPopulation> vectorPopulationProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider,
        IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfigProvider.Select(ExtractDefaultGenerateDocumentation);

        var reducedScalars = scalars.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(ReduceToDataModel)
            .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionaryProvider).Flatten().Select(AppendDocumentation);

        CommonGenerator.Initialize(context, reducedScalars);
        ConversionsGenerator.Initialize(context, reducedScalars);
        DerivationsGenerator.Initialize(context, reducedScalars);
        MathsGenerator.Initialize(context, reducedScalars);
        UnitsGenerator.Initialize(context, reducedScalars);
        VectorsGenerator.Initialize(context, reducedScalars);
    }

    private static Optional<DataModel> ReduceToDataModel((Optional<ResolvedScalarType> Scalar, IUnitPopulation UnitPopulation, IResolvedScalarPopulation ScalarPopulation, IResolvedVectorPopulation VectorPopulation) input, CancellationToken _)
    {
        if (input.Scalar.HasValue is false)
        {
            return new Optional<DataModel>();
        }

        return new DataModel(input.Scalar.Value, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    private static (Optional<DataModel> Model, bool GenerateDocumentation) InterpretGenerateDocumentation((Optional<DataModel> Model, bool Default) data, CancellationToken _)
    {
        if (data.Model.HasValue is false)
        {
            return (data.Model, false);
        }

        return (data.Model, data.Model.Value.Scalar.GenerateDocumentation ?? data.Default);
    }

    private static Optional<DataModel> AppendDocumentation((Optional<DataModel> Model, bool GenerateDocumentation, DocumentationDictionary DocumentationDictionary) input, CancellationToken _)
    {
        if (input.GenerateDocumentation is false || input.Model.HasValue is false)
        {
            return input.Model;
        }

        DefaultDocumentation defaultDocumentation = new(input.Model.Value);

        if (input.DocumentationDictionary.TryGetValue(input.Model.Value.Scalar.Type.Name, out DocumentationFile documentationFile))
        {
            return input.Model.Value with
            {
                Documentation = new FileDocumentation(documentationFile, defaultDocumentation)
            };
        }

        return input.Model.Value with
        {
            Documentation = defaultDocumentation
        };
    }

    private static bool ExtractDefaultGenerateDocumentation(GlobalAnalyzerConfig config, CancellationToken _) => config.GenerateDocumentationByDefault;
}

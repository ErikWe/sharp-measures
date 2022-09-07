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
        IncrementalValueProvider<IResolvedVectorPopulation> vectorPopulationProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider,
        IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider);
}

internal class ScalarGenerator : IScalarGenerator
{
    private IncrementalValueProvider<IResolvedScalarPopulation> ScalarPopulationProvider { get; }

    private IncrementalValuesProvider<ResolvedScalarType> ScalarBaseProvider { get; }
    private IncrementalValuesProvider<ResolvedScalarType> ScalarSpecializationProvider { get; }

    internal ScalarGenerator(IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider, IncrementalValuesProvider<ResolvedScalarType> scalarBaseProvider,
        IncrementalValuesProvider<ResolvedScalarType> scalarSpecializationProvider)
    {
        ScalarPopulationProvider = scalarPopulationProvider;

        ScalarBaseProvider = scalarBaseProvider;
        ScalarSpecializationProvider = scalarSpecializationProvider;
    }

    public void Generate(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IResolvedVectorPopulation> vectorPopulationProvider,
        IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        Generate(context, ScalarBaseProvider, unitPopulationProvider, ScalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider, documentationDictionaryProvider);
        Generate(context, ScalarSpecializationProvider, unitPopulationProvider, ScalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider, documentationDictionaryProvider);
    }

    private static void Generate(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<ResolvedScalarType> scalars,
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

    private static DataModel ReduceToDataModel((ResolvedScalarType Scalar, IUnitPopulation UnitPopulation, IResolvedScalarPopulation ScalarPopulation, IResolvedVectorPopulation VectorPopulation) input, CancellationToken _)
    {
        return new(input.Scalar, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    private static (DataModel Model, bool GenerateDocumentation) InterpretGenerateDocumentation((DataModel Model, bool Default) data, CancellationToken _)
    {
        return (data.Model, data.Model.Scalar.GenerateDocumentation ?? data.Default);
    }

    private static DataModel AppendDocumentation((DataModel Model, bool GenerateDocumentation, DocumentationDictionary DocumentationDictionary) input, CancellationToken _)
    {
        if (input.GenerateDocumentation is false)
        {
            return input.Model;
        }

        DefaultDocumentation defaultDocumentation = new(input.Model);

        if (input.DocumentationDictionary.TryGetValue(input.Model.Scalar.Type.Name, out DocumentationFile documentationFile))
        {
            return input.Model with
            {
                Documentation = new FileDocumentation(documentationFile, defaultDocumentation)
            };
        }

        return input.Model with
        {
            Documentation = defaultDocumentation
        };
    }

    private static bool ExtractDefaultGenerateDocumentation(GlobalAnalyzerConfig config, CancellationToken _) => config.GenerateDocumentationByDefault;
}

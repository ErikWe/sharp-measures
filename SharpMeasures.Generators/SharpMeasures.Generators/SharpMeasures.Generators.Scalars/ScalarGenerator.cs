namespace SharpMeasures.Generators.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Scalars.Pipelines.Common;
using SharpMeasures.Generators.Scalars.Pipelines.Conversions;
using SharpMeasures.Generators.Scalars.Pipelines.Maths;
using SharpMeasures.Generators.Scalars.Pipelines.Units;
using SharpMeasures.Generators.Scalars.Pipelines.Vectors;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System.Threading;

public interface IScalarGenerator
{
    public abstract void Perform(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider,
        IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider);
}

internal class ScalarGenerator : IScalarGenerator
{
    private IncrementalValuesProvider<ScalarType> ScalarBaseProvider { get; }
    private IncrementalValuesProvider<ScalarType> ScalarSpecializationProvider { get; }

    internal ScalarGenerator(IncrementalValuesProvider<ScalarType> scalarBaseProvider, IncrementalValuesProvider<ScalarType> scalarSpecializationProvider)
    {
        ScalarBaseProvider = scalarBaseProvider;
        ScalarSpecializationProvider = scalarSpecializationProvider;
    }

    public void Perform(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider,
        IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        Perform(context, ScalarBaseProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider,
            documentationDictionaryProvider);

        Perform(context, ScalarSpecializationProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider, globalAnalyzerConfigProvider,
            documentationDictionaryProvider);
    }

    private static void Perform(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<ScalarType> scalars,
        IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider, IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider,
        IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfigProvider.Select(ExtractDefaultGenerateDocumentation);

        var reducedScalars = scalars.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(ReduceToDataModel)
            .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionaryProvider).Flatten().Select(AppendDocumentation);

        CommonGenerator.Initialize(context, reducedScalars);
        ConversionsGenerator.Initialize(context, reducedScalars);
        MathsGenerator.Initialize(context, reducedScalars);
        UnitsGenerator.Initialize(context, reducedScalars);
        VectorsGenerator.Initialize(context, reducedScalars);
    }

    private static DataModel ReduceToDataModel
        ((ScalarType Scalar, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken _)
    {
        return new(input.Scalar, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    private static (DataModel Model, bool GenerateDocumentation) InterpretGenerateDocumentation((DataModel Model, bool Default) data, CancellationToken _)
    {
        return (data.Model, data.Model.Scalar.Definition.GenerateDocumentation ?? data.Default);
    }

    private static DataModel AppendDocumentation((DataModel Model, bool GenerateDocumentation, DocumentationDictionary DocumentationDictionary) input, CancellationToken _)
    {
        if (input.GenerateDocumentation)
        {
            DefaultDocumentation defaultDocumentation = new(input.Model);

            if (input.DocumentationDictionary.TryGetValue(input.Model.Scalar.Type.Name, out DocumentationFile documentationFile))
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
}

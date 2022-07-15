namespace SharpMeasures.Generators.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Scalars.Pipelines.Common;
using SharpMeasures.Generators.Scalars.Pipelines.Convertible;
using SharpMeasures.Generators.Scalars.Pipelines.Maths;
using SharpMeasures.Generators.Scalars.Pipelines.Units;
using SharpMeasures.Generators.Scalars.Pipelines.Vectors;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System.Threading;

public class ScalarGenerator
{
    private IncrementalValuesProvider<BaseScalarType> BaseScalarProvider { get; }
    private IncrementalValuesProvider<SpecializedScalarType> SpecializedScalarProvider { get; }

    internal ScalarGenerator(IncrementalValuesProvider<BaseScalarType> baseScalarProvider, IncrementalValuesProvider<SpecializedScalarType> specializedScalarProvider)
    {
        BaseScalarProvider = baseScalarProvider;
        SpecializedScalarProvider = specializedScalarProvider;
    }

    public void Perform(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider,
        IncrementalValueProvider<GlobalAnalyzerConfig> globalAnalyzerConfigProvider, IncrementalValueProvider<DocumentationDictionary> documentationDictionaryProvider)
    {
        var defaultGenerateDocumentation = globalAnalyzerConfigProvider.Select(ExtractDefaultGenerateDocumentation);

        var minimizedBaseScalar = BaseScalarProvider.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(ReduceToDataModel)
            .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionaryProvider).Flatten()
            .Select(AppendDocumentation<BaseDataModel, BaseScalarType>);

        var minimizedSpecializedScalar = SpecializedScalarProvider.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(ReduceToDataModel)
            .Combine(defaultGenerateDocumentation).Select(InterpretGenerateDocumentation).Combine(documentationDictionaryProvider).Flatten()
            .Select(AppendDocumentation<SpecializedDataModel, SpecializedScalarType>);

        CommonGenerator.Initialize(context, minimizedBaseScalar);
        ConvertibleGenerator.Initialize(context, minimizedBaseScalar);
        MathsGenerator.Initialize(context, minimizedBaseScalar);
        UnitsGenerator.Initialize(context, minimizedBaseScalar);
        VectorsGenerator.Initialize(context, minimizedBaseScalar);

        CommonGenerator.Initialize(context, minimizedSpecializedScalar);
        ConvertibleGenerator.Initialize(context, minimizedSpecializedScalar);
        MathsGenerator.Initialize(context, minimizedSpecializedScalar);
        UnitsGenerator.Initialize(context, minimizedSpecializedScalar);
        VectorsGenerator.Initialize(context, minimizedSpecializedScalar);
    }

    private static BaseDataModel ReduceToDataModel
        ((BaseScalarType Scalar, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken _)
    {
        var inheritance = InheritanceResolver.Resolve(input.Scalar, input.UnitPopulation, input.ScalarPopulation);

        return new(input.Scalar, inheritance, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    private static SpecializedDataModel ReduceToDataModel
        ((SpecializedScalarType Scalar, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken _)
    {
        var inheritance = InheritanceResolver.Resolve(input.Scalar, input.UnitPopulation, input.ScalarPopulation);

        return new(input.Scalar, inheritance, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    private static (BaseDataModel Model, bool GenerateDocumentation) InterpretGenerateDocumentation((BaseDataModel Model, bool Default) data, CancellationToken _)
    {
        return (data.Model, data.Model.Scalar.Definition.GenerateDocumentation ?? data.Default);
    }

    private static (SpecializedDataModel Model, bool GenerateDocumentation) InterpretGenerateDocumentation((SpecializedDataModel Model, bool Default) data, CancellationToken _)
    {
        return (data.Model, determineInheritedState(data.Model.Scalar) ?? data.Default);

        bool? determineInheritedState(IScalarType scalarType)
        {
            if (scalarType.Definition.GenerateDocumentation is not null)
            {
                return scalarType.Definition.GenerateDocumentation;
            }

            if (scalarType is ISpecializedScalarType specializedScalarType
                && data.Model.ScalarPopulation.Scalars.TryGetValue(specializedScalarType.Definition.OriginalScalar.Type.AsNamedType(), out var originalScalar))
            {
                return determineInheritedState(originalScalar);
            }

            return null;
        }
    }

    private static TDataModel AppendDocumentation<TDataModel, TScalarType>
        ((TDataModel Model, bool GenerateDocumentation, DocumentationDictionary DocumentationDictionary) input, CancellationToken _)
        where TDataModel : ADataModel<TScalarType>
        where TScalarType : IScalarType
    {
        if (input.GenerateDocumentation)
        {
            DefaultDocumentation<TScalarType> defaultDocumentation = new(input.Model);

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

namespace SharpMeasures.Generators.Vectors.Pipelines.DimensionalEquivalence;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Diagnostics;

using System.Collections.Generic;
using System.Threading;
using System.Linq;
using SharpMeasures.Generators.Vectors.Refinement.DimensionalEquivalence;

internal static class DimensionEquivalenceGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Vectors.DataModel> generatedVectorProvider,
        IncrementalValuesProvider<ResizedDataModel> resizedVectorProvider)
    {
        var reducedGeneratedVectors = generatedVectorProvider.Select(ReduceToDataModel).ReportDiagnostics(context);
        var reducedResizedVectors = resizedVectorProvider.Select(ReduceToDataModel).ReportDiagnostics(context);

        context.RegisterSourceOutput(reducedGeneratedVectors, Execution.Execute);
        context.RegisterSourceOutput(reducedResizedVectors, Execution.Execute);
    }

    private static IOptionalWithDiagnostics<DataModel> ReduceToDataModel(IDataModel input, CancellationToken _)
    {
        if (input.VectorPopulation.ResizedVectorGroups.TryGetValue(input.VectorType.AsNamedType(), out var vectorGroup) is false)
        {
            return OptionalWithDiagnostics.Empty<DataModel>();
        }

        if (input.VectorPopulationData.DimensionalEquivalences.TryGetValue(vectorGroup, out var dimensionallyEquivalentGroups) is false)
        {
            return OptionalWithDiagnostics.Empty<DataModel>();
        }

        HashSet<NamedType> excessiveQuantities = new();

        if (input.VectorPopulationData.ExcessiveDimensionalEquivalences.TryGetValue(input.VectorType.AsNamedType(), out var excessiveQuantitiesList))
        {
            foreach (var quantity in excessiveQuantitiesList)
            {
                excessiveQuantities.Add(quantity);
            }
        }

        DimensionalEquivalenceValidationContext context = new(input.VectorType, excessiveQuantities, input.VectorPopulation);
        DimensionalEquivalenceValidator validator = new(DimensionalEquivalenceDiagnostics.Instance);

        var dimensionalEquivalenceDiagnostics = ValidityFilter.Create(validator).Filter(context, input.DimensionalEquivalences);

        DataModel model = new(input.VectorType, input.Dimension, dimensionallyEquivalentGroups, input.Documentation);
        return ResultWithDiagnostics.Construct(model, dimensionalEquivalenceDiagnostics.Diagnostics);
    }

    private readonly record struct DimensionalEquivalenceValidationContext : IDimensionalEquivalenceValidationContext
    {
        public DefinedType Type { get; }

        public HashSet<NamedType> ExcessiveQuantities { get; }
        public VectorPopulation VectorPopulation { get; }

        public DimensionalEquivalenceValidationContext(DefinedType type, HashSet<NamedType> excessiveQuantities, VectorPopulation vectorPopulation)
        {
            Type = type;

            ExcessiveQuantities = excessiveQuantities;
            VectorPopulation = vectorPopulation;
        }
    }
}

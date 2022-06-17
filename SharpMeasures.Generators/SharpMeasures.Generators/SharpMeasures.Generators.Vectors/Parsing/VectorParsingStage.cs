namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing;
using SharpMeasures.Generators.Vectors;

public static class VectorParsingStage
{
    public static VectorGenerator Attach(IncrementalGeneratorInitializationContext context)
    {
        var generatedVectors = new GeneratedVectorParsingStage(context);
        var resizedVectors = new ResizedVectorParsingStage(context);

        var interfaces = generatedVectors.InterfaceProvider.Collect().Combine(resizedVectors.InterfaceProvider.Collect());
        
        var populationWithData = interfaces.Select(VectorPopulationBuilder.Build);
        var population = populationWithData.Select(static (x, _) => x.Item1);
        var populationData = populationWithData.Select(static (x, _) => x.Item2);

        var unitInclusionPopulation = interfaces.Select(UnitInclusionPopulationBuilder.Build);

        return new(population, populationData, unitInclusionPopulation, generatedVectors.ProcessedProvider, resizedVectors.ProcessedProvider);
    }
}

namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Utility;
using SharpMeasures.Generators.Vectors;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Linq;

internal static class VectorPopulationBuilder
{
    public static (VectorPopulation, VectorPopulationData) Build
        ((ImmutableArray<SharpMeasuresVectorInterface> Base, ImmutableArray<ResizedSharpMeasuresVectorInterface> Resized) vectors, CancellationToken _)
    {
        Dictionary<NamedType, IVectorInterface> allVectors = new();
        Dictionary<NamedType, IVectorInterface> nonUniquelyDefinedTypes = new();

        foreach (SharpMeasuresVectorInterface baseVector in vectors.Base)
        {
            allVectors.Add(baseVector.VectorType, baseVector);
        }

        foreach (ResizedSharpMeasuresVectorInterface resizedVector in vectors.Resized)
        {
            if (allVectors.ContainsKey(resizedVector.VectorType))
            {
                if (nonUniquelyDefinedTypes.ContainsKey(resizedVector.VectorType))
                {
                    continue;
                }

                nonUniquelyDefinedTypes.Add(resizedVector.VectorType, resizedVector);
                continue;
            }

            allVectors.Add(resizedVector.VectorType, resizedVector);
        }

        Dictionary<NamedType, ResizedVectorGroup.IBuilder> groupBuilders = new();
        Dictionary<NamedType, ResizedSharpMeasuresVectorInterface> duplicatePopulation = new();

        foreach (SharpMeasuresVectorInterface vector in vectors.Base)
        {
            groupBuilders.Add(vector.VectorType, ResizedVectorGroup.StartBuilder(vector));
        }

        List<ResizedSharpMeasuresVectorInterface> ungroupedResizedVectors = new(vectors.Resized);
        AddResizedVectors(groupBuilders, ungroupedResizedVectors, duplicatePopulation, nonUniquelyDefinedTypes);

        ReadOnlyEquatableDictionary<NamedType, ResizedVectorGroup> resizedVectorGroups
            = groupBuilders.ToDictionary(static (x) => x.Key, static (x) => x.Value.Build()).AsReadOnlyEquatable();

        ReadOnlyEquatableDictionary<NamedType, IVectorInterface> unresolvedPopulation
            = ungroupedResizedVectors.ToDictionary(static (x) => x.VectorType, static (x) => x as IVectorInterface).AsReadOnlyEquatable();

        Dictionary<ResizedVectorGroup, Dictionary<ResizedVectorGroup, ConversionOperationBehaviour>> dimensionalEquivalences = new();
        Dictionary<NamedType, List<NamedType>> excessiveDimensionalEquivalences = new();

        VectorPopulation population = new(allVectors, resizedVectorGroups);

        foreach (var baseVector in vectors.Base)
        {
            var resizedVectorGroup = population.ResizedVectorGroups[baseVector.VectorType];

            Dictionary<ResizedVectorGroup, ConversionOperationBehaviour> linkedGroups = new();

            foreach (var vector in resizedVectorGroup.VectorsByDimension.Values)
            {
                List<NamedType> excessiveDefinitions = new();

                foreach (var dimensionalEquivalenceDefinitions in vector.DimensionalEquivalences)
                {
                    foreach (var dimensionalEquivalenceDefinition in dimensionalEquivalenceDefinitions.Quantities)
                    {
                        if (resizedVectorGroups.TryGetValue(dimensionalEquivalenceDefinition, out var linkedGroup))
                        {
                            if (linkedGroups.ContainsKey(linkedGroup))
                            {
                                excessiveDefinitions.Add(dimensionalEquivalenceDefinition);
                                continue;
                            }

                            linkedGroups.Add(linkedGroup, dimensionalEquivalenceDefinitions.CastOperatorBehaviour);
                        }
                    }
                }

                excessiveDimensionalEquivalences.Add(vector.VectorType, excessiveDefinitions);
            }

            dimensionalEquivalences.Add(resizedVectorGroup, linkedGroups);
        }

        VectorPopulationData populationData = new
        (
            dimensionalEquivalences.ToDictionary((x) => x.Key, (x) => x.Value.AsReadOnlyEquatable()),
            excessiveDimensionalEquivalences.ToDictionary((x) => x.Key, (x) => x.Value.AsReadOnlyEquatable()),
            unresolvedPopulation,
            duplicatePopulation,
            nonUniquelyDefinedTypes
        );

        return (population, populationData);
    }

    private static void AddResizedVectors(Dictionary<NamedType, ResizedVectorGroup.IBuilder> groupBuilders,
        List<ResizedSharpMeasuresVectorInterface> ungroupedVectors, Dictionary<NamedType, ResizedSharpMeasuresVectorInterface> duplicateDimensionVectors,
        Dictionary<NamedType, IVectorInterface> nonUniquelyDefinedTypes)
    {
        while (true)
        {
            if (ungroupedVectors.Count is 0)
            {
                break;
            }

            int startLength = ungroupedVectors.Count;

            for (int i = 0; i < ungroupedVectors.Count; i++)
            {
                if (nonUniquelyDefinedTypes.ContainsKey(ungroupedVectors[i].VectorType))
                {
                    continue;
                }

                if (groupBuilders.TryGetValue(ungroupedVectors[i].AssociatedVector, out var builder) is false)
                {
                    if (duplicateDimensionVectors.TryGetValue(ungroupedVectors[i].AssociatedVector, out var duplicateParent))
                    {
                        AddResizedVectorWithDuplicateParent(ungroupedVectors[i], duplicateParent, groupBuilders, duplicateDimensionVectors);
                        continue;
                    }

                    continue;
                }

                if (builder.HasVectorOfDimension(ungroupedVectors[i].Dimension))
                {
                    duplicateDimensionVectors.Add(ungroupedVectors[i].VectorType, ungroupedVectors[i]);
                    removeAndDecrementLoop();

                    continue;
                }

                builder.AddResizedVector(ungroupedVectors[i]);
                groupBuilders.Add(ungroupedVectors[i].VectorType, builder);
                removeAndDecrementLoop();

                void removeAndDecrementLoop()
                {
                    ungroupedVectors.RemoveAt(i);
                    i -= 1;
                }
            }

            if (ungroupedVectors.Count == startLength)
            {
                break;
            }
        }
    }

    private static void AddResizedVectorWithDuplicateParent(ResizedSharpMeasuresVectorInterface vector, ResizedSharpMeasuresVectorInterface parent,
        Dictionary<NamedType, ResizedVectorGroup.IBuilder> groupBuilders, Dictionary<NamedType, ResizedSharpMeasuresVectorInterface> duplicateDimensionVectors)
    {
        if (groupBuilders.TryGetValue(parent.AssociatedVector, out var builder) is false)
        {
            if (duplicateDimensionVectors.TryGetValue(parent.AssociatedVector, out var duplicateParent))
            {
                AddResizedVectorWithDuplicateParent(vector, duplicateParent, groupBuilders, duplicateDimensionVectors);
                return;
            }

            return;
        }

        if (builder.HasVectorOfDimension(vector.Dimension))
        {
            duplicateDimensionVectors.Add(vector.VectorType, vector);
            return;
        }

        builder.AddResizedVector(vector);
        groupBuilders.Add(vector.VectorType, builder);
    }
}

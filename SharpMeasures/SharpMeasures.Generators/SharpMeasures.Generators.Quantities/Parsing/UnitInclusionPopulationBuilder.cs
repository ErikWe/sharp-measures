namespace SharpMeasures.Generators.Quantities.Parsing;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Vectors;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Linq;

public static class UnitInclusionPopulationBuilder
{
    public static UnitInclusionPopulation Build((ImmutableArray<GeneratedVectorInterface> Generated, ImmutableArray<ResizedVectorInterface> Resized) vectors,
        CancellationToken _)
    {
        Dictionary<NamedType, UnitInclusion> unitInclusionPopulation = new(vectors.Generated.Length + vectors.Resized.Length);

        AddGeneratedVectors(unitInclusionPopulation, vectors.Generated);
        AddResizedVectors(unitInclusionPopulation, vectors.Resized);

        return new(unitInclusionPopulation);
    }

    private static void AddGeneratedVectors(Dictionary<NamedType, UnitInclusion> population, ImmutableArray<GeneratedVectorInterface> generatedVectors)
    {
        foreach (GeneratedVectorInterface generatedVector in generatedVectors)
        {
            HashSet<string> listedUnits = new();

            if (generatedVector.IncludedUnits.Any())
            {
                AddListedUnits(listedUnits, generatedVector.IncludedUnits);
            }
            else
            {
                AddListedUnits(listedUnits, generatedVector.ExcludedUnits);
            }

            UnitInclusion.InclusionMode mode = generatedVector.IncludedUnits.Any() ? UnitInclusion.InclusionMode.Include : UnitInclusion.InclusionMode.Exclude;
            UnitInclusion unitInclusion = new(mode, listedUnits);

            population.Add(generatedVector.VectorType, unitInclusion);
        }
    }

    private static void AddResizedVectors(Dictionary<NamedType, UnitInclusion> population, ImmutableArray<ResizedVectorInterface> resizedVectors)
    {
        List<ResizedVectorInterface> unpairedVectors = new(resizedVectors);

        while (true)
        {
            if (unpairedVectors.Count is 0)
            {
                break;
            }

            int startLength = unpairedVectors.Count;

            for (int i = 0; i < unpairedVectors.Count; i++)
            {
                if (population.TryGetValue(unpairedVectors[i].AssociatedVector, out var unitInclusion))
                {
                    population.Add(unpairedVectors[i].VectorType, DeriveUnitInclusion(unitInclusion, unpairedVectors[i]));

                    unpairedVectors.RemoveAt(i);
                    i -= 1;
                }
            }

            if (unpairedVectors.Count == startLength)
            {
                break;
            }
        }
    }

    private static UnitInclusion DeriveUnitInclusion(UnitInclusion parent, IVectorInterface vector)
    {
        HashSet<string> listedUnits = new(parent.ListedUnits);

        if (parent.Mode is UnitInclusion.InclusionMode.Include)
        {
            if (vector.IncludedUnits.Any())
            {
                AddListedUnits(listedUnits, vector.IncludedUnits);
            }
            else
            {
                RemoveListedUnits(listedUnits, vector.ExcludedUnits);
            }
        }
        else
        {
            if (vector.IncludedUnits.Any())
            {
                RemoveListedUnits(listedUnits, vector.IncludedUnits);
            }
            else
            {
                AddListedUnits(listedUnits, vector.ExcludedUnits);
            }
        }

        return new(parent.Mode, listedUnits);
    }

    private static void AddListedUnits(HashSet<string> list, IEnumerable<IncludeUnitsInterface> unitInclusions)
    {
        foreach (IncludeUnitsInterface includeUnits in unitInclusions)
        {
            foreach (string includedUnit in includeUnits.IncludedUnits)
            {
                list.Add(includedUnit);
            }
        }
    }

    private static void AddListedUnits(HashSet<string> list, IEnumerable<ExcludeUnitsInterface> unitExclusions)
    {
        foreach (ExcludeUnitsInterface excludeUnits in unitExclusions)
        {
            foreach (string excludedUnit in excludeUnits.ExcludedUnits)
            {
                list.Add(excludedUnit);
            }
        }
    }

    private static void RemoveListedUnits(HashSet<string> list, IEnumerable<IncludeUnitsInterface> unitInclusions)
    {
        foreach (IncludeUnitsInterface includeUnits in unitInclusions)
        {
            foreach (string includedUnit in includeUnits.IncludedUnits)
            {
                list.Remove(includedUnit);
            }
        }
    }

    private static void RemoveListedUnits(HashSet<string> list, IEnumerable<ExcludeUnitsInterface> unitExclusions)
    {
        foreach (ExcludeUnitsInterface excludeUnits in unitExclusions)
        {
            foreach (string excludedUnit in excludeUnits.ExcludedUnits)
            {
                list.Remove(excludedUnit);
            }
        }
    }
}

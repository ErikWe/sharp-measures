namespace SharpMeasures.Generators.Vectors.Populations;

using SharpMeasures.Equatables;

using System.Collections.Generic;
using System.Linq;

internal static class DimensionalEquivalencePopulationBuilder
{
    public static (DimensionalEquivalencePopulation, DimensionalEquivalencePopulationErrors) Build(ResizedGroupPopulation groupPopulation)
    {
        Builder builder = new(groupPopulation);
        return (builder.Population, builder.Errors);
    }

    private class Builder
    {
        public DimensionalEquivalencePopulation Population { get; }
        public DimensionalEquivalencePopulationErrors Errors { get; }

        private Dictionary<ResizedGroup, List<ResizedGroupDimensionalEquivalence>> Equivalences { get; } = new();

        public Builder(ResizedGroupPopulation groupPopulation)
        {
            foreach (var group in groupPopulation.Values)
            {
                AddEquivalencesForGroup(groupPopulation, group);
            }

            Population = ConstructPopulation();
        }

        private void AddEquivalencesForGroup(ResizedGroupPopulation groupPopulation, ResizedGroup group)
        {
            List<ResizedGroupDimensionalEquivalence> groupEquivalences = new();
            HashSet<ResizedGroup> addedGroups = new();

            foreach (var vector in group.VectorsByDimension.Values)
            {
                foreach (var equivalenceList in vector.DimensionalEquivalences)
                {
                    foreach (var equivalentVectorName in equivalenceList.Quantities)
                    {
                        if (groupPopulation.TryGetValue(equivalentVectorName, out var equivalentGroup) is false)
                        {
                            // TODO: Errors.UnresolvedDimensionalEquivalences.Add
                            continue;
                        }

                        if (addedGroups.Contains(equivalentGroup))
                        {
                            // TODO: Errors.DuplicateDimensionalEquivalences.Add
                            continue;
                        }

                        if (true) // TODO: No vector dimension overlap
                        {
                            // TODO: Errors.NoDimensionalOverlap.Add
                            continue;
                        }

                        addedGroups.Add(equivalentGroup);
                        groupEquivalences.Add(new(equivalentGroup, equivalenceList.CastOperatorBehaviour));
                    }
                }
            }
        }

        private DimensionalEquivalencePopulation ConstructPopulation()
        {
            return new(Equivalences.ToDictionary((x) => x.Key, transformEquivalences));

            static ReadOnlyEquatableCollection<ResizedGroupDimensionalEquivalence> transformEquivalences
                (KeyValuePair<ResizedGroup, List<ResizedGroupDimensionalEquivalence>> equivalence)
            {
                return equivalence.Value.AsReadOnlyEquatable();
            }
        }
    }
}

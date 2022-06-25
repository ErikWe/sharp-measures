namespace SharpMeasures.Generators.Vectors.Populations;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class DimensionalEquivalencePopulation : ReadOnlyEquatableDictionary<ResizedGroup, ReadOnlyEquatableCollection<ResizedGroupDimensionalEquivalence>>
{
    public DimensionalEquivalencePopulation(IReadOnlyDictionary<ResizedGroup, ReadOnlyEquatableCollection<ResizedGroupDimensionalEquivalence>> items) : base(items) { }
}

namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class VectorPopulation
{
    public ReadOnlyEquatableDictionary<NamedType, ResizedVectorGroup> VectorGroups { get; }

    public ReadOnlyEquatableDictionary<NamedType, ResizedVectorInterface> UnresolvedVectors { get; }
    public ReadOnlyEquatableDictionary<NamedType, ResizedVectorInterface> DuplicateDimensionVectors { get; }

    public VectorPopulation(IReadOnlyDictionary<NamedType, ResizedVectorGroup> resolvedVectors,
        IReadOnlyDictionary<NamedType, ResizedVectorInterface> unresolvedVectors, IReadOnlyDictionary<NamedType, ResizedVectorInterface> duplicateDimensionVectors)
    {
        VectorGroups = new(resolvedVectors);

        UnresolvedVectors = new(unresolvedVectors);
        DuplicateDimensionVectors = new(duplicateDimensionVectors);
    }
}

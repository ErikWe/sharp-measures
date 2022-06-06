namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;

internal class VectorPopulation
{
    public EquatableDictionary<NamedType, ResizedVectorGroup> VectorGroups { get; }

    public EquatableDictionary<NamedType, VectorInterface> UnresolvedVectors { get; }
    public EquatableDictionary<NamedType, VectorInterface> DuplicateDimensionVectors { get; }

    public VectorPopulation(EquatableDictionary<NamedType, ResizedVectorGroup> resolvedVectors,
        EquatableDictionary<NamedType, VectorInterface> unresolvedVectors, EquatableDictionary<NamedType, VectorInterface> duplicateDimensionVectors)
    {
        VectorGroups = resolvedVectors;

        UnresolvedVectors = unresolvedVectors;
        DuplicateDimensionVectors = duplicateDimensionVectors;
    }
}

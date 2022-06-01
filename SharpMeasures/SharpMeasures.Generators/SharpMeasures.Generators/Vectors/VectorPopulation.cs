namespace SharpMeasures.Generators.Vectors;

internal class VectorPopulation
{
    public NamedTypePopulation<ResizedVectorGroup> VectorGroups { get; }

    public NamedTypePopulation<VectorInterface> UnresolvedVectors { get; }
    public NamedTypePopulation<VectorInterface> DuplicateDimensionVectors { get; }

    public VectorPopulation(NamedTypePopulation<ResizedVectorGroup> resolvedVectors, NamedTypePopulation<VectorInterface> unresolvedVectors,
        NamedTypePopulation<VectorInterface> duplicateDimensionVectors)
    {
        VectorGroups = resolvedVectors;

        UnresolvedVectors = unresolvedVectors;
        DuplicateDimensionVectors = duplicateDimensionVectors;
    }
}

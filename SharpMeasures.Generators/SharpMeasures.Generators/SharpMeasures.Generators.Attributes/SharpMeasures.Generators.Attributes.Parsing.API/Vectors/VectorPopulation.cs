namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class VectorPopulation
{
    public ReadOnlyEquatableDictionary<NamedType, IVectorInterface> AllVectors { get; }
    public ReadOnlyEquatableDictionary<NamedType, ResizedVectorGroup> ResizedVectorGroups { get; }

    public VectorPopulation(IReadOnlyDictionary<NamedType, IVectorInterface> allVectors, IReadOnlyDictionary<NamedType, ResizedVectorGroup> resizedVectorGroups)
    {
        AllVectors = allVectors.AsReadOnlyEquatable();
        ResizedVectorGroups = resizedVectorGroups.AsReadOnlyEquatable();
    }
}

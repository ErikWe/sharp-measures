namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;

using System;
using System.Collections.Generic;

public class VectorCollection : ReadOnlyEquatableList<ResizedVectorInterface>
{
    new public static VectorCollection Empty { get; } = new(Array.Empty<ResizedVectorInterface>());

    public static VectorCollection FromGroup(ResizedVectorGroup sizeGroup)
    {
        ResizedVectorInterface[] vectors = new ResizedVectorInterface[sizeGroup.VectorsByDimension.Count];

        int index = 0;
        foreach (ResizedVectorInterface vector in sizeGroup.VectorsByDimension.Values)
        {
            vectors[index++] = vector;
        }

        return new(vectors);
    }

    public VectorCollection(IReadOnlyList<ResizedVectorInterface> vectors) : base(vectors) { }
    public VectorCollection(params ResizedVectorInterface[] vectors) : base(vectors) { }
}

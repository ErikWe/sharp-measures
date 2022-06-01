namespace SharpMeasures.Generators.Vectors;

using System;
using System.Collections.Generic;
using System.Linq;

internal record class VectorCollection
{
    public static VectorCollection Empty { get; } = new(Array.Empty<VectorInterface>());

    public static VectorCollection FromGroup(ResizedVectorGroup sizeGroup)
    {
        VectorInterface[] vectors = new VectorInterface[sizeGroup.VectorsByDimension.Count];

        int index = 0;
        foreach (VectorInterface vector in sizeGroup.VectorsByDimension.Values)
        {
            vectors[index++] = vector;
        }

        return new(vectors);
    }

    public IReadOnlyList<VectorInterface> Vectors { get; }

    public VectorCollection(IReadOnlyList<VectorInterface> vectors)
    {
        Vectors = vectors;
    }

    public VectorCollection(params VectorInterface[] vectors)
    {
        Vectors = vectors;
    }

    public virtual bool Equals(VectorCollection other)
    {
        if (other is null)
        {
            return false;
        }

        return Vectors.SequenceEqual(other.Vectors);
    }

    public override int GetHashCode()
    {
        return Vectors.GetSequenceHashCode();
    }
}

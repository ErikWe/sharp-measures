namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public record class ResizedVectorGroup
{
    public static IBuilder StartBuilder(VectorInterface root) => new Builder(root);
    
    public VectorInterface Root { get; }

    public IReadOnlyDictionary<int, ResizedVectorInterface> VectorsByDimension => VectorsByDimensionBuilder;
    private EquatableDictionary<int, ResizedVectorInterface> VectorsByDimensionBuilder { get; } = new();

    private ResizedVectorGroup(VectorInterface root)
    {
        Root = root;

        VectorsByDimensionBuilder.Add(root.Dimension, new ResizedVectorInterface(root.VectorType, root.VectorType, root.Dimension));
    }

    [SuppressMessage("Design", "CA1034", Justification = "Builder")]
    public interface IBuilder
    {
        public abstract ResizedVectorGroup Target { get; }
        public abstract void AddResizedVector(ResizedVectorInterface vector);
        public abstract bool HasVectorOfDimension(int dimension);
    }

    private class Builder : IBuilder
    {
        public ResizedVectorGroup Target { get; }

        public Builder(VectorInterface root)
        {
            Target = new(root);
        }

        public void AddResizedVector(ResizedVectorInterface vector)
        {
            Target.VectorsByDimensionBuilder.Add(vector.Dimension, vector);
        }

        public bool HasVectorOfDimension(int dimension) => Target.VectorsByDimensionBuilder.ContainsKey(dimension);
    }
}

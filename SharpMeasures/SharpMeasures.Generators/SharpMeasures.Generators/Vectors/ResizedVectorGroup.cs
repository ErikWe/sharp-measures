namespace SharpMeasures.Generators.Vectors;

using System.Collections.Generic;

internal record class ResizedVectorGroup
{
    public static IBuilder StartBuilder(RootVectorInterface root) => new Builder(root);
    
    public RootVectorInterface Root { get; }

    public IReadOnlyDictionary<int, VectorInterface> VectorsByDimension => VectorsByDimensionBuilder;
    private Dictionary<int, VectorInterface> VectorsByDimensionBuilder { get; } = new();

    private ResizedVectorGroup(RootVectorInterface root)
    {
        Root = root;
    }

    public interface IBuilder
    {
        public abstract ResizedVectorGroup Target { get; }
        public abstract void AddResizedVector(VectorInterface vector);
        public abstract bool HasVectorOfDimension(int dimension);
    }

    private class Builder : IBuilder
    {
        public ResizedVectorGroup Target { get; }

        public Builder(RootVectorInterface root)
        {
            Target = new(root);
            AddResizedVector(root);
        }

        public void AddResizedVector(VectorInterface vector)
        {
            Target.VectorsByDimensionBuilder.Add(vector.Dimension, vector);
        }

        public bool HasVectorOfDimension(int dimension) => Target.VectorsByDimensionBuilder.ContainsKey(dimension);
    }

    public virtual bool Equals(ResizedVectorGroup other)
    {
        if (other is null)
        {
            return false;
        }

        return VectorsByDimension.OrderIndependentSequenceEquals(other.VectorsByDimension);
    }

    public override int GetHashCode()
    {
        return VectorsByDimension.GetOrderIndependentHashCode();
    }
}

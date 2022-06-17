namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public record class ResizedVectorGroup
{
    public static IBuilder StartBuilder(SharpMeasuresVectorInterface root) => new Builder(root);
    
    public SharpMeasuresVectorInterface Root { get; }

    public IReadOnlyDictionary<int, ResizedSharpMeasuresVectorInterface> VectorsByDimension => VectorsByDimensionBuilder;
    private EquatableDictionary<int, ResizedSharpMeasuresVectorInterface> VectorsByDimensionBuilder { get; } = EquatableDictionary<int, ResizedSharpMeasuresVectorInterface>.Empty;

    private bool Built { get; set; }
    private int? CachedHashCode { get; set; }

    private ResizedVectorGroup(SharpMeasuresVectorInterface root)
    {
        Root = root;

        VectorsByDimensionBuilder.Add(root.Dimension, new ResizedSharpMeasuresVectorInterface(root.VectorType, root.VectorType, root.Dimension, root.IncludedUnits,
            root.ExcludedUnits, root.DimensionalEquivalences));
    }

    public virtual bool Equals(ResizedVectorGroup? other)
    {
        if (other is null || CachedHashCode is not null && other.CachedHashCode is not null && CachedHashCode != other.CachedHashCode)
        {
            return false;
        }

        return Root == other.Root && VectorsByDimensionBuilder.Equals(other.VectorsByDimensionBuilder);
    }

    public override int GetHashCode()
    {
        if (CachedHashCode is not null)
        {
            return CachedHashCode.Value;
        }

        int hashCode = (Root, VectorsByDimensionBuilder.GetOrderIndependentHashCode()).GetHashCode();

        if (Built)
        {
            CachedHashCode = hashCode;
        }

        return hashCode;
    }

    [SuppressMessage("Design", "CA1034", Justification = "Builder")]
    public interface IBuilder
    {
        public abstract void AddResizedVector(ResizedSharpMeasuresVectorInterface vector);
        public abstract bool HasVectorOfDimension(int dimension);

        public abstract ResizedVectorGroup Build();
    }

    private class Builder : IBuilder
    {
        private ResizedVectorGroup Target { get; }

        public Builder(SharpMeasuresVectorInterface root)
        {
            Target = new(root);
        }

        public void AddResizedVector(ResizedSharpMeasuresVectorInterface vector)
        {
            Target.VectorsByDimensionBuilder.Add(vector.Dimension, vector);
        }

        public ResizedVectorGroup Build()
        {
            Target.Built = true;

            return Target;
        }

        public bool HasVectorOfDimension(int dimension) => Target.VectorsByDimensionBuilder.ContainsKey(dimension);
    }
}

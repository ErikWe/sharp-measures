namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public record class ResizedGroup
{
    public static IBuilder StartBuilder(IRootVector root) => new Builder(root);
    
    public IRootVector Root { get; }

    public IReadOnlyDictionary<int, ResizedVector> VectorsByDimension => VectorsByDimensionBuilder;
    private EquatableDictionary<int, ResizedVector> VectorsByDimensionBuilder { get; } = EquatableDictionary<int, ResizedVector>.Empty;

    private bool Built { get; set; }
    private int? CachedHashCode { get; set; }

    private ResizedGroup(IRootVector root)
    {
        Root = root;

        VectorsByDimensionBuilder.Add(root.Dimension, new ResizedVector(root.VectorType, root.VectorType, root.Dimension, root.IncludedUnits,
            root.ExcludedUnits, root.DimensionalEquivalences));
    }

    public virtual bool Equals(ResizedGroup? other)
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
        public abstract void AddResizedVector(ResizedVector vector);
        public abstract bool HasVectorOfDimension(int dimension);

        public abstract ResizedGroup Build();
    }

    private class Builder : IBuilder
    {
        private ResizedGroup Target { get; }

        public Builder(IRootVector root)
        {
            Target = new(root);
        }

        public void AddResizedVector(ResizedVector vector)
        {
            Target.VectorsByDimensionBuilder.Add(vector.Dimension, vector);
        }

        public ResizedGroup Build()
        {
            Target.Built = true;

            return Target;
        }

        public bool HasVectorOfDimension(int dimension) => Target.VectorsByDimensionBuilder.ContainsKey(dimension);
    }
}

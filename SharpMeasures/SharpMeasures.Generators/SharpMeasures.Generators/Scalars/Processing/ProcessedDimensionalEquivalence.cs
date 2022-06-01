namespace SharpMeasures.Generators.Scalars.Processing;

using System;
using System.Collections.Generic;
using System.Linq;

internal readonly record struct ProcessedDimensionalEquivalence
{
    public ICollection<ScalarInterface> EquivalentQuantitiesWithoutCast { get; } = new List<ScalarInterface>();
    public ICollection<ScalarInterface> EquivalentQuantitiesWithImplicitCast { get; } = new List<ScalarInterface>();
    public ICollection<ScalarInterface> EquivalentQuantitiesWithExplicitCast { get; } = new List<ScalarInterface>();

    public IEnumerable<ScalarInterface> EquivalentQuantities => EquivalentQuantitiesWithoutCast.Concat(EquivalentQuantitiesWithImplicitCast)
        .Concat(EquivalentQuantitiesWithExplicitCast);

    public ProcessedDimensionalEquivalence() { }

    public bool Equals(ProcessedDimensionalEquivalence other)
    {
        return EquivalentQuantitiesWithoutCast.SequenceEqual(other.EquivalentQuantitiesWithoutCast)
            && EquivalentQuantitiesWithExplicitCast.SequenceEqual(other.EquivalentQuantitiesWithExplicitCast)
            && EquivalentQuantitiesWithImplicitCast.SequenceEqual(other.EquivalentQuantitiesWithImplicitCast);
    }

    public override int GetHashCode()
    {
        return EquivalentQuantitiesWithoutCast.GetSequenceHashCode() ^ EquivalentQuantitiesWithExplicitCast.GetSequenceHashCode()
            ^ EquivalentQuantitiesWithImplicitCast.GetSequenceHashCode();
    }
}

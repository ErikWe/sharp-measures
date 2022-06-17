namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IVectorInterface
{
    public abstract NamedType VectorType { get; }
    public abstract int Dimension { get; }

    public abstract IEnumerable<IncludeUnitsInterface> IncludedUnits { get; }
    public abstract IEnumerable<ExcludeUnitsInterface> ExcludedUnits { get; }
    public abstract IEnumerable<DimensionalEquivalenceInterface> DimensionalEquivalences { get; }
}

namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public class ResizedVectorInterface : IAssociatedVectorInterface
{
    public NamedType VectorType { get; }

    public NamedType AssociatedVector { get; }
    public int Dimension { get; }

    public EquatableEnumerable<IncludeUnitsInterface> IncludedUnits { get; }
    public EquatableEnumerable<ExcludeUnitsInterface> ExcludedUnits { get; }
    public EquatableEnumerable<DimensionalEquivalenceInterface> DimensionalEquivalences { get; }

    IEnumerable<IncludeUnitsInterface> IVectorInterface.IncludedUnits => IncludedUnits;
    IEnumerable<ExcludeUnitsInterface> IVectorInterface.ExcludedUnits => ExcludedUnits;
    IEnumerable<DimensionalEquivalenceInterface> IVectorInterface.DimensionalEquivalences => DimensionalEquivalences;

    public ResizedVectorInterface(NamedType vectorType, NamedType associatedVector, int dimension, IEnumerable<IncludeUnitsInterface> includedUnits,
        IEnumerable<ExcludeUnitsInterface> excludedUnits, IEnumerable<DimensionalEquivalenceInterface> dimensionalEquivalences)
    {
        VectorType = vectorType;

        AssociatedVector = associatedVector;
        Dimension = dimension;

        IncludedUnits = includedUnits.AsEquatable();
        ExcludedUnits = excludedUnits.AsEquatable();

        DimensionalEquivalences = dimensionalEquivalences.AsEquatable();
    }
}

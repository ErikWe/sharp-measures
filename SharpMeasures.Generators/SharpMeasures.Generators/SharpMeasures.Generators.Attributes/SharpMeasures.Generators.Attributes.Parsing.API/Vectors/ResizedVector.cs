namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing.InclusionExclusion;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

public class ResizedVector : ILinkedVector
{
    public NamedType VectorType { get; }

    public NamedType AssociatedVector { get; }
    public int Dimension { get; }

    public EquatableEnumerable<IncludeUnitsInterface> IncludedUnits { get; }
    public EquatableEnumerable<ExcludeUnitsInterface> ExcludedUnits { get; }
    public EquatableEnumerable<DimensionalEquivalenceInterface> DimensionalEquivalences { get; }

    public ResizedVector(NamedType vectorType, NamedType associatedVector, int dimension, IEnumerable<IncludeUnitsInterface> includedUnits,
        IEnumerable<ExcludeUnitsInterface> excludedUnits, IEnumerable<DimensionalEquivalenceInterface> dimensionalEquivalences)
    {
        VectorType = vectorType;

        AssociatedVector = associatedVector;
        Dimension = dimension;

        IncludedUnits = includedUnits.AsEquatable();
        ExcludedUnits = excludedUnits.AsEquatable();

        DimensionalEquivalences = dimensionalEquivalences.AsEquatable();
    }

    [SuppressMessage("Design", "CA1033", Justification = "Available under another name")]
    NamedType ILinkedVector.LinkedTo => AssociatedVector;

    IEnumerable<IncludeUnitsInterface> IVector.IncludedUnits => IncludedUnits;
    IEnumerable<ExcludeUnitsInterface> IVector.ExcludedUnits => ExcludedUnits;
    IEnumerable<DimensionalEquivalenceInterface> IVector.DimensionalEquivalences => DimensionalEquivalences;

    protected virtual IEnumerable<string> AllIncludedUnits => IncludedUnits.SelectMany(static (x) => x.IncludedUnits);
    protected virtual IEnumerable<string> AllExcludedUnits => ExcludedUnits.SelectMany(static (x) => x.ExcludedUnits);

    [SuppressMessage("Design", "CA1033", Justification = "Available under another name")]
    NamedType IInclusionExclusion<string>.Type => VectorType;
    [SuppressMessage("Design", "CA1033", Justification = "Available under another name")]
    IEnumerable<string> IInclusionExclusion<string>.Inclusions => AllIncludedUnits;
    [SuppressMessage("Design", "CA1033", Justification = "Available under another name")]
    IEnumerable<string> IInclusionExclusion<string>.Exclusions => AllExcludedUnits;
}

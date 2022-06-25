namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing.InclusionExclusion;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

public record class BaseVector : IRootVector
{
    public NamedType VectorType { get; }

    public NamedType UnitType { get; }
    public NamedType? ScalarType { get; }

    public int Dimension { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public NamedType Difference { get; }

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public EquatableEnumerable<IncludeUnitsInterface> IncludedUnits { get; }
    public EquatableEnumerable<ExcludeUnitsInterface> ExcludedUnits { get; }
    public EquatableEnumerable<DimensionalEquivalenceInterface> DimensionalEquivalences { get; }

    public BaseVector(NamedType vectorType, NamedType unitType, NamedType? scalarType, int dimension, bool implementSum, bool implementDifference,
        NamedType difference, string? defaultUnitName, string? defaultUnitSymbol,
        IEnumerable<IncludeUnitsInterface> includedUnits, IEnumerable<ExcludeUnitsInterface> excludedUnits,
        IEnumerable<DimensionalEquivalenceInterface> dimensionalEquivalences)
    {
        VectorType = vectorType;

        UnitType = unitType;
        ScalarType = scalarType;

        Dimension = dimension;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Difference = difference;

        DefaultUnitName = defaultUnitName;
        DefaultUnitSymbol = defaultUnitSymbol;

        IncludedUnits = includedUnits.AsEquatable();
        ExcludedUnits = excludedUnits.AsEquatable();

        DimensionalEquivalences = dimensionalEquivalences.AsEquatable();
    }

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

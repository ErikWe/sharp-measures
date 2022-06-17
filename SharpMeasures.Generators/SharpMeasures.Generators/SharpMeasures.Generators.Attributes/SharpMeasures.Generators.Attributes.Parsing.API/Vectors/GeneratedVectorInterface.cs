namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public record class GeneratedVectorInterface : IVectorInterface
{
    public NamedType VectorType { get; }

    public NamedType UnitType { get; }
    public NamedType? ScalarType { get; }

    public int Dimension { get; }

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public EquatableEnumerable<IncludeUnitsInterface> IncludedUnits { get; }
    public EquatableEnumerable<ExcludeUnitsInterface> ExcludedUnits { get; }
    public EquatableEnumerable<DimensionalEquivalenceInterface> DimensionalEquivalences { get; }

    IEnumerable<IncludeUnitsInterface> IVectorInterface.IncludedUnits => IncludedUnits;
    IEnumerable<ExcludeUnitsInterface> IVectorInterface.ExcludedUnits => ExcludedUnits;
    IEnumerable<DimensionalEquivalenceInterface> IVectorInterface.DimensionalEquivalences => DimensionalEquivalences;

    public GeneratedVectorInterface(NamedType vectorType, NamedType unitType, NamedType? scalarType, int dimension, string? defaultUnitName, string? defaultUnitSymbol,
        IEnumerable<IncludeUnitsInterface> includedUnits, IEnumerable<ExcludeUnitsInterface> excludedUnits,
        IEnumerable<DimensionalEquivalenceInterface> dimensionalEquivalences)
    {
        VectorType = vectorType;

        UnitType = unitType;
        ScalarType = scalarType;

        Dimension = dimension;

        DefaultUnitName = defaultUnitName;
        DefaultUnitSymbol = defaultUnitSymbol;

        IncludedUnits = includedUnits.AsEquatable();
        ExcludedUnits = excludedUnits.AsEquatable();

        DimensionalEquivalences = dimensionalEquivalences.AsEquatable();
    }
}

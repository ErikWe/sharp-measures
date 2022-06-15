namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public record class ScalarInterface
{
    public DefinedType ScalarType { get; }
    public NamedType UnitType { get; }

    public bool Biased { get; }

    public NamedType? Reciprocal { get; }
    public NamedType? Square { get; }
    public NamedType? Cube { get; }
    public NamedType? SquareRoot { get; }
    public NamedType? CubeRoot { get; }

    public EquatableEnumerable<IncludeBasesInterface> IncludedBases { get; }
    public EquatableEnumerable<ExcludeBasesInterface> ExcludedBases { get; }

    public EquatableEnumerable<IncludeUnitsInterface> IncludedUnits { get; }
    public EquatableEnumerable<ExcludeUnitsInterface> ExcludedUnits { get; }

    public EquatableEnumerable<DimensionalEquivalenceInterface> DimensionalEquivalences { get; }

    public ScalarInterface(DefinedType scalarType, NamedType unittype, bool biased, NamedType? reciprocal, NamedType? square, NamedType? cube, NamedType? squareRoot,
        NamedType? cubeRoot, IEnumerable<IncludeBasesInterface> includedBases, IEnumerable<ExcludeBasesInterface> excludedBases,
        IEnumerable<IncludeUnitsInterface> includedUnits, IEnumerable<ExcludeUnitsInterface> excludedUnits,
        IEnumerable<DimensionalEquivalenceInterface> dimensionalEquivalences)
    {
        ScalarType = scalarType;
        UnitType = unittype;

        Biased = biased;

        Reciprocal = reciprocal;
        Square = square;
        Cube = cube;
        SquareRoot = squareRoot;
        CubeRoot = cubeRoot;

        IncludedBases = includedBases.AsEquatable();
        ExcludedBases = excludedBases.AsEquatable();

        IncludedUnits = includedUnits.AsEquatable();
        ExcludedUnits = excludedUnits.AsEquatable();

        DimensionalEquivalences = dimensionalEquivalences.AsEquatable();
    }
}

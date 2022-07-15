namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections.Generic;

internal record class InheritanceData
{
    public IBaseScalar BaseScalar { get; }

    public IUnresolvedUnitInstance? DefaultUnit { get; }
    public string? DefaultUnitSymbol { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public IUnresolvedScalarType Difference { get; }

    public IReadOnlyList<IUnitInstance> IncludedBases => includedBases;
    public IReadOnlyList<IUnitInstance> IncludedUnits => includedUnits;

    public IReadOnlyList<IDerivedQuantity> Derivations => derivations;
    public IReadOnlyList<IScalarConstant> Constants => constants;
    public IReadOnlyList<IConvertibleScalar> ConvertibleScalars => convertibleScalars;

    public IUnresolvedScalarType? Reciprocal { get; }
    public IUnresolvedScalarType? Square { get; }
    public IUnresolvedScalarType? Cube { get; }
    public IUnresolvedScalarType? SquareRoot { get; }
    public IUnresolvedScalarType? CubeRoot { get; }

    private ReadOnlyEquatableList<IUnitInstance> includedBases { get; }
    private ReadOnlyEquatableList<IUnitInstance> includedUnits { get; }

    private ReadOnlyEquatableList<IDerivedQuantity> derivations { get; }
    private ReadOnlyEquatableList<IScalarConstant> constants { get; }
    private ReadOnlyEquatableList<IConvertibleScalar> convertibleScalars { get; }

    public InheritanceData(IBaseScalar baseScalar, IUnresolvedUnitInstance? defaultUnit, string? defaultUnitSymbol, bool implementSum, bool implementDifference,
        IUnresolvedScalarType difference, IReadOnlyList<IUnitInstance> includedBases,
        IReadOnlyList<IUnitInstance> includedUnits, IReadOnlyList<IDerivedQuantity> derivations, IReadOnlyList<IScalarConstant> constants,
        IReadOnlyList<IConvertibleScalar> convertibleScalars, IUnresolvedScalarType? reciprocal, IUnresolvedScalarType? square, IUnresolvedScalarType? cube,
        IUnresolvedScalarType? squareRoot, IUnresolvedScalarType? cubeRoot)
    {
        BaseScalar = baseScalar;

        DefaultUnit = defaultUnit;
        DefaultUnitSymbol = defaultUnitSymbol;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Difference = difference;

        this.includedBases = includedBases.AsReadOnlyEquatable();
        this.includedUnits = includedUnits.AsReadOnlyEquatable();

        this.constants = constants.AsReadOnlyEquatable();
        this.derivations = derivations.AsReadOnlyEquatable();
        this.convertibleScalars = convertibleScalars.AsReadOnlyEquatable();

        Reciprocal = reciprocal;
        Square = square;
        Cube = cube;
        SquareRoot = squareRoot;
        CubeRoot = cubeRoot;
    }
}

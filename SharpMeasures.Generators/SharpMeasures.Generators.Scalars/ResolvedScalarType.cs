namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal record class ResolvedScalarType : IResolvedScalarType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public NamedType Unit { get; }
    public bool UseUnitBias { get; }

    public NamedType? Vector { get; }

    public NamedType? Reciprocal { get; }
    public NamedType? Square { get; }
    public NamedType? Cube { get; }
    public NamedType? SquareRoot { get; }
    public NamedType? CubeRoot { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }
    public NamedType? Difference { get; }

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public IReadOnlyList<IDerivedQuantity> Derivations => derivations;
    public IReadOnlyList<IScalarConstant> Constants => constants;
    public IReadOnlyList<IConvertibleScalar> Conversions => conversions;

    public IReadOnlyList<string> IncludedBases => includedBases;
    public IReadOnlyList<string> IncludedUnits => includedUnits;

    private ReadOnlyEquatableList<IDerivedQuantity> derivations { get; }
    private ReadOnlyEquatableList<IScalarConstant> constants { get; }
    private ReadOnlyEquatableList<IConvertibleScalar> conversions { get; }

    private ReadOnlyEquatableList<string> includedBases { get; }
    private ReadOnlyEquatableList<string> includedUnits { get; }

    public bool? GenerateDocumentation { get; }

    IReadOnlyList<IConvertibleQuantity> IResolvedQuantityType.Conversions => Conversions;

    public ResolvedScalarType(DefinedType type, MinimalLocation typeLocation, NamedType unit, bool useUnitBias, NamedType? vector, NamedType? reciprocal, NamedType? square, NamedType? cube, NamedType? squareRoot,
        NamedType? cubeRoot, bool implementSum, bool implementDifference, NamedType? difference, string? defaultUnitName, string? defaultUnitSymbol, IReadOnlyList<IDerivedQuantity> derivations,
        IReadOnlyList<IScalarConstant> constants, IReadOnlyList<IConvertibleScalar> conversions, IReadOnlyList<string> includedBases, IReadOnlyList<string> includedUnits, bool? generateDocumentation)
    {
        Type = type;
        TypeLocation = typeLocation;

        Unit = unit;
        UseUnitBias = useUnitBias;

        Vector = vector;

        Reciprocal = reciprocal;
        Square = square;
        Cube = cube;
        SquareRoot = squareRoot;
        CubeRoot = cubeRoot;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;
        Difference = difference;

        DefaultUnitName = defaultUnitName;
        DefaultUnitSymbol = defaultUnitSymbol;

        this.derivations = derivations.AsReadOnlyEquatable();
        this.constants = constants.AsReadOnlyEquatable();
        this.conversions = conversions.AsReadOnlyEquatable();

        this.includedBases = includedBases.AsReadOnlyEquatable();
        this.includedUnits = includedUnits.AsReadOnlyEquatable();

        GenerateDocumentation = generateDocumentation;
    }
}

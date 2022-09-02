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

    public string? DefaultUnitInstanceName { get; }
    public string? DefaultUnitInstanceSymbol { get; }

    public IReadOnlyList<IDerivedQuantity> Derivations => derivations;
    public IReadOnlyList<IScalarConstant> Constants => constants;
    public IReadOnlyList<IConvertibleQuantity> Conversions => conversions;

    public IReadOnlyList<string> IncludedUnitBaseInstancesNames => includedUnitBaseInstanceNames;
    public IReadOnlyList<string> IncludedUnitInstanceNames => includedUnitInstanceNames;

    private ReadOnlyEquatableList<IDerivedQuantity> derivations { get; }
    private ReadOnlyEquatableList<IScalarConstant> constants { get; }
    private ReadOnlyEquatableList<IConvertibleQuantity> conversions { get; }

    private ReadOnlyEquatableList<string> includedUnitBaseInstanceNames { get; }
    private ReadOnlyEquatableList<string> includedUnitInstanceNames { get; }

    public bool? GenerateDocumentation { get; }

    IReadOnlyList<IConvertibleQuantity> IResolvedQuantityType.Conversions => Conversions;

    public ResolvedScalarType(DefinedType type, MinimalLocation typeLocation, NamedType unit, bool useUnitBias, NamedType? vector, NamedType? reciprocal, NamedType? square, NamedType? cube, NamedType? squareRoot,
        NamedType? cubeRoot, bool implementSum, bool implementDifference, NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol, IReadOnlyList<IDerivedQuantity> derivations,
        IReadOnlyList<IScalarConstant> constants, IReadOnlyList<IConvertibleQuantity> conversions, IReadOnlyList<string> includedUnitBaseInstanceNames, IReadOnlyList<string> includedUnitInstanceNames, bool? generateDocumentation)
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

        DefaultUnitInstanceName = defaultUnitInstanceName;
        DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol;

        this.derivations = derivations.AsReadOnlyEquatable();
        this.constants = constants.AsReadOnlyEquatable();
        this.conversions = conversions.AsReadOnlyEquatable();

        this.includedUnitBaseInstanceNames = includedUnitBaseInstanceNames.AsReadOnlyEquatable();
        this.includedUnitInstanceNames = includedUnitInstanceNames.AsReadOnlyEquatable();

        GenerateDocumentation = generateDocumentation;
    }
}

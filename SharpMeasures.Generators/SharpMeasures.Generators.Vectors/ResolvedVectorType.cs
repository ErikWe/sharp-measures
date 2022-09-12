namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal record class ResolvedVectorType : IResolvedVectorType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public int Dimension { get; }

    public NamedType Unit { get; }

    public NamedType? Scalar { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }
    public NamedType? Difference { get; }

    public string? DefaultUnitInstanceName { get; }
    public string? DefaultUnitInstanceSymbol { get; }

    public IReadOnlyList<IDerivedQuantity> DefinedDerivations => definedDerivations;
    public IReadOnlyList<IDerivedQuantity> InheritedDerivations => inheritedDerivations;
    public IReadOnlyList<IVectorConstant> Constants => constants;
    public IReadOnlyList<IConvertibleQuantity> Conversions => conversions;

    public IReadOnlyList<string> IncludedUnitInstanceNames => includedUnits;

    private ReadOnlyEquatableList<IDerivedQuantity> definedDerivations { get; }
    private ReadOnlyEquatableList<IDerivedQuantity> inheritedDerivations { get; }
    private ReadOnlyEquatableList<IVectorConstant> constants { get; }
    private ReadOnlyEquatableList<IConvertibleQuantity> conversions { get; }

    private ReadOnlyEquatableList<string> includedUnits { get; }

    public bool? GenerateDocumentation { get; }

    IReadOnlyList<IConvertibleQuantity> IResolvedQuantityType.Conversions => Conversions;

    public ResolvedVectorType(DefinedType type, MinimalLocation typeLocation, int dimension, NamedType unit, NamedType? scalar, bool implementSum, bool implementDifference, NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol,
        IReadOnlyList<IDerivedQuantity> definedDerivations, IReadOnlyList<IDerivedQuantity> inheritedDerivations, IReadOnlyList<IVectorConstant> constants, IReadOnlyList<IConvertibleQuantity> conversions, IReadOnlyList<string> includedUnits, bool? generateDocumentation)
    {
        Type = type;
        TypeLocation = typeLocation;

        Dimension = dimension;

        Unit = unit;

        Scalar = scalar;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;
        Difference = difference;

        DefaultUnitInstanceName = defaultUnitInstanceName;
        DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol;

        this.definedDerivations = definedDerivations.AsReadOnlyEquatable();
        this.inheritedDerivations = inheritedDerivations.AsReadOnlyEquatable();
        this.constants = constants.AsReadOnlyEquatable();
        this.conversions = conversions.AsReadOnlyEquatable();

        this.includedUnits = includedUnits.AsReadOnlyEquatable();

        GenerateDocumentation = generateDocumentation;
    }
}

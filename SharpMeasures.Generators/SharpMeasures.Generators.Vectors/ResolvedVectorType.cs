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

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public IReadOnlyList<IDerivedQuantity> Derivations => derivations;
    public IReadOnlyList<IVectorConstant> Constants => constants;
    public IReadOnlyList<IConvertibleVector> Conversions => conversions;

    public IReadOnlyList<string> IncludedUnits => includedUnits;

    private ReadOnlyEquatableList<IDerivedQuantity> derivations { get; }
    private ReadOnlyEquatableList<IVectorConstant> constants { get; }
    private ReadOnlyEquatableList<IConvertibleVector> conversions { get; }

    private ReadOnlyEquatableList<string> includedUnits { get; }

    public bool? GenerateDocumentation { get; }

    IReadOnlyList<IConvertibleQuantity> IResolvedQuantityType.Conversions => Conversions;

    public ResolvedVectorType(DefinedType type, MinimalLocation typeLocation, int dimension, NamedType unit, NamedType? scalar, bool implementSum, bool implementDifference, NamedType? difference,
        string? defaultUnitName, string? defaultUnitSymbol, IReadOnlyList<IDerivedQuantity> derivations, IReadOnlyList<IVectorConstant> constants, IReadOnlyList<IConvertibleVector> conversions,
        IReadOnlyList<string> includedUnits, bool? generateDocumentation)
    {
        Type = type;
        TypeLocation = typeLocation;

        Dimension = dimension;

        Unit = unit;

        Scalar = scalar;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;
        Difference = difference;

        DefaultUnitName = defaultUnitName;
        DefaultUnitSymbol = defaultUnitSymbol;

        this.derivations = derivations.AsReadOnlyEquatable();
        this.constants = constants.AsReadOnlyEquatable();
        this.conversions = conversions.AsReadOnlyEquatable();

        this.includedUnits = includedUnits.AsReadOnlyEquatable();

        GenerateDocumentation = generateDocumentation;
    }
}

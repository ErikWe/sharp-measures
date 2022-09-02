namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal record class ResolvedGroupType : IResolvedVectorGroupType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public NamedType Unit { get; }

    public NamedType? Scalar { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }
    public NamedType? Difference { get; }

    public string? DefaultUnitInstanceName { get; }
    public string? DefaultUnitInstanceSymbol { get; }

    public IReadOnlyDictionary<int, NamedType> MembersByDimension => membersByDimension;

    public IReadOnlyList<IDerivedQuantity> Derivations => derivations;
    public IReadOnlyList<IConvertibleQuantity> Conversions => conversions;

    public IReadOnlyList<string> IncludedUnitInstanceNames => includedUnits;

    private ReadOnlyEquatableDictionary<int, NamedType> membersByDimension { get; }

    private ReadOnlyEquatableList<IDerivedQuantity> derivations { get; }
    private ReadOnlyEquatableList<IConvertibleQuantity> conversions { get; }

    private ReadOnlyEquatableList<string> includedUnits { get; }

    public bool? GenerateDocumentation { get; }

    IReadOnlyList<IConvertibleQuantity> IResolvedQuantityType.Conversions => Conversions;

    public ResolvedGroupType(DefinedType type, MinimalLocation typeLocation, NamedType unit, NamedType? scalar, bool implementSum, bool implementDifference, NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol,
        IReadOnlyDictionary<int, NamedType> membersByDimension, IReadOnlyList<IDerivedQuantity> derivations, IReadOnlyList<IConvertibleQuantity> conversions, IReadOnlyList<string> includedUnits, bool? generateDocumentation)
    {
        Type = type;
        TypeLocation = typeLocation;

        Unit = unit;

        Scalar = scalar;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;
        Difference = difference;

        DefaultUnitInstanceName = defaultUnitInstanceName;
        DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol;

        this.membersByDimension = membersByDimension.AsReadOnlyEquatable();

        this.derivations = derivations.AsReadOnlyEquatable();
        this.conversions = conversions.AsReadOnlyEquatable();

        this.includedUnits = includedUnits.AsReadOnlyEquatable();

        GenerateDocumentation = generateDocumentation;
    }
}

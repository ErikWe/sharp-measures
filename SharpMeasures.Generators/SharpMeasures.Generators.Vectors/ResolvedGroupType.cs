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

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public IReadOnlyDictionary<int, NamedType> MembersByDimension => membersByDimension;

    public IReadOnlyList<IDerivedQuantity> Derivations => derivations;
    public IReadOnlyList<IConvertibleVector> Conversions => conversions;

    public IReadOnlyList<string> IncludedUnits => includedUnits;

    private ReadOnlyEquatableDictionary<int, NamedType> membersByDimension { get; }

    private ReadOnlyEquatableList<IDerivedQuantity> derivations { get; }
    private ReadOnlyEquatableList<IConvertibleVector> conversions { get; }

    private ReadOnlyEquatableList<string> includedUnits { get; }

    public bool? GenerateDocumentation { get; }

    IReadOnlyList<IConvertibleQuantity> IResolvedQuantityType.Conversions => Conversions;

    public ResolvedGroupType(DefinedType type, MinimalLocation typeLocation, NamedType unit, NamedType? scalar, bool implementSum, bool implementDifference, NamedType? difference, string? defaultUnitName,
        string? defaultUnitSymbol, IReadOnlyDictionary<int, NamedType> membersByDimension, IReadOnlyList<IDerivedQuantity> derivations, IReadOnlyList<IConvertibleVector> conversions,
        IReadOnlyList<string> includedUnits, bool? generateDocumentation)
    {
        Type = type;
        TypeLocation = typeLocation;

        Unit = unit;

        Scalar = scalar;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;
        Difference = difference;

        DefaultUnitName = defaultUnitName;
        DefaultUnitSymbol = defaultUnitSymbol;

        this.membersByDimension = membersByDimension.AsReadOnlyEquatable();

        this.derivations = derivations.AsReadOnlyEquatable();
        this.conversions = conversions.AsReadOnlyEquatable();

        this.includedUnits = includedUnits.AsReadOnlyEquatable();

        GenerateDocumentation = generateDocumentation;
    }
}

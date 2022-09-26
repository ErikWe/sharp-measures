namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal sealed record class ResolvedGroupType : IResolvedVectorGroupType
{
    public DefinedType Type { get; }

    public NamedType Unit { get; }

    public NamedType? OriginalQuantity { get; }

    public ConversionOperatorBehaviour SpecializationForwardsConversionBehaviour { get; }
    public ConversionOperatorBehaviour SpecializationBackwardsConversionBehaviour { get; }

    public NamedType? Scalar { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }
    public NamedType? Difference { get; }

    public string? DefaultUnitInstanceName { get; }
    public string? DefaultUnitInstanceSymbol { get; }

    public IReadOnlyDictionary<int, NamedType> MembersByDimension { get; }

    public IReadOnlyList<IDerivedQuantity> Derivations { get; }
    public IReadOnlyList<IConvertibleQuantity> Conversions { get; }

    public IReadOnlyList<IDerivedQuantity> InheritedDerivations { get; }
    public IReadOnlyList<IConvertibleQuantity> InheritedConversions { get; }

    public IReadOnlyList<string> IncludedUnitInstanceNames { get; }

    public bool? GenerateDocumentation { get; }

    IReadOnlyList<IConvertibleQuantity> IResolvedQuantityType.Conversions => Conversions;

    public ResolvedGroupType(DefinedType type, NamedType unit, NamedType? originalQuantity, ConversionOperatorBehaviour specializationForwardsConversionBehaviour, ConversionOperatorBehaviour specializationBackwardsConversionBehaviour, NamedType? scalar, bool implementSum,
        bool implementDifference, NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol, IReadOnlyDictionary<int, NamedType> membersByDimension, IReadOnlyList<IDerivedQuantity> derivations, IReadOnlyList<IConvertibleQuantity> conversions,
        IReadOnlyList<IDerivedQuantity> inheritedDerivations, IReadOnlyList<IConvertibleQuantity> inheritedConversions, IReadOnlyList<string> includedUnitInstanceNames, bool? generateDocumentation)
    {
        Type = type;

        Unit = unit;

        OriginalQuantity = originalQuantity;

        SpecializationForwardsConversionBehaviour = specializationForwardsConversionBehaviour;
        SpecializationBackwardsConversionBehaviour = specializationBackwardsConversionBehaviour;

        Scalar = scalar;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;
        Difference = difference;

        DefaultUnitInstanceName = defaultUnitInstanceName;
        DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol;

        MembersByDimension = membersByDimension.AsReadOnlyEquatable();

        Derivations = derivations.AsReadOnlyEquatable();
        Conversions = conversions.AsReadOnlyEquatable();

        InheritedDerivations = inheritedDerivations.AsReadOnlyEquatable();
        InheritedConversions = inheritedConversions.AsReadOnlyEquatable();

        IncludedUnitInstanceNames = includedUnitInstanceNames.AsReadOnlyEquatable();

        GenerateDocumentation = generateDocumentation;
    }
}

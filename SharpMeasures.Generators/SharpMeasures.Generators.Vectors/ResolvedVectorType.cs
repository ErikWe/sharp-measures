namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal sealed record class ResolvedVectorType : IResolvedVectorType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public int Dimension { get; }

    public NamedType? Group { get; }
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

    public IReadOnlyList<IDerivedQuantity> Derivations { get; }
    public IReadOnlyList<IVectorConstant> Constants { get; }
    public IReadOnlyList<IConvertibleQuantity> Conversions { get; }

    public IReadOnlyList<IDerivedQuantity> InheritedDerivations { get; }
    public IReadOnlyList<IConvertibleQuantity> InheritedConversions { get; }

    public IReadOnlyList<string> IncludedUnitInstanceNames { get; }

    public bool? GenerateDocumentation { get; }

    IReadOnlyList<IConvertibleQuantity> IResolvedQuantityType.Conversions => Conversions;

    public ResolvedVectorType(DefinedType type, MinimalLocation typeLocation, int dimension, NamedType? group, NamedType unit, NamedType? originalQuantity, ConversionOperatorBehaviour specializationForwardsConversionBehaviour, ConversionOperatorBehaviour specializationBackwardsConversionBehaviour, NamedType? scalar,
        bool implementSum, bool implementDifference, NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol, IReadOnlyList<IDerivedQuantity> derivations, IReadOnlyList<IVectorConstant> constants, IReadOnlyList<IConvertibleQuantity> conversions,
        IReadOnlyList<IDerivedQuantity> inheritedDerivations, IReadOnlyList<IConvertibleQuantity> inheritedConversions, IReadOnlyList<string> includedUnitInstanceNames, bool? generateDocumentation)
    {
        Type = type;
        TypeLocation = typeLocation;

        Dimension = dimension;

        Group = group;
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

        Derivations = derivations.AsReadOnlyEquatable();
        Constants = constants.AsReadOnlyEquatable();
        Conversions = conversions.AsReadOnlyEquatable();

        InheritedDerivations = inheritedDerivations.AsReadOnlyEquatable();
        InheritedConversions = inheritedConversions.AsReadOnlyEquatable();

        IncludedUnitInstanceNames = includedUnitInstanceNames.AsReadOnlyEquatable();

        GenerateDocumentation = generateDocumentation;
    }
}

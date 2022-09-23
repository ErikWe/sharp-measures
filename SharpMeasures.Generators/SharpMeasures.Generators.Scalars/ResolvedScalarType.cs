namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal sealed record class ResolvedScalarType : IResolvedScalarType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public NamedType Unit { get; }
    public bool UseUnitBias { get; }

    public NamedType? OriginalQuantity { get; }

    public ConversionOperatorBehaviour SpecializationForwardsConversionBehaviour { get; }
    public ConversionOperatorBehaviour SpecializationBackwardsConversionBehaviour { get; }

    public NamedType? Vector { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }
    public NamedType? Difference { get; }

    public string? DefaultUnitInstanceName { get; }
    public string? DefaultUnitInstanceSymbol { get; }

    public IReadOnlyList<IDerivedQuantity> Derivations { get; }
    public IReadOnlyList<IScalarConstant> Constants { get; }
    public IReadOnlyList<IConvertibleQuantity> Conversions { get; }

    public IReadOnlyList<IDerivedQuantity> InheritedDerivations { get; }
    public IReadOnlyList<IConvertibleQuantity> InheritedConversions { get; }

    public IReadOnlyList<string> IncludedUnitBaseInstanceNames { get; }
    public IReadOnlyList<string> IncludedUnitInstanceNames { get; }

    public bool? GenerateDocumentation { get; }

    IReadOnlyList<IConvertibleQuantity> IResolvedQuantityType.Conversions => Conversions;

    public ResolvedScalarType(DefinedType type, MinimalLocation typeLocation, NamedType unit, bool useUnitBias, NamedType? originalQuantity, ConversionOperatorBehaviour specializationForwardsConversionBehaviour, ConversionOperatorBehaviour specializationBackwardsConversionBehaviour, NamedType? vector,
        bool implementSum, bool implementDifference, NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol, IReadOnlyList<IDerivedQuantity> derivations, IReadOnlyList<IScalarConstant> constants, IReadOnlyList<IConvertibleQuantity> conversions,
        IReadOnlyList<IDerivedQuantity> inheritedDerivations, IReadOnlyList<IConvertibleQuantity> inheritedConversions, IReadOnlyList<string> includedUnitBaseInstanceNames, IReadOnlyList<string> includedUnitInstanceNames, bool? generateDocumentation)
    {
        Type = type;
        TypeLocation = typeLocation;

        Unit = unit;
        UseUnitBias = useUnitBias;

        OriginalQuantity = originalQuantity;

        SpecializationForwardsConversionBehaviour = specializationForwardsConversionBehaviour;
        SpecializationBackwardsConversionBehaviour = specializationBackwardsConversionBehaviour;

        Vector = vector;

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

        IncludedUnitBaseInstanceNames = includedUnitBaseInstanceNames.AsReadOnlyEquatable();
        IncludedUnitInstanceNames = includedUnitInstanceNames.AsReadOnlyEquatable();

        GenerateDocumentation = generateDocumentation;
    }
}

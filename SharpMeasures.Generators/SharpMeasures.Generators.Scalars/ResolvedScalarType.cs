namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal sealed record class ResolvedScalarType : IResolvedScalarType
{
    public DefinedType Type { get; }

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

    public IReadOnlyList<IQuantityOperation> Operations { get; }
    public IReadOnlyList<IQuantityProcess> Processes { get; }
    public IReadOnlyList<IScalarConstant> Constants { get; }
    public IReadOnlyList<IConvertibleQuantity> Conversions { get; }

    public IReadOnlyList<IQuantityOperation> InheritedOperations { get; }
    public IReadOnlyList<IQuantityProcess> InheritedProcesses { get; }
    public IReadOnlyList<IScalarConstant> InheritedConstants { get; }
    public IReadOnlyList<IConvertibleQuantity> InheritedConversions { get; }

    public IReadOnlyList<string> IncludedUnitBaseInstanceNames { get; }
    public IReadOnlyList<string> IncludedUnitInstanceNames { get; }

    public bool? GenerateDocumentation { get; }

    IReadOnlyList<IConvertibleQuantity> IResolvedQuantityType.Conversions => Conversions;

    public ResolvedScalarType(DefinedType type, NamedType unit, bool useUnitBias, NamedType? originalQuantity, ConversionOperatorBehaviour specializationForwardsConversionBehaviour, ConversionOperatorBehaviour specializationBackwardsConversionBehaviour, NamedType? vector,
        bool implementSum, bool implementDifference, NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol, IReadOnlyList<IQuantityOperation> operations, IReadOnlyList<IQuantityProcess> processes, IReadOnlyList<IScalarConstant> constants, IReadOnlyList<IConvertibleQuantity> conversions,
        IReadOnlyList<IQuantityOperation> inheritedOperations, IReadOnlyList<IQuantityProcess> inheritedProcesses, IReadOnlyList<IScalarConstant> inheritedConstants, IReadOnlyList<IConvertibleQuantity> inheritedConversions, IReadOnlyList<string> includedUnitBaseInstanceNames, IReadOnlyList<string> includedUnitInstanceNames, bool? generateDocumentation)
    {
        Type = type;

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

        Operations = operations.AsReadOnlyEquatable();
        Processes = processes.AsReadOnlyEquatable();
        Constants = constants.AsReadOnlyEquatable();
        Conversions = conversions.AsReadOnlyEquatable();

        InheritedOperations = inheritedOperations.AsReadOnlyEquatable();
        InheritedProcesses = inheritedProcesses.AsReadOnlyEquatable();
        InheritedConstants = inheritedConstants.AsReadOnlyEquatable();
        InheritedConversions = inheritedConversions.AsReadOnlyEquatable();

        IncludedUnitBaseInstanceNames = includedUnitBaseInstanceNames.AsReadOnlyEquatable();
        IncludedUnitInstanceNames = includedUnitInstanceNames.AsReadOnlyEquatable();

        GenerateDocumentation = generateDocumentation;
    }
}

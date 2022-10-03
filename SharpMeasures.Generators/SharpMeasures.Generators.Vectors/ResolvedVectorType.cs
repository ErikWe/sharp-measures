namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal sealed record class ResolvedVectorType : IResolvedVectorType
{
    public DefinedType Type { get; }

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

    public IReadOnlyList<IQuantityOperation> Operations { get; }
    public IReadOnlyList<IVectorOperation> VectorOperations { get; }
    public IReadOnlyList<IQuantityProcess> Processes { get; }
    public IReadOnlyList<IVectorConstant> Constants { get; }
    public IReadOnlyList<IConvertibleQuantity> Conversions { get; }

    public IReadOnlyList<IQuantityOperation> InheritedOperations { get; }
    public IReadOnlyList<IVectorOperation> InheritedVectorOperations { get; }
    public IReadOnlyList<IQuantityProcess> InheritedProcesses { get; }
    public IReadOnlyList<IVectorConstant> InheritedConstants { get; }
    public IReadOnlyList<IConvertibleQuantity> InheritedConversions { get; }

    public IReadOnlyList<string> IncludedUnitInstanceNames { get; }

    public bool? GenerateDocumentation { get; }

    IReadOnlyList<IConvertibleQuantity> IResolvedQuantityType.Conversions => Conversions;

    public ResolvedVectorType(DefinedType type, int dimension, NamedType? group, NamedType unit, NamedType? originalQuantity, ConversionOperatorBehaviour specializationForwardsConversionBehaviour, ConversionOperatorBehaviour specializationBackwardsConversionBehaviour, NamedType? scalar,
        bool implementSum, bool implementDifference, NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol, IReadOnlyList<IQuantityOperation> operations, IReadOnlyList<IVectorOperation> vectorOperations, IReadOnlyList<IQuantityProcess> processes, IReadOnlyList<IVectorConstant> constants,
        IReadOnlyList<IConvertibleQuantity> conversions, IReadOnlyList<IQuantityOperation> inheritedOperations, IReadOnlyList<IVectorOperation> inheritedVectorOperations, IReadOnlyList<IQuantityProcess> inheritedProcesses, IReadOnlyList<IVectorConstant> inheritedConstants, IReadOnlyList<IConvertibleQuantity> inheritedConversions, IReadOnlyList<string> includedUnitInstanceNames, bool? generateDocumentation)
    {
        Type = type;

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

        Operations = operations.AsReadOnlyEquatable();
        VectorOperations = vectorOperations.AsReadOnlyEquatable();
        Processes = processes.AsReadOnlyEquatable();
        Constants = constants.AsReadOnlyEquatable();
        Conversions = conversions.AsReadOnlyEquatable();

        InheritedOperations = inheritedOperations.AsReadOnlyEquatable();
        InheritedVectorOperations = inheritedVectorOperations.AsReadOnlyEquatable();
        InheritedProcesses = inheritedProcesses.AsReadOnlyEquatable();
        InheritedConstants = inheritedConstants.AsReadOnlyEquatable();
        InheritedConversions = inheritedConversions.AsReadOnlyEquatable();

        IncludedUnitInstanceNames = includedUnitInstanceNames.AsReadOnlyEquatable();

        GenerateDocumentation = generateDocumentation;
    }
}

﻿namespace SharpMeasures.Generators.Vectors;

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

    public IReadOnlyList<IQuantityOperation> Operations { get; }
    public IReadOnlyList<IVectorOperation> VectorOperations { get; }
    public IReadOnlyList<IConvertibleQuantity> Conversions { get; }

    public IReadOnlyList<IQuantityOperation> InheritedOperations { get; }
    public IReadOnlyList<IVectorOperation> InheritedVectorOperations { get; }
    public IReadOnlyList<IConvertibleQuantity> InheritedConversions { get; }

    public IReadOnlyList<string> IncludedUnitInstanceNames { get; }

    IReadOnlyList<IConvertibleQuantity> IResolvedQuantityType.Conversions => Conversions;

    public ResolvedGroupType(DefinedType type, NamedType unit, NamedType? originalQuantity, ConversionOperatorBehaviour specializationForwardsConversionBehaviour, ConversionOperatorBehaviour specializationBackwardsConversionBehaviour, NamedType? scalar, bool implementSum,
        bool implementDifference, NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol, IReadOnlyDictionary<int, NamedType> membersByDimension, IReadOnlyList<IQuantityOperation> operations, IReadOnlyList<IVectorOperation> vectorOperations, IReadOnlyList<IConvertibleQuantity> conversions,
        IReadOnlyList<IQuantityOperation> inheritedOperations, IReadOnlyList<IVectorOperation> inheritedVectorOperations, IReadOnlyList<IConvertibleQuantity> inheritedConversions, IReadOnlyList<string> includedUnitInstanceNames)
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

        Operations = operations.AsReadOnlyEquatable();
        VectorOperations = vectorOperations.AsReadOnlyEquatable();
        Conversions = conversions.AsReadOnlyEquatable();

        InheritedOperations = inheritedOperations.AsReadOnlyEquatable();
        InheritedVectorOperations = inheritedVectorOperations.AsReadOnlyEquatable();
        InheritedConversions = inheritedConversions.AsReadOnlyEquatable();

        IncludedUnitInstanceNames = includedUnitInstanceNames.AsReadOnlyEquatable();
    }
}

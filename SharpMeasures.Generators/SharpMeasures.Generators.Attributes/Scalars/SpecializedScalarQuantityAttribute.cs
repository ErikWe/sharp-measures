namespace SharpMeasures.Generators;

using System;

/// <summary>Marks the type as an auto-generated scalar quantity, behaving as a specialized form of another quantity.</summary>
/// <remarks><inheritdoc cref="ScalarQuantityAttribute" path="/remarks"/></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class SpecializedScalarQuantityAttribute : Attribute
{
    /// <summary>The original scalar quantity, of which this quantity is a specialized form.</summary>
    public Type OriginalScalar { get; }

    /// <summary>Dictates whether this quantity inherits the operations defined by the original quantity. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritOperations { get; init; }
    /// <summary>Dictates whether this quantity inherits the processes defined by the original quantity. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritProcesses { get; init; }
    /// <summary>Dictates whether this quantity inherits the constants defined by the original quantity. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritConstants { get; init; }
    /// <summary>Dictates whether this quantity inherits the conversions defined by the original quantity. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritConversions { get; init; }
    /// <summary>Dictates whether this quantity inherits the bases of the original quantity. The default behaviour is <see langword="true"/>.</summary>
    /// <remarks><see cref="IncludeUnitBasesAttribute"/> and <see cref="ExcludeUnitBasesAttribute"/> allow more granular control of what bases to include.</remarks>
    public bool InheritBases { get; init; }
    /// <summary>Dictates whether this quantity inherits the units of the original quantity. The default behaviour is <see langword="true"/>.</summary>
    /// <remarks><see cref="IncludeUnitsAttribute"/> and <see cref="ExcludeUnitsAttribute"/> allow more granular control of what units to include.</remarks>
    public bool InheritUnits { get; init; }

    /// <summary>Determines the behaviour of the operator converting from the original quantity to this quantity. The default behaviour is <see cref="ConversionOperatorBehaviour.Explicit"/>.</summary>
    public ConversionOperatorBehaviour ForwardsCastOperatorBehaviour { get; init; }
    /// <summary>Determines the behaviour of the operator converting from this quantity to the original quantity. The default behaviour is <see cref="ConversionOperatorBehaviour.Implicit"/>.</summary>
    public ConversionOperatorBehaviour BackwardsCastOperatorBehaviour { get; init; }

    /// <summary><inheritdoc cref="ScalarQuantityAttribute.Vector" path="/summary"/> By default, the value is inherited from the original quantity.</summary>
    /// <remarks><inheritdoc cref="ScalarQuantityAttribute.Vector" path="/remarks"/></remarks>
    public Type? Vector { get; init; }

    /// <summary>Dictates whether this quantity should support addition of two instances. By default, the behaviour is inherited from the original quantity.</summary>
    public bool ImplementSum { get; init; }

    /// <summary>Dictates whether this quantity should support subtraction of two instances. By default, the behaviour is inherited from the original quantity.</summary>
    /// <remarks>To specify the quantity that represents the difference, use <see cref="Difference"/>.</remarks>
    public bool ImplementDifference { get; init; }

    /// <summary>Determines the quantity that is considered the difference between two instances of this quantity. By default, the value is inherited from the original quantity.</summary>
    /// <remarks>To disable support for computing the difference, use <see cref="ImplementDifference"/>.</remarks>
    public Type? Difference { get; init; }

    /// <summary><inheritdoc cref="ScalarQuantityAttribute.DefaultUnit" path="/summary"/> By default, the value is inherited from the original quantity.</summary>
    public string? DefaultUnit { get; init; }

    /// <summary><inheritdoc cref="ScalarQuantityAttribute.DefaultSymbol" path="/summary"/> By default, the value is inherited from the original quantity.</summary>
    public string? DefaultSymbol { get; init; }

    /// <inheritdoc cref="SpecializedScalarQuantityAttribute"/>
    /// <param name="originalScalar"><inheritdoc cref="OriginalScalar" path="/summary"/><para><inheritdoc cref="OriginalScalar" path="/remarks"/></para></param>
    public SpecializedScalarQuantityAttribute(Type originalScalar)
    {
        OriginalScalar = originalScalar;
    }
}

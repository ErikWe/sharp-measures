namespace SharpMeasures.Generators;

using System;

/// <summary>Marks the type as the auto-generated root of a group of vectors that represent the same quantity, but of varying dimension - behaving as a specialized form of another such group.</summary>
/// <remarks><inheritdoc cref="VectorGroupAttribute" path="/remarks"/></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class SpecializedVectorGroupAttribute : Attribute
{
    /// <summary>The original vector group, of which this group is a specialized form.</summary>
    public Type OriginalVectorGroup { get; }

    /// <summary>Dictates whether this group inherits the operations defined by the original group. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritOperations { get; init; }
    /// <summary>Dictates whether this group inherits the conversions defined by the original group. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritConversions { get; init; }
    /// <summary>Dictates whether this group inherits the units of the original group. The default behaviour is <see langword="true"/>.</summary>
    /// <remarks><see cref="IncludeUnitsAttribute"/> and <see cref="ExcludeUnitsAttribute"/> allow more granular control of what units to include.</remarks>
    public bool InheritUnits { get; init; }

    /// <summary>Determines the behaviour of the operator converting from the original group to this group. The default behaviour is <see cref="ConversionOperatorBehaviour.Explicit"/>.</summary>
    public ConversionOperatorBehaviour ForwardsCastOperatorBehaviour { get; init; }
    /// <summary>Determines the behaviour of the operator converting from this group to the original group. The default behaviour is <see cref="ConversionOperatorBehaviour.Implicit"/>.</summary>
    public ConversionOperatorBehaviour BackwardsCastOperatorBehaviour { get; init; }

    /// <summary><inheritdoc cref="VectorQuantityAttribute.Scalar" path="/summary"/> By default, the value is inherited from the original quantity.</summary>
    /// <remarks>For example; <i>Speed</i> could be considered the scalar associated with <i>Velocity</i>.</remarks>
    public Type? Scalar { get; init; }

    /// <inheritdoc cref="SpecializedVectorQuantityAttribute.ImplementSum"/>
    public bool ImplementSum { get; init; }

    /// <summary><inheritdoc cref="SpecializedVectorQuantityAttribute.ImplementSum" path="/summary"/></summary>
    /// <remarks>To specify the quantity that represents the difference, use <see cref="Difference"/>.</remarks>
    public bool ImplementDifference { get; init; }

    /// <summary><inheritdoc cref="SpecializedVectorQuantityAttribute.Difference" path="/summary"/></summary>
    /// <remarks>To disable support for computing the difference, use <see cref="ImplementDifference"/>.</remarks>
    public Type? Difference { get; init; }

    /// <inheritdoc cref="SpecializedScalarQuantityAttribute.DefaultUnit"/>
    public string? DefaultUnit { get; init; }

    /// <inheritdoc cref="SpecializedScalarQuantityAttribute.DefaultSymbol"/>
    public string? DefaultSymbol { get; init; }

    /// <inheritdoc cref="VectorGroupAttribute"/>
    /// <param name="originalVectorGroup"><inheritdoc cref="OriginalVectorGroup" path="/summary"/><para><inheritdoc cref="OriginalVectorGroup" path="/remarks"/></para></param>
    public SpecializedVectorGroupAttribute(Type originalVectorGroup)
    {
        OriginalVectorGroup = originalVectorGroup;
    }
}

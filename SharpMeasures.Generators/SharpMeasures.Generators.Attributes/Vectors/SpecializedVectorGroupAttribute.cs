﻿namespace SharpMeasures.Generators;

using System;

/// <summary>Marks the type as a specialized form of another vector group root.
/// For example, <i>GravitationalAcceleration</i> could be defined as a specialized form of <i>Acceleration</i>.</summary>
/// <remarks>The following attributes may be used to modify how the vectors in the group are generated:
/// <list type="bullet">
/// <item>
/// <term><see cref="QuantityOperationAttribute"/></term>
/// <description>Describes how the vectors of the group may be derived from other quantities.</description>
/// </item>
/// <item>
/// <term><see cref="IncludeUnitsAttribute"/></term>
/// <description>Dictates the units for which a property representing the magnitude is implemented.</description>
/// </item>
/// <item>
/// <term><see cref="ExcludeUnitsAttribute"/></term>
/// <description>Dictates the units for which a property representing the magnitude is <i>not</i> implemented.</description>
/// </item>
/// <item>
/// <term><see cref="ConvertibleQuantityAttribute"/></term>
/// <description>Lists other vectors that the vectors in the group may be converted to.</description>
/// </item>
/// </list></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class SpecializedVectorGroupAttribute : Attribute
{
    /// <summary>The original vector group, of which this is a specialized form.</summary>
    public Type OriginalVectorGroup { get; }

    /// <inheritdoc cref="SpecializedVectorQuantityAttribute.InheritOperations"/>
    public bool InheritOperations { get; init; }
    /// <summary>Dictates whether the vectors of this group inherits the constants defined by the vectors of the original group. The default behaviour is
    /// <see langword="true"/>.</summary>
    public bool InheritConstants { get; init; }
    /// <inheritdoc cref="SpecializedVectorQuantityAttribute.InheritConversions"/>
    public bool InheritConversions { get; init; }
    /// <inheritdoc cref="SpecializedVectorQuantityAttribute.InheritUnits"/>
    public bool InheritUnits { get; init; }

    /// <summary>Determines the behaviour of the operator converting from the original quantity to this quantity. The default behaviour is <see cref="ConversionOperatorBehaviour.Explicit"/>.</summary>
    public ConversionOperatorBehaviour ForwardsCastOperatorBehaviour { get; init; }
    /// <summary>Determines the behaviour of the operator converting from this quantity to the original quantity. The default behaviour is <see cref="ConversionOperatorBehaviour.Implicit"/>.</summary>
    public ConversionOperatorBehaviour BackwardsCastOperatorBehaviour { get; init; }

    /// <summary><inheritdoc cref="VectorGroupAttribute.Scalar" path="/summary"/> By default, the value is inherited from the original group.</summary>
    /// <remarks><inheritdoc cref="VectorGroupAttribute.Scalar" path="/remarks"/></remarks>
    public Type? Scalar { get; init; }

    /// <summary>Dictates whether the vectors of this group should implement support for computing the sum of two instances of that vector. By default,
    /// the behaviour is inherited from the original group.</summary>
    public bool ImplementSum { get; init; }

    /// <summary>Dictates whether the vectors of this group should implement support for computing the difference between two instances of that vector. By default,
    /// the behaviour is inherited from the original group.</summary>
    /// <remarks>To specify the vector group that represents the differences, use <see cref="Difference"/>.</remarks>
    public bool ImplementDifference { get; init; }

    /// <summary>The vector group that consists of vectors that are considered the differences between two instances of the vectors of this group. By default, the
    /// value is inherited from the original group.</summary>
    /// <remarks>To disable support for computing the differences in the first place, use <see cref="ImplementDifference"/>.</remarks>
    public Type? Difference { get; init; }

    /// <summary><inheritdoc cref="VectorGroupAttribute.DefaultUnit" path="/summary"/> By default, the value is inherited from the original group.</summary>
    /// <remarks><inheritdoc cref="VectorGroupAttribute.DefaultUnit" path="/remarks"/></remarks>
    public string? DefaultUnit { get; init; }

    /// <summary><inheritdoc cref="VectorGroupAttribute.DefaultSymbol" path="/summary"/> By default, the value is inherited from the original group.</summary>
    /// <remarks><inheritdoc cref="VectorGroupAttribute.DefaultSymbol" path="/remarks"/></remarks>
    public string? DefaultSymbol { get; init; }

    /// <inheritdoc cref="VectorGroupAttribute"/>
    /// <param name="originalVectorGroup"><inheritdoc cref="OriginalVectorGroup" path="/summary"/><para><inheritdoc cref="OriginalVectorGroup" path="/remarks"/></para></param>
    public SpecializedVectorGroupAttribute(Type originalVectorGroup)
    {
        OriginalVectorGroup = originalVectorGroup;
    }
}

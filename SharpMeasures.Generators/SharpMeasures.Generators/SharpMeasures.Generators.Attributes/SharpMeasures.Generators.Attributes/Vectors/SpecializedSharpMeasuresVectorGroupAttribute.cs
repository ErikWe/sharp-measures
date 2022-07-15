﻿namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

using System;

/// <summary>Marks the type as the root of a specialized form of another vector group. The members of the group are registered
/// using <see cref="RegisterVectorGroupMemberAttribute"/>.</summary>
/// <remarks>The following accompanying attributes may be used to enhance the vector group:
/// <list type="bullet">
/// <item>
/// <term><see cref="DerivedQuantityAttribute"/></term>
/// <description>Describes how the quantities of the vector group may be derived from other quantities.</description>
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
/// <description>Lists vector groups that this vector group may be converted to.</description>
/// </item>
/// </list></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class SpecializedSharpMeasuresVectorGroupAttribute : Attribute
{
    /// <summary>The original vector group, of which this is a specialized form.</summary>
    public Type OriginalVectorGroup { get; }

    /// <summary>Dictates whether this vector group inherits the derivations defined by the original group. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritDerivations { get; init; }
    /// <summary>Dictates whether the vectors of this group inherits the constants defined by the vectors of the original group. The default behaviour is
    /// <see langword="true"/>.</summary>
    public bool InheritConstants { get; init; }
    /// <summary>Dictates whether this vector group inherits the units of the original group. The default behaviour is <see langword="true"/>.</summary>
    /// <remarks>The attributes <see cref="IncludeUnitsAttribute"/> and <see cref="ExcludeUnitsAttribute"/> enable more granular control of what units are
    /// inherited.</remarks>
    public bool InheritUnits { get; init; }

    /// <summary><inheritdoc cref="SharpMeasuresVectorGroupAttribute.Scalar" path="/summary"/> By default, the value is inherited from the original group.</summary>
    /// <remarks><inheritdoc cref="SharpMeasuresVectorGroupAttribute.Scalar" path="/remarks"/></remarks>
    public Type? Scalar { get; init; }

    /// <summary>Dictates whether the vectors of this group should implement support for computing the sum of two instances of that vector. By default,
    /// the value is inherited from the original group.</summary>
    public bool ImplementSum { get; init; }

    /// <summary>Dictates whether the vectors of this group should implement support for computing the difference between two instances of that vector. By default,
    /// the value is inherited from the original group.</summary>
    /// <remarks>To specify the vector group that represents the differences, use <see cref="Difference"/>.</remarks>
    public bool ImplementDifference { get; init; }

    /// <summary>The vector group that consists of vectors that are considered the differences between two instances of the vectors of this group. By default, the
    /// value is inherited from the original group.</summary>
    /// <remarks>To disable support for computing the differences in the first place, use <see cref="ImplementDifference"/>.</remarks>
    public Type? Difference { get; init; }

    /// <summary><inheritdoc cref="SharpMeasuresVectorGroupAttribute.DefaultUnitName" path="/summary"/> By default, the value is inherited from the original group.</summary>
    /// <remarks><inheritdoc cref="SharpMeasuresVectorGroupAttribute.DefaultUnitName" path="/remarks"/></remarks>
    public string? DefaultUnitName { get; init; }

    /// <summary><inheritdoc cref="SharpMeasuresVectorGroupAttribute.DefaultUnitSymbol" path="/summary"/> By default, the value is inherited from the original group.</summary>
    /// <remarks><inheritdoc cref="SharpMeasuresVectorGroupAttribute.DefaultUnitSymbol" path="/remarks"/></remarks>
    public string? DefaultUnitSymbol { get; init; }

    /// <summary><inheritdoc cref="SharpMeasuresVectorGroupAttribute.GenerateDocumentation" path="/summary"/> By default, the value is inherited from the original group.</summary>
    public bool GenerateDocumentation { get; init; }

    /// <inheritdoc cref="SharpMeasuresVectorGroupAttribute"/>
    /// <param name="originalVectorGroup"><inheritdoc cref="OriginalVectorGroup" path="/summary"/><para><inheritdoc cref="OriginalVectorGroup" path="/remarks"/></para></param>
    public SpecializedSharpMeasuresVectorGroupAttribute(Type originalVectorGroup)
    {
        OriginalVectorGroup = originalVectorGroup;
    }
}
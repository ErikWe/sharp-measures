namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Marks the type as a auto-generated SharpMeasures vector quantity, and a member of a group of vectors representing the same quantity.</summary>
/// <typeparam name="TGroup">The vector group that the member belongs to.</typeparam>
/// <remarks>The following attributes may be used to modify the quantity:
/// <list type="bullet">
/// <item>
/// <term><see cref="QuantityOperationAttribute{TResult, TOther}"/></term>
/// <description>Describes the operations { + , - , ⋅ , ÷, ⨯ } supported by the quantity.</description>
/// </item>
/// <item>
/// <term><see cref="QuantityProcessAttribute{TResult}"/></term>
/// <description>Describes a custom process implemented by the quantity.</description>
/// </item>
/// <item>
/// <term><see cref="QuantityPropertyAttribute{TResult}"/></term>
/// <description>Describes a custom readonly property implemented by the quantity.</description>
/// </item>
/// <item>
/// <term><see cref="QuantitySumAttribute{TSum}"/></term>
/// <description>Describes the result of addition of two instances of the quantity.</description>
/// </item>
/// <item>
/// <term><see cref="QuantityDifferenceAttribute{TDifference}"/></term>
/// <description>Describes the result of subtraction of two instances of the quantity.</description>
/// </item>
/// <item>
/// <term><see cref="ConvertibleQuantityAttribute"/></term>
/// <description>Lists other quantities that the quantity supports conversion to and/or from.</description>
/// </item>
/// <item>
/// <term><see cref="VectorConstantAttribute"/></term>
/// <description>Defines a constant value of the quantity.</description>
/// </item>
/// <item>
/// <term><see cref="IncludeUnitsAttribute"/> / <see cref="ExcludeUnitsAttribute"/></term>
/// <description>Dictates the set of unit instances for which a property representing the magnitudes of the components is implemented.</description>
/// </item>
/// <item>
/// <term><see cref="DefaultUnitAttribute"/></term>
/// <description>Dictates the default unit of the quantity - used when formatting the quantity.</description>
/// </item>
/// </list></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class VectorGroupMemberAttribute<TGroup> : Attribute
{
    /// <inheritdoc cref="VectorQuantityAttribute{TUnit}.Dimension"/>
    public int Dimension { get; init; }

    /// <summary>Dictates whether this quantity inherits the operations defined by the vector group. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritOperationsFromGroup { get; init; }

    /// <summary>If the associated vector group is a specialized form of another vector group, this property dictates whether this member inherits the operations defined by the corresponding member of the
    /// original vector group. By default, the behaviour mimics that of <see cref="InheritOperationsFromGroup"/>.</summary>
    public bool InheritOperationsFromMembers { get; init; }

    /// <summary>If the associated vector group is a specialized form of another vector group, this property dictates whether this member inherits the processes defined by the corresponding member of the
    /// original vector group. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritProcessesFromMembers { get; init; }

    /// <summary>If the associated vector group is a specialized form of another vector group, this property dictates whether this member inherits the properties defined by the corresponding member of the
    /// original vector group. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritPropertiesFromMembers { get; init; }

    /// <summary>If the associated vector group is a specialized form of another vector group, this property dictates whether this member inherits the constants defined by the corresponding member of the
    /// original vector group. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritConstantsFromMembers { get; init; }

    /// <summary>Dictates whether this quantity inherits the conversions defined by the vector group. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritConversionsFromGroup { get; init; }

    /// <summary>If the associated vector group is a specialized form of another vector group, this property dictates whether this member inherits the conversions defined by the corresponding member of the
    /// original vector group. By default, the behaviour mimics that of <see cref="InheritConversionsFromGroup"/>.</summary>
    public bool InheritConversionsFromMembers { get; init; }

    /// <inheritdoc cref="VectorGroupMemberAttribute{TGroup}"/>
    public VectorGroupMemberAttribute() { }
}

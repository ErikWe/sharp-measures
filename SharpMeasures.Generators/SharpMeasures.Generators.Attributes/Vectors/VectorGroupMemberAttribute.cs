namespace SharpMeasures.Generators;

using System;

/// <summary>Marks the type as a vector quantity, and a member of a group of vectors.</summary>
/// <remarks>The following attributes may be used to modify how the vector is generated:
/// <list type="bullet">
/// <item>
/// <term><see cref="QuantityOperationAttribute"/></term>
/// <description>Describes how the vector may be derived from other quantities.</description>
/// </item>
/// <item>
/// <term><see cref="VectorConstantAttribute"/></term>
/// <description>Defines a constant of the vector.</description>
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
/// <description>Lists other vectors that this vector may be converted to.</description>
/// </item>
/// </list></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class VectorGroupMemberAttribute : Attribute
{
    /// <summary>The vector group that this quantity belongs to.</summary>
    public Type VectorGroup { get; }

    /// <summary>Dictates whether this quantity inherits the operations defined by the vector group. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritOperations { get; init; }
    /// <summary>Dictates whether this quantity inherits the conversions defined by the vector group. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritConversions { get; init; }
    /// <summary>Dictates whether this quantity inherits the units of the vector group. The default behaviour is <see langword="true"/>.</summary>
    /// <remarks>The attributes <see cref="IncludeUnitsAttribute"/> and <see cref="ExcludeUnitsAttribute"/> enable more granular control of what units are included.</remarks>
    public bool InheritUnits { get; init; }

    /// <summary>If the vector group is a specialized form of another vector group, this property dictates whether this member inherits the operations defined by the corresponding member of the
    /// original vector group. If the vector group is not a specialized form of another vector group, this property has no effect. By default the behaviour mimics that of <see cref="InheritOperations"/>,
    /// which in turn is <see langword="true"/> by default.</summary>
    public bool InheritOperationsFromMembers { get; init; }
    /// <summary>If the vector group is a specialized form of another vector group, this property dictates whether this member inherits the processes defined by the corresponding member of the
    /// original vector group. If the vector group is not a specialized form of another vector group, this property has no effect. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritProcessesFromMembers { get; init; }
    /// <summary>If the vector group is a specialized form of another vector group, this property dictates whether this member inherits the constants defined by the corresponding member of the
    /// original vector group. If the vector group is not a specialized form of another vector group, this property has no effect. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritConstantsFromMembers { get; init; }
    /// <summary>If the vector group is a specialized form of another vector group, this property dictates whether this member inherits the conversions defined by the corresponding member of the
    /// original vector group. If the vector group is not a specialized form of another vector group, this property has no effect. By default the behaviour mimics that of <see cref="InheritConversions"/>,
    /// which in turn is <see langword="true"/> by default.</summary>
    public bool InheritConversionsFromMembers { get; init; }
    /// <summary>If the vector group is a specialized form of another vector group, this property dictates whether this member inherits the units of the corresponding member of the
    /// original vector group. If the vector group is not a specialized form of another vector group, this property has no effect. By default the behaviour mimics that of <see cref="InheritUnits"/>,
    /// which in turn is <see langword="true"/> by default.</summary>
    /// <remarks>The attributes <see cref="IncludeUnitsAttribute"/> and <see cref="ExcludeUnitsAttribute"/> enable more granular control of what units are included.</remarks>
    public bool InheritUnitsFromMembers { get; init; }

    /// <inheritdoc cref="VectorQuantityAttribute.Dimension"/>
    public int Dimension { get; init; }

    /// <summary><inheritdoc cref="VectorQuantityAttribute.GenerateDocumentation" path="/summary"/> By default, the behaviour is inherited from the vector group.</summary>
    public bool GenerateDocumentation { get; init; }

    /// <inheritdoc cref="VectorGroupMemberAttribute"/>
    /// <param name="vectorGroup"><inheritdoc cref="VectorGroup" path="/summary"/><para><inheritdoc cref="VectorGroup" path="/remarks"/></para></param>
    public VectorGroupMemberAttribute(Type vectorGroup)
    {
        VectorGroup = vectorGroup;
    }
}

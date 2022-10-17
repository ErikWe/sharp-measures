namespace SharpMeasures.Generators;

using System;

/// <summary>Marks the type as a auto-generated vector quantity, and a member of a group of vectors representing the same quantity.</summary>
/// <remarks><inheritdoc cref="VectorQuantityAttribute" path="/remarks"/></remarks>
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
    /// <remarks><inheritdoc cref="SpecializedVectorQuantityAttribute.InheritUnits" path="/remarks"/></remarks>
    public bool InheritUnits { get; init; }

    /// <summary>If the vector group is a specialized form of another vector group, this property dictates whether this member inherits the operations defined by the corresponding member of the
    /// original vector group. By default, the behaviour mimics that of <see cref="InheritOperations"/> - which in turn is <see langword="true"/> by default.</summary>
    public bool InheritOperationsFromMembers { get; init; }
    /// <summary>If the vector group is a specialized form of another vector group, this property dictates whether this member inherits the processes defined by the corresponding member of the
    /// original vector group. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritProcessesFromMembers { get; init; }
    /// <summary>If the vector group is a specialized form of another vector group, this property dictates whether this member inherits the constants defined by the corresponding member of the
    /// original vector group. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritConstantsFromMembers { get; init; }
    /// <summary>If the vector group is a specialized form of another vector group, this property dictates whether this member inherits the conversions defined by the corresponding member of the
    /// original vector group. By default, the behaviour mimics that of <see cref="InheritConversions"/> - which in turn is <see langword="true"/> by default.</summary>
    public bool InheritConversionsFromMembers { get; init; }
    /// <summary>If the vector group is a specialized form of another vector group, this property dictates whether this member inherits the units of the corresponding member of the
    /// original vector group. By default, the behaviour mimics that of <see cref="InheritUnits"/> - which in turn is <see langword="true"/> by default.</summary>
    /// <remarks><inheritdoc cref="SpecializedVectorQuantityAttribute.InheritUnits" path="/remarks"/></remarks>
    public bool InheritUnitsFromMembers { get; init; }

    /// <inheritdoc cref="VectorQuantityAttribute.Dimension"/>
    public int Dimension { get; init; }

    /// <inheritdoc cref="VectorGroupMemberAttribute"/>
    /// <param name="vectorGroup"><inheritdoc cref="VectorGroup" path="/summary"/><para><inheritdoc cref="VectorGroup" path="/remarks"/></para></param>
    public VectorGroupMemberAttribute(Type vectorGroup)
    {
        VectorGroup = vectorGroup;
    }
}

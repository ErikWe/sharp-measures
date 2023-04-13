namespace SharpMeasures.Generators.Parsing.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="VectorGroupMemberAttribute{TGroup}"/>.</summary>
public interface IRawVectorGroupMember
{
    /// <summary>The group that the member belongs to.</summary>
    public abstract ITypeSymbol Group { get; }

    /// <summary>The dimension of the quantity.</summary>
    public abstract int? Dimension { get; }

    /// <summary>Dictates whether the quantity should inherit the operations defined by the vector group.</summary>
    public abstract bool? InheritOperationsFromGroup { get; }

    /// <summary>Dictates whether the quantity should inherit the operations defined by the members of the original vector group, if one exists.</summary>
    public abstract bool? InheritOperationsFromMembers { get; }

    /// <summary>Dictates whether the quantity should inherit the processes defined by the members of the original vector group, if one exists.</summary>
    public abstract bool? InheritProcessesFromMembers { get; }

    /// <summary>Dictates whether the quantity should inherit the properties defined by the members of the original vector group, if one exists.</summary>
    public abstract bool? InheritPropertiesFromMembers { get; }

    /// <summary>Dictates whether the quantity should inherit the constants defined by the members of the original vector group, if one exists.</summary>
    public abstract bool? InheritConstantsFromMembers { get; }

    /// <summary>Dictates whether the quantity should inherit the conversions defined by the vector group.</summary>
    public abstract bool? InheritConversionsFromGroup { get; }

    /// <summary>Dictates whether the quantity should inherit the conversions defined by the members of the original vector group, if one exists.</summary>
    public abstract bool? InheritConversionsFromMembers { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="VectorGroupMemberAttribute{TGroup}"/>.</summary>
    public abstract IVectorGroupMemberSyntax? Syntax { get; }
}

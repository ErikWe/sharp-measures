namespace SharpMeasures.Generators.Parsing.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="VectorGroupMemberAttribute{TGroup}"/>.</summary>
public interface IVectorGroupMemberSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the group that the member belongs to.</summary>
    public abstract Location Group { get; }

    /// <summary>The <see cref="Location"/> of the argument for the dimension of the quantity. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location Dimension { get; }

    /// <summary>The <see cref="Location"/> of the argument for whether the quantity should inherit the operations defined by the vector group. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location InheritOperationsFromGroup { get; }

    /// <summary>The <see cref="Location"/> of the argument for whether the quantity should inherit the operations defined by the members of the original vector group, if one exists. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location InheritOperationsFromMembers { get; }

    /// <summary>The <see cref="Location"/> of the argument for whether the quantity should inherit the processes defined by the members of the original vector group, if one exists. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location InheritProcessesFromMembers { get; }

    /// <summary>The <see cref="Location"/> of the argument for whether the quantity should inherit the properties defined by the members of the original vector group, if one exists. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location InheritPropertiesFromMembers { get; }

    /// <summary>The <see cref="Location"/> of the argument for whether the quantity should inherit the constants defined by the members of the original vector group, if one exists. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location InheritConstantsFromMembers { get; }

    /// <summary>The <see cref="Location"/> of the argument for whether the quantity should inherit the conversions defined by the vector group. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location InheritConversionsFromGroup { get; }

    /// <summary>The <see cref="Location"/> of the argument for whether the quantity should inherit the conversions defined by the members of the original vector group, if one exists. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location InheritConversionsFromMembers { get; }
}

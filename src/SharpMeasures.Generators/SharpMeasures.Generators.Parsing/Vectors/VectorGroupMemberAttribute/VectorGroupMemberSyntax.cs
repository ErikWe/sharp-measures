namespace SharpMeasures.Generators.Parsing.Vectors.VectorGroupMemberAttribute;

using Microsoft.CodeAnalysis;

/// <inheritdoc cref="IVectorGroupMemberSyntax"/>
internal sealed record class VectorGroupMemberSyntax : IVectorGroupMemberSyntax
{
    private Location Group { get; }
    private Location Dimension { get; }
    private Location InheritOperationsFromGroup { get; }
    private Location InheritOperationsFromMembers { get; }
    private Location InheritProcessesFromMembers { get; }
    private Location InheritPropertiesFromMembers { get; }
    private Location InheritConstantsFromMembers { get; }
    private Location InheritConversionsFromGroup { get; }
    private Location InheritConversionsFromMembers { get; }

    /// <summary>Instantiates a <see cref="VectorGroupMemberSyntax"/>, representing syntactical information about a parsed <see cref="VectorGroupMemberAttribute{TGroup}"/>.</summary>
    /// <param name="group"><inheritdoc cref="IVectorGroupMemberSyntax.Group" path="/summary"/></param>
    /// <param name="dimension"><inheritdoc cref="IVectorGroupMemberSyntax.Dimension" path="/summary"/></param>
    /// <param name="inheritOperationsFromGroup"><inheritdoc cref="IVectorGroupMemberSyntax.InheritOperationsFromGroup" path="/summary"/></param>
    /// <param name="inheritOperationsFromMembers"><inheritdoc cref="IVectorGroupMemberSyntax.InheritOperationsFromMembers" path="/summary"/></param>
    /// <param name="inheritProcessesFromMembers"><inheritdoc cref="IVectorGroupMemberSyntax.InheritProcessesFromMembers" path="/summary"/></param>
    /// <param name="inheritPropertiesFromMembers"><inheritdoc cref="IVectorGroupMemberSyntax.InheritPropertiesFromMembers" path="/summary"/></param>
    /// <param name="inheritConstantsFromMembers"><inheritdoc cref="IVectorGroupMemberSyntax.InheritConstantsFromMembers" path="/summary"/></param>
    /// <param name="inheritConversionsFromGroup"><inheritdoc cref="IVectorGroupMemberSyntax.InheritConversionsFromGroup" path="/summary"/></param>
    /// <param name="inheritConversionsFromMembers"><inheritdoc cref="IVectorGroupMemberSyntax.InheritConversionsFromMembers" path="/summary"/></param>
    public VectorGroupMemberSyntax(Location group, Location dimension, Location inheritOperationsFromGroup, Location inheritOperationsFromMembers, Location inheritProcessesFromMembers, Location inheritPropertiesFromMembers, Location inheritConstantsFromMembers,
        Location inheritConversionsFromGroup, Location inheritConversionsFromMembers)
    {
        Group = group;

        Dimension = dimension;

        InheritOperationsFromGroup = inheritOperationsFromGroup;
        InheritOperationsFromMembers = inheritOperationsFromMembers;
        InheritProcessesFromMembers = inheritProcessesFromMembers;
        InheritPropertiesFromMembers = inheritPropertiesFromMembers;
        InheritConstantsFromMembers = inheritConstantsFromMembers;
        InheritConversionsFromGroup = inheritConversionsFromGroup;
        InheritConversionsFromMembers = inheritConversionsFromMembers;
    }

    Location IVectorGroupMemberSyntax.Group => Group;
    Location IVectorGroupMemberSyntax.Dimension => Dimension;
    Location IVectorGroupMemberSyntax.InheritOperationsFromGroup => InheritOperationsFromGroup;
    Location IVectorGroupMemberSyntax.InheritOperationsFromMembers => InheritOperationsFromMembers;
    Location IVectorGroupMemberSyntax.InheritProcessesFromMembers => InheritProcessesFromMembers;
    Location IVectorGroupMemberSyntax.InheritPropertiesFromMembers => InheritPropertiesFromMembers;
    Location IVectorGroupMemberSyntax.InheritConstantsFromMembers => InheritConstantsFromMembers;
    Location IVectorGroupMemberSyntax.InheritConversionsFromGroup => InheritConversionsFromGroup;
    Location IVectorGroupMemberSyntax.InheritConversionsFromMembers => InheritConversionsFromMembers;
}

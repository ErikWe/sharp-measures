namespace SharpMeasures.Generators.Parsing.Vectors.VectorGroupMemberAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

/// <inheritdoc cref="IRawVectorGroupMember"/>
internal sealed record class RawVectorGroupMember : IRawVectorGroupMember
{
    private ITypeSymbol Group { get; }

    private int? Dimension { get; }

    private bool? InheritOperationsFromGroup { get; }
    private bool? InheritOperationsFromMembers { get; }
    private bool? InheritProcessesFromMembers { get; }
    private bool? InheritPropertiesFromMembers { get; }
    private bool? InheritConstantsFromMembers { get; }
    private bool? InheritConversionsFromGroup { get; }
    private bool? InheritConversionsFromMembers { get; }

    private IVectorGroupMemberSyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawVectorGroupMember"/>, representing a parsed <see cref="VectorGroupMemberAttribute{TGroup}"/>.</summary>
    /// <param name="group"><inheritdoc cref="IRawVectorGroupMember.Group" path="/summary"/></param>
    /// <param name="dimension"><inheritdoc cref="IRawVectorGroupMember.Dimension" path="/summary"/></param>
    /// <param name="inheritOperationsFromGroup"><inheritdoc cref="IRawVectorGroupMember.InheritOperationsFromGroup" path="/summary"/></param>
    /// <param name="inheritOperationsFromMembers"><inheritdoc cref="IRawVectorGroupMember.InheritOperationsFromMembers" path="/summary"/></param>
    /// <param name="inheritProcessesFromMembers"><inheritdoc cref="IRawVectorGroupMember.InheritProcessesFromMembers" path="/summary"/></param>
    /// <param name="inheritPropertiesFromMembers"><inheritdoc cref="IRawVectorGroupMember.InheritPropertiesFromMembers" path="/summary"/></param>
    /// <param name="inheritConstantsFromMembers"><inheritdoc cref="IRawVectorGroupMember.InheritConstantsFromMembers" path="/summary"/></param>
    /// <param name="inheritConversionsFromGroup"><inheritdoc cref="IRawVectorGroupMember.InheritConversionsFromGroup" path="/summary"/></param>
    /// <param name="inheritConversionsFromMembers"><inheritdoc cref="IRawVectorGroupMember.InheritConversionsFromMembers" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawVectorGroupMember.Syntax" path="/summary"/></param>
    public RawVectorGroupMember(ITypeSymbol group, int? dimension, bool? inheritOperationsFromGroup, bool? inheritOperationsFromMembers, bool? inheritProcessesFromMembers, bool? inheritPropertiesFromMembers,
        bool? inheritConstantsFromMembers, bool? inheritConversionsFromGroup, bool? inheritConversionsFromMembers, IVectorGroupMemberSyntax? syntax)
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

        Syntax = syntax;
    }

    ITypeSymbol IRawVectorGroupMember.Group => Group;

    int? IRawVectorGroupMember.Dimension => Dimension;

    bool? IRawVectorGroupMember.InheritOperationsFromGroup => InheritOperationsFromGroup;
    bool? IRawVectorGroupMember.InheritOperationsFromMembers => InheritOperationsFromMembers;
    bool? IRawVectorGroupMember.InheritProcessesFromMembers => InheritProcessesFromMembers;
    bool? IRawVectorGroupMember.InheritPropertiesFromMembers => InheritPropertiesFromMembers;
    bool? IRawVectorGroupMember.InheritConstantsFromMembers => InheritConstantsFromMembers;
    bool? IRawVectorGroupMember.InheritConversionsFromGroup => InheritConversionsFromGroup;
    bool? IRawVectorGroupMember.InheritConversionsFromMembers => InheritConversionsFromMembers;

    IVectorGroupMemberSyntax? IRawVectorGroupMember.Syntax => Syntax;
}

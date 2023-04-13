namespace SharpMeasures.Generators.Parsing.Vectors.VectorGroupMemberAttribute;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="VectorGroupMemberAttribute{TGroup}"/> to be parsed.</summary>
public sealed class VectorGroupMemberAttributeParser : IConstructiveSyntacticAttributeParser<IRawVectorGroupMember>, IConstructiveSemanticAttributeParser<IRawVectorGroupMember>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="VectorGroupMemberAttributeParser"/>, parsing the arguments of a <see cref="VectorGroupMemberAttribute{TGroup}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public VectorGroupMemberAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawVectorGroupMember? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        VectorGroupMemberAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawVectorGroupMember? TryParse(AttributeData attributeData)
    {
        VectorGroupMemberAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawVectorGroupMember? Create(VectorGroupMemberAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (recorder.Group is null)
        {
            return null;
        }

        return new RawVectorGroupMember(recorder.Group, recorder.Dimension, recorder.InheritOperationsFromGroup, recorder.InheritOperationsFromMembers, recorder.InheritProcessesFromMembers,
            recorder.InheritPropertiesFromMembers, recorder.InheritConstantsFromMembers, recorder.InheritConversionsFromGroup, recorder.InheritConversionsFromMembers, CreateSyntax(recorder, parsingMode));
    }

    private static IVectorGroupMemberSyntax? CreateSyntax(VectorGroupMemberAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new VectorGroupMemberSyntax(recorder.GroupLocation, recorder.DimensionLocation, recorder.InheritOperationsFromGroupLocation, recorder.InheritOperationsFromMembersLocation, recorder.InheritProcessesFromMembersLocation,
            recorder.InheritPropertiesFromMembersLocation, recorder.InheritConstantsFromMembersLocation, recorder.InheritConversionsFromGroupLocation, recorder.InheritConversionsFromMembersLocation);
    }

    private sealed class VectorGroupMemberAttributeArgumentRecorder : AArgumentRecorder
    {
        public ITypeSymbol? Group { get; private set; }
        public int? Dimension { get; private set; }
        public bool? InheritOperationsFromGroup { get; private set; }
        public bool? InheritOperationsFromMembers { get; private set; }
        public bool? InheritProcessesFromMembers { get; private set; }
        public bool? InheritPropertiesFromMembers { get; private set; }
        public bool? InheritConstantsFromMembers { get; private set; }
        public bool? InheritConversionsFromGroup { get; private set; }
        public bool? InheritConversionsFromMembers { get; private set; }

        public Location GroupLocation { get; private set; } = Location.None;
        public Location DimensionLocation { get; private set; } = Location.None;
        public Location InheritOperationsFromGroupLocation { get; private set; } = Location.None;
        public Location InheritOperationsFromMembersLocation { get; private set; } = Location.None;
        public Location InheritProcessesFromMembersLocation { get; private set; } = Location.None;
        public Location InheritPropertiesFromMembersLocation { get; private set; } = Location.None;
        public Location InheritConstantsFromMembersLocation { get; private set; } = Location.None;
        public Location InheritConversionsFromGroupLocation { get; private set; } = Location.None;
        public Location InheritConversionsFromMembersLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TGroup", Adapters.For(RecordGroup));
        }

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Dimension", Adapters.For<int>(RecordDimension));
            yield return ("InheritOperationsFromGroup", Adapters.For<bool>(RecordInheritOperationsFromGroup));
            yield return ("InheritOperationsFromMembers", Adapters.For<bool>(RecordInheritOperationsFromMembers));
            yield return ("InheritProcessesFromMembers", Adapters.For<bool>(RecordInheritProcessesFromMembers));
            yield return ("InheritPropertiesFromMembers", Adapters.For<bool>(RecordInheritPropertiesFromMembers));
            yield return ("InheritConstantsFromMembers", Adapters.For<bool>(RecordInheritConstantsFromMembers));
            yield return ("InheritConversionsFromGroup", Adapters.For<bool>(RecordInheritConversionsFromGroup));
            yield return ("InheritConversionsFromMembers", Adapters.For<bool>(RecordInheritConversionsFromMembers));
        }

        private void RecordGroup(ITypeSymbol group, Location location)
        {
            Group = group;
            GroupLocation = location;
        }

        private void RecordDimension(int dimension, Location location)
        {
            Dimension = dimension;
            DimensionLocation = location;
        }

        private void RecordInheritOperationsFromGroup(bool inheritOperationsFromGroup, Location location)
        {
            InheritOperationsFromGroup = inheritOperationsFromGroup;
            InheritOperationsFromGroupLocation = location;
        }

        private void RecordInheritOperationsFromMembers(bool inheritOperationsFromMembers, Location location)
        {
            InheritOperationsFromMembers = inheritOperationsFromMembers;
            InheritOperationsFromMembersLocation = location;
        }

        private void RecordInheritProcessesFromMembers(bool inheritProcessesFromMembers, Location location)
        {
            InheritProcessesFromMembers = inheritProcessesFromMembers;
            InheritProcessesFromMembersLocation = location;
        }

        private void RecordInheritPropertiesFromMembers(bool inheritPropertiesFromMembers, Location location)
        {
            InheritPropertiesFromMembers = inheritPropertiesFromMembers;
            InheritPropertiesFromMembersLocation = location;
        }

        private void RecordInheritConstantsFromMembers(bool inheritConstantsFromMembers, Location location)
        {
            InheritConstantsFromMembers = inheritConstantsFromMembers;
            InheritConstantsFromMembersLocation = location;
        }

        private void RecordInheritConversionsFromGroup(bool inheritConversionsFromGroup, Location location)
        {
            InheritConversionsFromGroup = inheritConversionsFromGroup;
            InheritConversionsFromGroupLocation = location;
        }

        private void RecordInheritConversionsFromMembers(bool inheritConversionsFromMembers, Location location)
        {
            InheritConversionsFromMembers = inheritConversionsFromMembers;
            InheritConversionsFromMembersLocation = location;
        }
    }
}

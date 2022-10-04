namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class SharpMeasuresVectorGroupMemberProperties
{
    public static IReadOnlyList<IAttributeProperty<SymbolicSharpMeasuresVectorGroupMemberDefinition>> AllProperties => new IAttributeProperty<SymbolicSharpMeasuresVectorGroupMemberDefinition>[]
    {
        VectorGroup,
        InheritOperations,
        InheritConversions,
        InheritUnits,
        InheritOperationsFromMembers,
        InheritProcessesFromMembers,
        InheritConstantsFromMembers,
        InheritConversionsFromMembers,
        InheritUnitsFromMembers,
        Dimension,
        GenerateDocumentation
    };

    private static SharpMeasuresVectorGroupMemberProperty<INamedTypeSymbol> VectorGroup { get; } = new
    (
        name: nameof(VectorGroupMemberAttribute.VectorGroup),
        setter: static (definition, vectorGroup) => definition with { VectorGroup = vectorGroup },
        locator: static (locations, vectorGroupLocation) => locations with { VectorGroup = vectorGroupLocation }
    );

    private static SharpMeasuresVectorGroupMemberProperty<bool> InheritOperations { get; } = new
    (
        name: nameof(VectorGroupMemberAttribute.InheritOperations),
        setter: static (definition, inheritOperations) => definition with { InheritOperations = inheritOperations },
        locator: static (locations, inheritOperationsLocation) => locations with { InheritOperations = inheritOperationsLocation }
    );

    private static SharpMeasuresVectorGroupMemberProperty<bool> InheritConversions { get; } = new
    (
        name: nameof(VectorGroupMemberAttribute.InheritConversions),
        setter: static (definition, inheritConversions) => definition with { InheritConversions = inheritConversions },
        locator: static (locations, inheritConversionsLocation) => locations with { InheritConversions = inheritConversionsLocation }
    );

    private static SharpMeasuresVectorGroupMemberProperty<bool> InheritUnits { get; } = new
    (
        name: nameof(VectorGroupMemberAttribute.InheritUnits),
        setter: static (definition, inheritUnits) => definition with { InheritUnits = inheritUnits },
        locator: static (locations, inheritUnitsLocation) => locations with { InheritUnits = inheritUnitsLocation }
    );

    private static SharpMeasuresVectorGroupMemberProperty<bool> InheritOperationsFromMembers { get; } = new
    (
        name: nameof(VectorGroupMemberAttribute.InheritOperationsFromMembers),
        setter: static (definition, inheritOperationsFromMembers) => definition with { InheritOperationsFromMembers = inheritOperationsFromMembers },
        locator: static (locations, inheritOperationsFromMembersLocation) => locations with { InheritOperationsFromMembers = inheritOperationsFromMembersLocation }
    );

    private static SharpMeasuresVectorGroupMemberProperty<bool> InheritProcessesFromMembers { get; } = new
    (
        name: nameof(VectorGroupMemberAttribute.InheritProcessesFromMembers),
        setter: static (definition, inheritProcessesFromMembers) => definition with { InheritProcessesFromMembers = inheritProcessesFromMembers },
        locator: static (locations, inheritProcessesFromMembersLocation) => locations with { InheritProcessesFromMembers = inheritProcessesFromMembersLocation }
    );

    private static SharpMeasuresVectorGroupMemberProperty<bool> InheritConstantsFromMembers { get; } = new
    (
        name: nameof(VectorGroupMemberAttribute.InheritConstantsFromMembers),
        setter: static (definition, inheritConstantsFromMembers) => definition with { InheritConstantsFromMembers = inheritConstantsFromMembers },
        locator: static (locations, inheritConstantsFromMembersLocation) => locations with { InheritConstantsFromMembers = inheritConstantsFromMembersLocation }
    );

    private static SharpMeasuresVectorGroupMemberProperty<bool> InheritConversionsFromMembers { get; } = new
    (
        name: nameof(VectorGroupMemberAttribute.InheritConversionsFromMembers),
        setter: static (definition, inheritConversionsFromMembers) => definition with { InheritConversionsFromMembers = inheritConversionsFromMembers },
        locator: static (locations, inheritConversionsFromMembersLocation) => locations with { InheritConversionsFromMembers = inheritConversionsFromMembersLocation }
    );

    private static SharpMeasuresVectorGroupMemberProperty<bool> InheritUnitsFromMembers { get; } = new
    (
        name: nameof(VectorGroupMemberAttribute.InheritUnitsFromMembers),
        setter: static (definition, inheritUnitsFromMembers) => definition with { InheritUnitsFromMembers = inheritUnitsFromMembers },
        locator: static (locations, inheritUnitsFromMembersLocation) => locations with { InheritUnitsFromMembers = inheritUnitsFromMembersLocation }
    );

    private static SharpMeasuresVectorGroupMemberProperty<int> Dimension { get; } = new
    (
        name: nameof(VectorGroupMemberAttribute.Dimension),
        setter: static (definition, dimension) => definition with { Dimension = dimension },
        locator: static (locations, dimensionLocation) => locations with { Dimension = dimensionLocation }
    );

    private static SharpMeasuresVectorGroupMemberProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(VectorGroupMemberAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}

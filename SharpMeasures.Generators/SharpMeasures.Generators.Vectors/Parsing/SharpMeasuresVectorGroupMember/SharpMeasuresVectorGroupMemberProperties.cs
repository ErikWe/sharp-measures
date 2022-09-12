namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class SharpMeasuresVectorGroupMemberProperties
{
    public static IReadOnlyList<IAttributeProperty<RawSharpMeasuresVectorGroupMemberDefinition>> AllProperties
        => new IAttributeProperty<RawSharpMeasuresVectorGroupMemberDefinition>[]
    {
        VectorGroup,
        InheritDerivations,
        InheritConversions,
        InheritUnits,
        InheritDerivationsFromMembers,
        InheritConstantsFromMembers,
        InheritConversionsFromMembers,
        InheritUnitsFromMembers,
        Dimension,
        GenerateDocumentation
    };

    private static SharpMeasuresVectorGroupMemberProperty<INamedTypeSymbol> VectorGroup { get; } = new
    (
        name: nameof(SharpMeasuresVectorGroupMemberAttribute.VectorGroup),
        setter: static (definition, vectorGroup) => definition with { VectorGroup = vectorGroup.AsNamedType() },
        locator: static (locations, vectorGroupLocation) => locations with { VectorGroup = vectorGroupLocation }
    );

    private static SharpMeasuresVectorGroupMemberProperty<bool> InheritDerivations { get; } = new
    (
        name: nameof(SharpMeasuresVectorGroupMemberAttribute.InheritDerivations),
        setter: static (definition, inheritDerivations) => definition with { InheritDerivations = inheritDerivations },
        locator: static (locations, inheritDerivationsLocation) => locations with { InheritDerivations = inheritDerivationsLocation }
    );

    private static SharpMeasuresVectorGroupMemberProperty<bool> InheritConversions { get; } = new
    (
        name: nameof(SharpMeasuresVectorGroupMemberAttribute.InheritConversions),
        setter: static (definition, inheritConversions) => definition with { InheritConversions = inheritConversions },
        locator: static (locations, inheritConversionsLocation) => locations with { InheritConversions = inheritConversionsLocation }
    );

    private static SharpMeasuresVectorGroupMemberProperty<bool> InheritUnits { get; } = new
    (
        name: nameof(SharpMeasuresVectorGroupMemberAttribute.InheritUnits),
        setter: static (definition, inheritUnits) => definition with { InheritUnits = inheritUnits },
        locator: static (locations, inheritUnitsLocation) => locations with { InheritUnits = inheritUnitsLocation }
    );

    private static SharpMeasuresVectorGroupMemberProperty<bool> InheritDerivationsFromMembers { get; } = new
    (
        name: nameof(SharpMeasuresVectorGroupMemberAttribute.InheritDerivationsFromMembers),
        setter: static (definition, inheritDerivationsFromMembers) => definition with { InheritDerivationsFromMembers = inheritDerivationsFromMembers },
        locator: static (locations, inheritDerivationsFromMembersLocation) => locations with { InheritDerivationsFromMembers = inheritDerivationsFromMembersLocation }
    );

    private static SharpMeasuresVectorGroupMemberProperty<bool> InheritConstantsFromMembers { get; } = new
    (
        name: nameof(SharpMeasuresVectorGroupMemberAttribute.InheritConstantsFromMembers),
        setter: static (definition, inheritConstantsFromMembers) => definition with { InheritConstantsFromMembers = inheritConstantsFromMembers },
        locator: static (locations, inheritConstantsFromMembersLocation) => locations with { InheritConstantsFromMembers = inheritConstantsFromMembersLocation }
    );

    private static SharpMeasuresVectorGroupMemberProperty<bool> InheritConversionsFromMembers { get; } = new
    (
        name: nameof(SharpMeasuresVectorGroupMemberAttribute.InheritConversionsFromMembers),
        setter: static (definition, inheritConversionsFromMembers) => definition with { InheritConversionsFromMembers = inheritConversionsFromMembers },
        locator: static (locations, inheritConversionsFromMembersLocation) => locations with { InheritConversionsFromMembers = inheritConversionsFromMembersLocation }
    );

    private static SharpMeasuresVectorGroupMemberProperty<bool> InheritUnitsFromMembers { get; } = new
    (
        name: nameof(SharpMeasuresVectorGroupMemberAttribute.InheritUnitsFromMembers),
        setter: static (definition, inheritUnitsFromMembers) => definition with { InheritUnitsFromMembers = inheritUnitsFromMembers },
        locator: static (locations, inheritUnitsFromMembersLocation) => locations with { InheritUnitsFromMembers = inheritUnitsFromMembersLocation }
    );

    private static SharpMeasuresVectorGroupMemberProperty<int> Dimension { get; } = new
    (
        name: nameof(SharpMeasuresVectorGroupMemberAttribute.Dimension),
        setter: static (definition, dimension) => definition with { Dimension = dimension },
        locator: static (locations, dimensionLocation) => locations with { Dimension = dimensionLocation }
    );

    private static SharpMeasuresVectorGroupMemberProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(SharpMeasuresVectorGroupMemberAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}

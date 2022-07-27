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
        Dimension,
        GenerateDocumentation
    };

    private static SharpMeasuresVectorGroupMemberProperty<INamedTypeSymbol> VectorGroup { get; } = new
    (
        name: nameof(SharpMeasuresVectorGroupMemberAttribute.VectorGroup),
        setter: static (definition, vectorGroup) => definition with { VectorGroup = vectorGroup.AsNamedType() },
        locator: static (locations, vectorGroupLocation) => locations with { VectorGroup = vectorGroupLocation }
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

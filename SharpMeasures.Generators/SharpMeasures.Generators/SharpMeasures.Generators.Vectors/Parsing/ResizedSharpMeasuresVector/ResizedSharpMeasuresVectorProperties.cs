namespace SharpMeasures.Generators.Vectors.Parsing.ResizedSharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class ResizedSharpMeasuresVectorProperties
{
    public static IReadOnlyList<IAttributeProperty<RawResizedSharpMeasuresVectorDefinition>> AllProperties => new IAttributeProperty<RawResizedSharpMeasuresVectorDefinition>[]
    {
        AssociatedVector,
        Dimension,
        Resizedocumentation
    };

    private static ResizedSharpMeasuresVectorProperty<INamedTypeSymbol> AssociatedVector { get; } = new
    (
        name: nameof(ResizedSharpMeasuresVectorAttribute.AssociatedVector),
        setter: static (definition, associatedVector) => definition with { AssociatedVector = associatedVector.AsNamedType() },
        locator: static (locations, associatedVectorLocation) => locations with { AssociatedVector = associatedVectorLocation }
    );

    private static ResizedSharpMeasuresVectorProperty<int> Dimension { get; } = new
    (
        name: nameof(ResizedSharpMeasuresVectorAttribute.Dimension),
        setter: static (definition, dimension) => definition with { Dimension = dimension },
        locator: static (locations, dimensionLocation) => locations with { Dimension = dimensionLocation }
    );

    private static ResizedSharpMeasuresVectorProperty<bool> Resizedocumentation { get; } = new
    (
        name: nameof(ResizedSharpMeasuresVectorAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}

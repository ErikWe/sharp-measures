namespace SharpMeasures.Generators.Vectors.Parsing.ResizedVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class ResizedVectorProperties
{
    public static IReadOnlyList<IAttributeProperty<RawResizedVectorDefinition>> AllProperties => new IAttributeProperty<RawResizedVectorDefinition>[]
    {
        AssociatedVector,
        Dimension,
        Resizedocumentation
    };

    private static ResizedVectorProperty<INamedTypeSymbol> AssociatedVector { get; } = new
    (
        name: nameof(ResizedVectorAttribute.AssociatedVector),
        setter: static (definition, associatedVector) => definition with { AssociatedVector = associatedVector.AsNamedType() },
        locator: static (locations, associatedVectorLocation) => locations with { AssociatedVector = associatedVectorLocation }
    );

    private static ResizedVectorProperty<int> Dimension { get; } = new
    (
        name: nameof(ResizedVectorAttribute.Dimension),
        setter: static (definition, dimension) => definition with { Dimension = dimension },
        locator: static (locations, dimensionLocation) => locations with { Dimension = dimensionLocation }
    );

    private static ResizedVectorProperty<bool> Resizedocumentation { get; } = new
    (
        name: nameof(ResizedVectorAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}

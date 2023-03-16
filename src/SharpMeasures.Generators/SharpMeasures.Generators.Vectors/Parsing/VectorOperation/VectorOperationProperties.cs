namespace SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class VectorOperationProperties
{
    public static IReadOnlyList<IAttributeProperty<SymbolicVectorOperationDefinition>> AllProperties => new IAttributeProperty<SymbolicVectorOperationDefinition>[]
    {
        Result,
        Other,
        OperatorType,
        Position,
        Mirror,
        Name,
        MirroredName
    };

    private static VectorOperationProperty<INamedTypeSymbol> Result { get; } = new
    (
        name: nameof(VectorOperationAttribute.Result),
        setter: static (definition, result) => definition with { Result = result },
        locator: static (locations, resultLocation) => locations with { Result = resultLocation }
    );

    private static VectorOperationProperty<INamedTypeSymbol> Other { get; } = new
    (
        name: nameof(VectorOperationAttribute.Other),
        setter: static (definition, other) => definition with { Other = other },
        locator: static (locations, otherLocation) => locations with { Other = otherLocation }
    );

    private static VectorOperationProperty<int> OperatorType { get; } = new
    (
        name: nameof(VectorOperationAttribute.OperatorType),
        setter: static (definition, operatortype) => definition with { OperatorType = (VectorOperatorType)operatortype },
        locator: static (locations, operatorTypeLocation) => locations with { OperatorType = operatorTypeLocation }
    );

    private static VectorOperationProperty<int> Position { get; } = new
    (
        name: nameof(VectorOperationAttribute.Position),
        setter: static (definition, position) => definition with { Position = (OperatorPosition)position },
        locator: static (locations, positionLocation) => locations with { Position = positionLocation }
    );

    private static VectorOperationProperty<bool> Mirror { get; } = new
    (
        name: nameof(VectorOperationAttribute.Mirror),
        setter: static (definition, mirror) => definition with { Mirror = mirror },
        locator: static (locations, mirrorLocation) => locations with { Mirror = mirrorLocation }
    );

    private static VectorOperationProperty<string> Name { get; } = new
    (
        name: nameof(VectorOperationAttribute.Name),
        setter: static (definition, name) => definition with { Name = name },
        locator: static (locations, nameLocation) => locations with { Name = nameLocation }
    );

    private static VectorOperationProperty<string> MirroredName { get; } = new
    (
        name: nameof(VectorOperationAttribute.MirroredName),
        setter: static (definition, mirroredName) => definition with { MirroredName = mirroredName },
        locator: static (locations, mirroredName) => locations with { MirroredName = mirroredName }
    );
}

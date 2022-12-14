namespace SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class QuantityOperationProperties
{
    public static IReadOnlyList<IAttributeProperty<SymbolicQuantityOperationDefinition>> AllProperties => new IAttributeProperty<SymbolicQuantityOperationDefinition>[]
    {
        Result,
        Other,
        OperatorType,
        Position,
        Mirror,
        Implementation,
        MethodName,
        MirroredMethodName
    };

    private static QuantityOperationProperty<INamedTypeSymbol> Result { get; } = new
    (
        name: nameof(QuantityOperationAttribute.Result),
        setter: static (definition, result) => definition with { Result = result },
        locator: static (locations, resultLocation) => locations with { Result = resultLocation }
    );

    private static QuantityOperationProperty<INamedTypeSymbol> Other { get; } = new
    (
        name: nameof(QuantityOperationAttribute.Other),
        setter: static (definition, other) => definition with { Other = other },
        locator: static (locations, otherLocation) => locations with { Other = otherLocation }
    );

    private static QuantityOperationProperty<int> OperatorType { get; } = new
    (
        name: nameof(QuantityOperationAttribute.OperatorType),
        setter: static (definition, operatortype) => definition with { OperatorType = (OperatorType)operatortype },
        locator: static (locations, operatorTypeLocation) => locations with { OperatorType = operatorTypeLocation }
    );

    private static QuantityOperationProperty<int> Position { get; } = new
    (
        name: nameof(QuantityOperationAttribute.Position),
        setter: static (definition, position) => definition with { Position = (OperatorPosition)position },
        locator: static (locations, positionLocation) => locations with { Position = positionLocation }
    );

    private static QuantityOperationProperty<bool> Mirror { get; } = new
    (
        name: nameof(QuantityOperationAttribute.Mirror),
        setter: static (definition, mirror) => definition with { Mirror = mirror },
        locator: static (locations, mirrorLocation) => locations with { Mirror = mirrorLocation }
    );

    private static QuantityOperationProperty<int> Implementation { get; } = new
    (
        name: nameof(QuantityOperationAttribute.Implementation),
        setter: static (definition, implementation) => definition with { Implementation = (QuantityOperationImplementation)implementation },
        locator: static (locations, implementationLocation) => locations with { Implementation = implementationLocation }
    );

    private static QuantityOperationProperty<string> MethodName { get; } = new
    (
        name: nameof(QuantityOperationAttribute.MethodName),
        setter: static (definition, methodName) => definition with { MethodName = methodName },
        locator: static (locations, methodNameLocation) => locations with { MethodName = methodNameLocation }
    );

    private static QuantityOperationProperty<string> MirroredMethodName { get; } = new
    (
        name: nameof(QuantityOperationAttribute.MirroredMethodName),
        setter: static (definition, mirroredMethodName) => definition with { MirroredMethodName = mirroredMethodName },
        locator: static (locations, mirroredMethodNameLocation) => locations with { MirroredMethodName = mirroredMethodNameLocation }
    );
}

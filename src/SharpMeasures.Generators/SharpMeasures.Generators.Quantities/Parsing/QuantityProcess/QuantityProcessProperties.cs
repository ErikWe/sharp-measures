namespace SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class QuantityProcessProperties
{
    public static IReadOnlyList<IAttributeProperty<RawQuantityProcessDefinition>> AllProperties => new IAttributeProperty<RawQuantityProcessDefinition>[]
    {
        Name,
        Result,
        Expression,
        ImplementAsProperty,
        ImplementStatically,
        ParameterTypes,
        ParameterNames
    };

    private static QuantityProcessProperty<string> Name { get; } = new
    (
        name: nameof(QuantityProcessAttribute.Name),
        setter: static (definition, name) => definition with { Name = name },
        locator: static (locations, nameLocation) => locations with { Name = nameLocation }
    );

    private static QuantityProcessProperty<INamedTypeSymbol> Result { get; } = new
    (
        name: nameof(QuantityProcessAttribute.Result),
        setter: static (definition, result) => definition with { Result = result.AsNamedType() },
        locator: static (locations, resultLocation) => locations with { Result = resultLocation }
    );

    private static QuantityProcessProperty<string> Expression { get; } = new
    (
        name: nameof(QuantityProcessAttribute.Expression),
        setter: static (definition, expression) => definition with { Expression = expression },
        locator: static (locations, expressionLocation) => locations with { Expression = expressionLocation }
    );

    private static QuantityProcessProperty<bool> ImplementAsProperty { get; } = new
    (
        name: nameof(QuantityProcessAttribute.ImplementAsProperty),
        setter: static (definition, implementAsProperty) => definition with { ImplementAsProperty = implementAsProperty },
        locator: static (locations, implementAsPropertyLocation) => locations with { ImplementAsProperty = implementAsPropertyLocation }
    );

    private static QuantityProcessProperty<bool> ImplementStatically { get; } = new
    (
        name: nameof(QuantityProcessAttribute.ImplementStatically),
        setter: static (definition, implementStatically) => definition with { ImplementStatically = implementStatically },
        locator: static (locations, implementStaticallyLocation) => locations with { ImplementStatically = implementStaticallyLocation }
    );

    private static QuantityProcessProperty<INamedTypeSymbol[]> ParameterTypes { get; } = new
    (
        name: nameof(QuantityProcessAttribute.ParameterTypes),
        setter: static (definition, parameterTypes) => definition with { ParameterTypes = parameterTypes.AsNamedTypes() },
        locator: static (locations, collectionLocation, elementLocations) => locations with
        {
            ParameterTypesCollection = collectionLocation,
            ParameterTypeElements = elementLocations
        }
    );

    private static QuantityProcessProperty<string[]> ParameterNames { get; } = new
    (
        name: nameof(QuantityProcessAttribute.ParameterNames),
        setter: static (definition, parameterNames) => definition with { ParameterNames = parameterNames },
        locator: static (locations, collectionLocation, elementLocations) => locations with
        {
            ParameterNamesCollection = collectionLocation,
            ParameterNameElements = elementLocations
        }
    );
}

namespace SharpMeasures.Generators.Quantities.Parsing.ProcessedQuantity;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class ProcessedQuantityProperties
{
    public static IReadOnlyList<IAttributeProperty<RawProcessedQuantityDefinition>> AllProperties => new IAttributeProperty<RawProcessedQuantityDefinition>[]
    {
        Name,
        Result,
        Expression,
        ImplementAsProperty,
        ImplementStatically,
        ParameterTypes,
        ParameterNames
    };

    private static ProcessedQuantityProperty<string> Name { get; } = new
    (
        name: nameof(ProcessedQuantityAttribute.Name),
        setter: static (definition, name) => definition with { Name = name },
        locator: static (locations, nameLocation) => locations with { Name = nameLocation }
    );

    private static ProcessedQuantityProperty<INamedTypeSymbol> Result { get; } = new
    (
        name: nameof(ProcessedQuantityAttribute.Result),
        setter: static (definition, result) => definition with { Result = result.AsNamedType() },
        locator: static (locations, resultLocation) => locations with { Result = resultLocation }
    );

    private static ProcessedQuantityProperty<string> Expression { get; } = new
    (
        name: nameof(ProcessedQuantityAttribute.Expression),
        setter: static (definition, expression) => definition with { Expression = expression },
        locator: static (locations, expressionLocation) => locations with { Expression = expressionLocation }
    );

    private static ProcessedQuantityProperty<bool> ImplementAsProperty { get; } = new
    (
        name: nameof(ProcessedQuantityAttribute.ImplementAsProperty),
        setter: static (definition, implementAsProperty) => definition with { ImplementAsProperty = implementAsProperty },
        locator: static (locations, implementAsPropertyLocation) => locations with { ImplementAsProperty = implementAsPropertyLocation }
    );

    private static ProcessedQuantityProperty<bool> ImplementStatically { get; } = new
    (
        name: nameof(ProcessedQuantityAttribute.ImplementStatically),
        setter: static (definition, implementStatically) => definition with { ImplementStatically = implementStatically },
        locator: static (locations, implementStaticallyLocation) => locations with { ImplementStatically = implementStaticallyLocation }
    );

    private static ProcessedQuantityProperty<INamedTypeSymbol[]> ParameterTypes { get; } = new
    (
        name: nameof(ProcessedQuantityAttribute.ParameterTypes),
        setter: static (definition, parameterTypes) => definition with { ParameterTypes = parameterTypes.AsNamedTypes() },
        locator: static (locations, collectionLocation, elementLocations) => locations with
        {
            ParameterTypesCollection = collectionLocation,
            ParameterTypeElements = elementLocations
        }
    );

    private static ProcessedQuantityProperty<string[]> ParameterNames { get; } = new
    (
        name: nameof(ProcessedQuantityAttribute.ParameterNames),
        setter: static (definition, parameterNames) => definition with { ParameterNames = parameterNames },
        locator: static (locations, collectionLocation, elementLocations) => locations with
        {
            ParameterNamesCollection = collectionLocation,
            ParameterNameElements = elementLocations
        }
    );
}

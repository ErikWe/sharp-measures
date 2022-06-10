namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class VectorConstantProperties
{
    public static IReadOnlyList<IAttributeProperty<RawVectorConstantDefinition>> AllProperties => new IAttributeProperty<RawVectorConstantDefinition>[]
    {
        Name,
        Unit,
        Value,
        GenerateMultiplesProperty,
        MultiplesName
    };

    private static VectorConstantProperty<string> Name { get; } = new
    (
        name: nameof(VectorConstantAttribute.Name),
        setter: static (definition, name) => definition with { Name = name },
        locator: static (locations, nameLocation) => locations with { Name = nameLocation }
    );

    private static VectorConstantProperty<string> Unit { get; } = new
    (
        name: nameof(VectorConstantAttribute.Unit),
        setter: static (definition, unit) => definition with { Unit = unit },
        locator: static (locations, unitLocation) => locations with { Unit = unitLocation }
    );

    private static VectorConstantProperty<double[]> Value { get; } = new
    (
        name: nameof(VectorConstantAttribute.Value),
        setter: static (definition, value) => definition with { Value = new(value) },
        locator: static (locations, collectionLocation, elementLocations) => locations with
        {
            ValueCollection = collectionLocation,
            ValueElements = elementLocations
        }
    );

    private static VectorConstantProperty<bool> GenerateMultiplesProperty { get; } = new
    (
        name: nameof(VectorConstantAttribute.GenerateMultiplesProperty),
        setter: static (definition, generateMagnitudeProperty) => definition with { GenerateMultiplesProperty = generateMagnitudeProperty },
        locator: static (locations, generateMagnitudePropertLocation) => locations with { GenerateMultiplesProperty = generateMagnitudePropertLocation }
    );

    private static VectorConstantProperty<string> MultiplesName { get; } = new
    (
        name: nameof(VectorConstantAttribute.MultiplesName),
        setter: static (definition, magnitudePropertyName) => definition with { MultiplesName = magnitudePropertyName },
        locator: static (locations, magnitudePropertyNameLocation) => locations with { MultiplesName = magnitudePropertyNameLocation }
    );
}

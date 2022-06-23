namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using SharpMeasures.Equatables;
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
        Multiples
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
        setter: static (definition, value) => definition with { Value = value.AsReadOnlyEquatable() },
        locator: static (locations, collectionLocation, elementLocations) => locations with
        {
            ValueCollection = collectionLocation,
            ValueElements = elementLocations
        }
    );

    private static VectorConstantProperty<bool> GenerateMultiplesProperty { get; } = new
    (
        name: nameof(VectorConstantAttribute.GenerateMultiplesProperty),
        setter: static (definition, generateMultiplesProperty) => definition with { GenerateMultiplesProperty = generateMultiplesProperty },
        locator: static (locations, generateMultiplesPropertyLocation) => locations with { GenerateMultiplesProperty = generateMultiplesPropertyLocation }
    );

    private static VectorConstantProperty<string> Multiples { get; } = new
    (
        name: nameof(VectorConstantAttribute.Multiples),
        setter: static (definition, multiples) => definition with { Multiples = multiples },
        locator: static (locations, multiplesLocation) => locations with { Multiples = multiplesLocation }
    );
}

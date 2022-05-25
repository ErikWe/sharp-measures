namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal static class ScalarConstantProperties
{
    public static IReadOnlyList<IAttributeProperty<ScalarConstantDefinition>> AllProperties => new IAttributeProperty<ScalarConstantDefinition>[]
    {
        Name,
        Value,
        Unit,
        GenerateMultiplesProperty,
        MultiplesName
    };

    private static ConstantScalarProperty<string> Name { get; } = new
    (
        name: nameof(ScalarConstantAttribute.Name),
        setter: static (definition, name) => definition with { Name = name },
        locator: static (locations, nameLocation) => locations with { Name = nameLocation }
    );

    private static ConstantScalarProperty<double> Value { get; } = new
    (
        name: nameof(ScalarConstantAttribute.Value),
        setter: static (definition, value) => definition with { Value = value },
        locator: static (locations, valueLocation) => locations with { Value = valueLocation }
    );

    private static ConstantScalarProperty<string> Unit { get; } = new
    (
        name: nameof(ScalarConstantAttribute.Unit),
        setter: static (definition, unit) => definition with { Unit = unit },
        locator: static (locations, unitLocation) => locations with { Unit = unitLocation }
    );

    private static ConstantScalarProperty<bool> GenerateMultiplesProperty { get; } = new
    (
        name: nameof(ScalarConstantAttribute.GenerateMultiplesProperty),
        setter: static (definition, generateMagnitudeProperty) => definition with { GenerateMultiplesProperty = generateMagnitudeProperty },
        locator: static (locations, generateMagnitudePropertLocation) => locations with { GenerateMultiplesProperty = generateMagnitudePropertLocation }
    );

    private static ConstantScalarProperty<string> MultiplesName { get; } = new
    (
        name: nameof(ScalarConstantAttribute.MultiplesName),
        setter: static (definition, magnitudePropertyName) => definition with { MultiplesName = magnitudePropertyName },
        locator: static (locations, magnitudePropertyNameLocation) => locations with { MultiplesName = magnitudePropertyNameLocation }
    );
}

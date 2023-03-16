namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class ScalarConstantProperties
{
    public static IReadOnlyList<IAttributeProperty<RawScalarConstantDefinition>> AllProperties => new IAttributeProperty<RawScalarConstantDefinition>[]
    {
        Name,
        UnitInstanceName,
        Value,
        Expression,
        GenerateMultiplesProperty,
        Multiples,
        MultiplesRegexSubstitution
    };

    private static ScalarConstantProperty<string> Name { get; } = new
    (
        name: nameof(ScalarConstantAttribute.Name),
        setter: static (definition, name) => definition with { Name = name },
        locator: static (locations, nameLocation) => locations with { Name = nameLocation }
    );

    private static ScalarConstantProperty<string> UnitInstanceName { get; } = new
    (
        name: nameof(ScalarConstantAttribute.UnitInstanceName),
        setter: static (definition, unitInstanceName) => definition with { UnitInstanceName = unitInstanceName },
        locator: static (locations, unitInstanceNameLocation) => locations with { UnitInstanceName = unitInstanceNameLocation }
    );

    private static ScalarConstantProperty<double> Value { get; } = new
    (
        name: nameof(ScalarConstantAttribute.Value),
        setter: static (definition, value) => definition with { Value = value },
        locator: static (locations, valueLocation) => locations with { Value = valueLocation }
    );

    private static ScalarConstantProperty<string> Expression { get; } = new
    (
        name: nameof(ScalarConstantAttribute.Expression),
        setter: static (definition, expression) => definition with { Expression = expression },
        locator: static (locations, expressionLocation) => locations with { Expression = expressionLocation }
    );

    private static ScalarConstantProperty<bool> GenerateMultiplesProperty { get; } = new
    (
        name: nameof(ScalarConstantAttribute.GenerateMultiplesProperty),
        setter: static (definition, generateMultiplesProperty) => definition with { GenerateMultiplesProperty = generateMultiplesProperty },
        locator: static (locations, generateMultiplesPropertyLocation) => locations with { GenerateMultiplesProperty = generateMultiplesPropertyLocation }
    );

    private static ScalarConstantProperty<string?> Multiples { get; } = new
    (
        name: nameof(ScalarConstantAttribute.Multiples),
        setter: static (definition, multiples) => definition with { Multiples = multiples },
        locator: static (locations, multiplesLocation) => locations with { Multiples = multiplesLocation }
    );

    private static ScalarConstantProperty<string?> MultiplesRegexSubstitution { get; } = new
    (
        name: nameof(ScalarConstantAttribute.MultiplesRegexSubstitution),
        setter: static (definition, multiplesRegexSubstitution) => definition with { MultiplesRegexSubstitution = multiplesRegexSubstitution },
        locator: static (locations, multiplesRegexSubstitutionLocation) => locations with { MultiplesRegexSubstitution = multiplesRegexSubstitutionLocation }
    );
}

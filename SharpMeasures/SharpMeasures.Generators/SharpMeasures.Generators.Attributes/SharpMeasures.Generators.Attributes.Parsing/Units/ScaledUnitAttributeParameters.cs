namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;
using System.Linq;

public readonly record struct ScaledUnitAttributeParameters(string Name, string Plural, string From, double Scale)
    : IUnitAttributeParameters, IDerivedUnitAttributeParameters
{
    string IDerivedUnitAttributeParameters.DerivedFrom => From;

    public static ScaledUnitAttributeParameters Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<ScaledUnitAttributeParameters> Parse(INamedTypeSymbol symbol)
        => ParameterParser.Parse<ScaledUnitAttributeParameters, ScaledUnitAttribute>(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<ScaledUnitAttributeParameters> Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseIndices<ScaledUnitAttributeParameters, ScaledUnitAttribute>(symbol, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    private static ScaledUnitAttributeParameters Defaults { get; } = new
    (
        Name: string.Empty,
        Plural: string.Empty,
        From: string.Empty,
        Scale: 0
    );

    private static Dictionary<string, AttributeProperty<ScaledUnitAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<ScaledUnitAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<ScaledUnitAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            From,
            Scale
        };

        private static AttributeProperty<ScaledUnitAttributeParameters> Name { get; } = new
        (
            name: nameof(ScaledUnitAttribute.Name),
            setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
        );

        private static AttributeProperty<ScaledUnitAttributeParameters> Plural { get; } = new
        (
            name: nameof(ScaledUnitAttribute.Plural),
            setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
        );

        private static AttributeProperty<ScaledUnitAttributeParameters> From { get; } = new
        (
            name: nameof(ScaledUnitAttribute.From),
            setter: static (parameters, obj) => obj is string from ? parameters with { From = from } : parameters
        );

        private static AttributeProperty<ScaledUnitAttributeParameters> Scale { get; } = new
        (
            name: nameof(ScaledUnitAttribute.Scale),
            setter: static (parameters, obj) => obj is double scale ? parameters with { Scale = scale } : parameters
        );
    }
}
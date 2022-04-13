namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;
using System.Linq;

public readonly record struct ScaledUnitInstanceAttributeParameters(string Name, string Plural, string Symbol, bool IsSIUnit, bool IsConstant,
    string From, double Scale)
    : IUnitInstanceAttributeParameters, IDerivedUnitInstanceAttributeParameters
{
    public static ScaledUnitInstanceAttributeParameters? Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<ScaledUnitInstanceAttributeParameters> Parse(INamedTypeSymbol symbol)
        => ParameterParser.Parse(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<ScaledUnitInstanceAttributeParameters> Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseIndices(symbol, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    string IDerivedUnitInstanceAttributeParameters.DerivedFrom => From;

    private static ScaledUnitInstanceAttributeParameters Defaults { get; } = new
    (
        Name: string.Empty,
        Plural: string.Empty,
        Symbol: string.Empty,
        IsSIUnit: false,
        IsConstant: false,
        From: string.Empty,
        Scale: 0
    );

    private static Dictionary<string, AttributeProperty<ScaledUnitInstanceAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<ScaledUnitInstanceAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<ScaledUnitInstanceAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            Symbol,
            IsSIUnit,
            IsConstant,
            From,
            Scale
        };

        private static AttributeProperty<ScaledUnitInstanceAttributeParameters> Name { get; } = new
        (
            name: nameof(ScaledUnitInstanceAttribute.Name),
            setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
        );

        private static AttributeProperty<ScaledUnitInstanceAttributeParameters> Plural { get; } = new
        (
            name: nameof(ScaledUnitInstanceAttribute.Plural),
            setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
        );

        private static AttributeProperty<ScaledUnitInstanceAttributeParameters> Symbol { get; } = new
        (
            name: nameof(ScaledUnitInstanceAttribute.Symbol),
            setter: static (parameters, obj) => obj is string symbol ? parameters with { Symbol = symbol } : parameters
        );

        private static AttributeProperty<ScaledUnitInstanceAttributeParameters> IsSIUnit { get; } = new
        (
            name: nameof(ScaledUnitInstanceAttribute.IsSIUnit),
            setter: static (parameters, obj) => obj is bool isSIUnit ? parameters with { IsSIUnit = isSIUnit } : parameters
        );

        private static AttributeProperty<ScaledUnitInstanceAttributeParameters> IsConstant { get; } = new
        (
            name: nameof(ScaledUnitInstanceAttribute.IsConstant),
            setter: static (parameters, obj) => obj is bool isConstant ? parameters with { IsConstant = isConstant } : parameters
        );

        private static AttributeProperty<ScaledUnitInstanceAttributeParameters> From { get; } = new
        (
            name: nameof(ScaledUnitInstanceAttribute.From),
            setter: static (parameters, obj) => obj is string from ? parameters with { From = from } : parameters
        );

        private static AttributeProperty<ScaledUnitInstanceAttributeParameters> Scale { get; } = new
        (
            name: nameof(ScaledUnitInstanceAttribute.Scale),
            setter: static (parameters, obj) => obj is double scale ? parameters with { Scale = scale } : parameters
        );
    }
}
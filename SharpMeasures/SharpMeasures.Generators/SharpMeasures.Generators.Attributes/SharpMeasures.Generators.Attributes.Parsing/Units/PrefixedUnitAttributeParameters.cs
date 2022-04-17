namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;
using System.Linq;

public readonly record struct PrefixedUnitAttributeParameters(string Name, string Plural, string Symbol, bool IsSIUnit, bool IsConstant,
    string From, string Prefix)
    : IUnitAttributeParameters, IDerivedUnitAttributeParameters
{
    public static PrefixedUnitAttributeParameters? Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<PrefixedUnitAttributeParameters> Parse(INamedTypeSymbol symbol)
        => ParameterParser.Parse<PrefixedUnitAttributeParameters, PrefixedUnitAttribute>(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<PrefixedUnitAttributeParameters> Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseIndices<PrefixedUnitAttributeParameters, PrefixedUnitAttribute>(symbol, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    string IDerivedUnitAttributeParameters.DerivedFrom => From;

    private static PrefixedUnitAttributeParameters Defaults { get; } = new
    (
        Name: string.Empty,
        Plural: string.Empty,
        Symbol: string.Empty,
        IsSIUnit: false,
        IsConstant: false,
        From: string.Empty,
        Prefix: string.Empty
    );

    private static Dictionary<string, AttributeProperty<PrefixedUnitAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<PrefixedUnitAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<PrefixedUnitAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            Symbol,
            IsSIUnit,
            IsConstant,
            From,
            Prefix
        };

        private static AttributeProperty<PrefixedUnitAttributeParameters> Name { get; } = new
        (
            name: nameof(PrefixedUnitAttribute.Name),
            setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
        );

        private static AttributeProperty<PrefixedUnitAttributeParameters> Plural { get; } = new
        (
            name: nameof(PrefixedUnitAttribute.Plural),
            setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
        );

        private static AttributeProperty<PrefixedUnitAttributeParameters> Symbol { get; } = new
        (
            name: nameof(PrefixedUnitAttribute.Symbol),
            setter: static (parameters, obj) => obj is string symbol ? parameters with { Symbol = symbol } : parameters
        );

        private static AttributeProperty<PrefixedUnitAttributeParameters> IsSIUnit { get; } = new
        (
            name: nameof(PrefixedUnitAttribute.IsSIUnit),
            setter: static (parameters, obj) => obj is bool isSIUnit ? parameters with { IsSIUnit = isSIUnit } : parameters
        );

        private static AttributeProperty<PrefixedUnitAttributeParameters> IsConstant { get; } = new
        (
            name: nameof(PrefixedUnitAttribute.IsConstant),
            setter: static (parameters, obj) => obj is bool isConstant ? parameters with { IsConstant = isConstant } : parameters
        );

        private static AttributeProperty<PrefixedUnitAttributeParameters> From { get; } = new
        (
            name: nameof(PrefixedUnitAttribute.From),
            setter: static (parameters, obj) => obj is string from ? parameters with { From = from } : parameters
        );

        private static AttributeProperty<PrefixedUnitAttributeParameters> Prefix { get; } = new
        (
            name: nameof(PrefixedUnitAttribute.Prefix),
            setter: static (parameters, obj) => obj is string prefix ? parameters with { Prefix = prefix } : parameters
        );
    }
}
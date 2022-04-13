namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public readonly record struct DerivedUnitInstanceAttributeParameters(string Name, string Plural, string Symbol, bool IsSIUnit, bool IsConstant,
    ReadOnlyCollection<INamedTypeSymbol?> Signature, ReadOnlyCollection<string> UnitInstanceNames)
    : IUnitInstanceAttributeParameters
{
    public static DerivedUnitInstanceAttributeParameters? Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<DerivedUnitInstanceAttributeParameters> Parse(INamedTypeSymbol symbol)
        => ParameterParser.Parse(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<DerivedUnitInstanceAttributeParameters> Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseIndices(symbol, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    private static DerivedUnitInstanceAttributeParameters Defaults { get; } = new
    (
        Name: string.Empty,
        Plural: string.Empty,
        Symbol: string.Empty,
        IsSIUnit: false,
        IsConstant: false,
        Signature: Array.AsReadOnly(Array.Empty<INamedTypeSymbol?>()),
        UnitInstanceNames: Array.AsReadOnly(Array.Empty<string>())
    );

    private static Dictionary<string, AttributeProperty<DerivedUnitInstanceAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<DerivedUnitInstanceAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<DerivedUnitInstanceAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            Symbol,
            IsSIUnit,
            IsConstant,
            Signature,
            UnitInstanceNames
        };

        private static AttributeProperty<DerivedUnitInstanceAttributeParameters> Name { get; } = new
        (
            name: nameof(DerivedUnitInstanceAttribute.Name),
            setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
        );

        private static AttributeProperty<DerivedUnitInstanceAttributeParameters> Plural { get; } = new
        (
            name: nameof(DerivedUnitInstanceAttribute.Plural),
            setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
        );

        private static AttributeProperty<DerivedUnitInstanceAttributeParameters> Symbol { get; } = new
        (
            name: nameof(DerivedUnitInstanceAttribute.Symbol),
            setter: static (parameters, obj) => obj is string symbol ? parameters with { Symbol = symbol } : parameters
        );

        private static AttributeProperty<DerivedUnitInstanceAttributeParameters> IsSIUnit { get; } = new
        (
            name: nameof(DerivedUnitInstanceAttribute.IsSIUnit),
            setter: static (parameters, obj) => obj is bool isSIUnit ? parameters with { IsSIUnit = isSIUnit } : parameters
        );

        private static AttributeProperty<DerivedUnitInstanceAttributeParameters> IsConstant { get; } = new
        (
            name: nameof(DerivedUnitInstanceAttribute.IsConstant),
            setter: static (parameters, obj) => obj is bool isConstant ? parameters with { IsConstant = isConstant } : parameters
        );

        private static AttributeProperty<DerivedUnitInstanceAttributeParameters> Signature { get; } = new
        (
            name: nameof(DerivedUnitInstanceAttribute.Signature),
            setter: static (parameters, obj) => obj is object?[] signature
                ? parameters with { Signature = Array.AsReadOnly(Array.ConvertAll(signature, static (x) => x as INamedTypeSymbol)) }
                : parameters
        );

        private static AttributeProperty<DerivedUnitInstanceAttributeParameters> UnitInstanceNames { get; } = new
        (
            name: nameof(DerivedUnitInstanceAttribute.UnitInstanceNames),
            setter: static (parameters, obj) => obj is object?[] signature
                ? parameters with { UnitInstanceNames = Array.AsReadOnly(Array.ConvertAll(signature, static (x) => x as string ?? string.Empty)) }
                : parameters
        );
    }
}
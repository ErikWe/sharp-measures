namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public readonly record struct DerivedUnitAttributeParameters(string Name, string Plural, ReadOnlyCollection<INamedTypeSymbol>? Signature,
    ReadOnlyCollection<string> Units)
    : IUnitAttributeParameters
{
    public static DerivedUnitAttributeParameters Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<DerivedUnitAttributeParameters> Parse(INamedTypeSymbol symbol)
        => ParameterParser.Parse<DerivedUnitAttributeParameters, DerivedUnitAttribute>(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<DerivedUnitAttributeParameters> Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseIndices<DerivedUnitAttributeParameters, DerivedUnitAttribute>(symbol, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    private static DerivedUnitAttributeParameters Defaults { get; } = new
    (
        Name: string.Empty,
        Plural: string.Empty,
        Signature: null,
        Units: Array.AsReadOnly(Array.Empty<string>())
    );

    private static Dictionary<string, AttributeProperty<DerivedUnitAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<DerivedUnitAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<DerivedUnitAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            Signature,
            Units
        };

        private static AttributeProperty<DerivedUnitAttributeParameters> Name { get; } = new
        (
            name: nameof(DerivedUnitAttribute.Name),
            setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
        );

        private static AttributeProperty<DerivedUnitAttributeParameters> Plural { get; } = new
        (
            name: nameof(DerivedUnitAttribute.Plural),
            setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
        );

        private static AttributeProperty<DerivedUnitAttributeParameters> Signature { get; } = new
        (
            name: nameof(DerivedUnitAttribute.Signature),
            setter: SetSignature
        );

        private static AttributeProperty<DerivedUnitAttributeParameters> Units { get; } = new
        (
            name: nameof(DerivedUnitAttribute.Units),
            setter: static (parameters, obj) => obj is object?[] signature
                ? parameters with { Units = Array.AsReadOnly(Array.ConvertAll(signature, static (x) => x as string ?? string.Empty)) }
                : parameters
        );

        private static DerivedUnitAttributeParameters SetSignature(DerivedUnitAttributeParameters parameters, object? obj)
        {
            if (obj is not object[] signature)
            {
                return parameters;
            }

            foreach (object signatureUnit in signature)
            {
                if (signatureUnit is not INamedTypeSymbol)
                {
                    return parameters;
                }
            }


            return parameters with { Signature = Array.AsReadOnly(Array.ConvertAll(signature, static (x) => (INamedTypeSymbol)x)) };
        }
    }
}
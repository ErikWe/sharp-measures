namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public readonly record struct DerivableUnitAttributeParameters(ReadOnlyCollection<INamedTypeSymbol?> Signature, ReadOnlyCollection<INamedTypeSymbol?> Quantities,
    string Expression)
{
    public static DerivableUnitAttributeParameters? Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<DerivableUnitAttributeParameters> Parse(INamedTypeSymbol symbol)
        => ParameterParser.Parse<DerivableUnitAttributeParameters, DerivableUnitAttribute>(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<DerivableUnitAttributeParameters> Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseIndices<DerivableUnitAttributeParameters, DerivableUnitAttribute>(symbol, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    private static DerivableUnitAttributeParameters Defaults { get; } = new
    (
        Signature: Array.AsReadOnly(Array.Empty<INamedTypeSymbol?>()),
        Quantities: Array.AsReadOnly(Array.Empty<INamedTypeSymbol?>()),
        Expression: string.Empty
    );

    private static Dictionary<string, AttributeProperty<DerivableUnitAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<DerivableUnitAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<DerivableUnitAttributeParameters>> AllProperties => new()
        {
            Signature,
            Expression
        };

        private static AttributeProperty<DerivableUnitAttributeParameters> Signature { get; } = new
        (
            name: nameof(DerivableUnitAttribute.Signature),
            setter: static (parameters, obj) => obj is object[] signature ? SetSignatureAndQuantities(parameters, signature) : parameters
        );

        private static AttributeProperty<DerivableUnitAttributeParameters> Expression { get; } = new
        (
            name: nameof(DerivableUnitAttribute.Expression),
            setter: static (parameters, obj) => obj is string expression ? parameters with { Expression = expression } : parameters
        );

        private static DerivableUnitAttributeParameters SetSignatureAndQuantities(DerivableUnitAttributeParameters parameters, object[] signature)
        {
            ReadOnlyCollection<INamedTypeSymbol?> units = Array.AsReadOnly(Array.ConvertAll(signature, static (x) => x as INamedTypeSymbol));
            INamedTypeSymbol?[] quantities = new INamedTypeSymbol?[signature.Length];

            for (int i = 0; i < signature.Length; i++)
            {
                if (signature[i] is not INamedTypeSymbol symbol)
                {
                    continue;
                }

                if (symbol.GetAttributeOfType<GeneratedUnitAttribute>() is AttributeData unitData)
                {
                    if (GeneratedUnitAttributeParameters.Parse(unitData) is GeneratedUnitAttributeParameters { Quantity: INamedTypeSymbol unitQuantity })
                    {
                        quantities[i] = unitQuantity;
                    }
                }
            }

            return parameters with { Signature = units, Quantities = Array.AsReadOnly(quantities) };
        }
    }
}
namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public readonly record struct DerivableUnitAttributeParameters(ReadOnlyCollection<INamedTypeSymbol>? Signature, ReadOnlyCollection<INamedTypeSymbol>? Quantities,
    string Expression)
{
    public static ParameterParser<DerivableUnitAttributeParameters, DerivableUnitAttribute> Parser { get; }
        = new(Properties.AllProperties, Defaults);

    private static DerivableUnitAttributeParameters Defaults => new
    (
        Signature: null,
        Quantities: null,
        Expression: string.Empty
    );

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
            foreach (object unit in signature)
            {
                if (unit is not INamedTypeSymbol)
                {
                    return parameters;
                }
            }

            ReadOnlyCollection<INamedTypeSymbol> units = Array.AsReadOnly(Array.ConvertAll(signature, static (x) => (INamedTypeSymbol)x));
            INamedTypeSymbol[] quantities = new INamedTypeSymbol[signature.Length];

            for (int i = 0; i < units.Count; i++)
            {
                if (units[i].GetAttributeOfType<GeneratedUnitAttribute>() is AttributeData unitData)
                {
                    if (GeneratedUnitAttributeParameters.Parser.Parse(unitData) is GeneratedUnitAttributeParameters { Quantity: INamedTypeSymbol unitQuantity })
                    {
                        quantities[i] = unitQuantity;
                    }
                    else
                    {
                        return parameters;
                    }
                }
                else
                {
                    return parameters;
                }
            }

            return parameters with { Signature = units, Quantities = Array.AsReadOnly(quantities) };
        }
    }
}
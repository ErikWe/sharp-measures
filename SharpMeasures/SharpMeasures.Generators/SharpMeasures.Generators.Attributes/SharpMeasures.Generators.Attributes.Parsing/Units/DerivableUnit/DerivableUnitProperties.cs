namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.ObjectModel;

internal static class DerivableUnitProperties
{
    public static ReadOnlyCollection<AttributeProperty<DerivableUnitParameters>> AllProperties => Array.AsReadOnly(new[]
    {
        Signature,
        Expression
    });

    public static AttributeProperty<DerivableUnitParameters> Signature { get; } = new
    (
        name: nameof(DerivableUnitAttribute.Signature),
        setter: static (parameters, obj) => obj is object[] signature ? SetSignatureAndQuantities(parameters, signature) : parameters
    );

    public static AttributeProperty<DerivableUnitParameters> Expression { get; } = new
    (
        name: nameof(DerivableUnitAttribute.Expression),
        setter: static (parameters, obj) => obj is string expression ? parameters with { Expression = expression } : parameters
    );

    private static DerivableUnitParameters SetSignatureAndQuantities(DerivableUnitParameters parameters, object[] signature)
    {
        foreach (object unit in signature)
        {
            if (unit is not INamedTypeSymbol)
            {
                return parameters;
            }
        }

        if (signature.Length == 0)
        {
            return parameters with { ParsingData = parameters.ParsingData with { EmptySignature = true } };
        }

        ReadOnlyCollection<INamedTypeSymbol> units = Array.AsReadOnly(Array.ConvertAll(signature, static (x) => (INamedTypeSymbol)x));
        INamedTypeSymbol[] quantities = new INamedTypeSymbol[signature.Length];

        for (int i = 0; i < units.Count; i++)
        {
            if (units[i].GetAttributeOfType<GeneratedUnitAttribute>() is AttributeData unitData)
            {
                if (GeneratedUnitParser.Parser.Parse(unitData) is GeneratedUnitParameters { Quantity: INamedTypeSymbol unitQuantity })
                {
                    quantities[i] = unitQuantity;
                }
                else
                {
                    DerivableUnitParsingData parsingData = parameters.ParsingData with { InvalidIndex = i };
                    return parameters with { Signature = units, ParsingData = parsingData };
                }
            }
            else
            {
                DerivableUnitParsingData parsingData = parameters.ParsingData with { InvalidIndex = i };
                return parameters with { Signature = units, ParsingData = parsingData };
            }
        }

        return parameters with { Signature = units, Quantities = Array.AsReadOnly(quantities) };
    }
}

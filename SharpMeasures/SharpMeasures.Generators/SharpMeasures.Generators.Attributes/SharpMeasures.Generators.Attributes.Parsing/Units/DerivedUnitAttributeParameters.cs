namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public readonly record struct DerivedUnitAttributeParameters(ReadOnlyCollection<INamedTypeSymbol?> Signature, ReadOnlyCollection<INamedTypeSymbol?> Quantities,
    string Expression)
{
    private static DerivedUnitAttributeParameters Defaults { get; } = new
    (
        Signature: Array.AsReadOnly(Array.Empty<INamedTypeSymbol?>()),
        Quantities: Array.AsReadOnly(Array.Empty<INamedTypeSymbol?>()),
        Expression: string.Empty
    );

    private static Dictionary<string, AttributeProperty<DerivedUnitAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<DerivedUnitAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    public static DerivedUnitAttributeParameters? Parse(AttributeData attributeData)
    {
        DerivedUnitAttributeParameters values = Defaults;

        (bool success, values) = AttributeDataArgumentParser.Parse(attributeData, values, ConstructorParameters, NamedParameters);

        return success ? values : null;
    }

    private static class Properties
    {
        public static List<AttributeProperty<DerivedUnitAttributeParameters>> AllProperties => new()
        {
            Signature,
            Expression
        };

        private static AttributeProperty<DerivedUnitAttributeParameters> Signature { get; } = new
        (
            name: nameof(DerivedUnitAttribute.Signature),
            setter: static (parameters, obj) => obj is object[] signature ? SetSignatureAndQuantities(parameters, signature) : parameters
        );

        private static AttributeProperty<DerivedUnitAttributeParameters> Expression { get; } = new
        (
            name: nameof(DerivedUnitAttribute.Expression),
            setter: static (parameters, obj) => obj is string expression ? parameters with { Expression = expression } : parameters
        );

        private static DerivedUnitAttributeParameters SetSignatureAndQuantities(DerivedUnitAttributeParameters parameters, object[] signature)
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
                else if (symbol.GetAttributeOfType<GeneratedBiasedUnitAttribute>() is AttributeData biasedUnitData)
                {
                    if (GeneratedBiasedUnitAttributeParameters.Parse(biasedUnitData) is GeneratedBiasedUnitAttributeParameters biasedUnitParameters)
                    {
                        if (biasedUnitParameters.UnbiasedQuantity is INamedTypeSymbol unbiasedUnitQuantity)
                        {
                            quantities[i] = unbiasedUnitQuantity;
                        }
                        else if (biasedUnitParameters.BiasedQuantity is INamedTypeSymbol biasedUnitQuantity)
                        {
                            quantities[i] = biasedUnitQuantity;
                        }
                    }
                }
            }

            return parameters with { Signature = units, Quantities = Array.AsReadOnly(quantities) };
        }
    }
}
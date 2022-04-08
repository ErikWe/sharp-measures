namespace SharpMeasures.SourceGenerators.Units.Attributes;

using SharpMeasures.Attributes;
using SharpMeasures.SourceGenerators.Utility;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.Linq;

internal readonly record struct DerivedUnitAttributeParameters((INamedTypeSymbol Unit, INamedTypeSymbol Quantity)[] Signature, string Expression)
{
    private static Dictionary<string, AttributeProperty<DerivedUnitAttributeParameters>> ConstructorArguments { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<DerivedUnitAttributeParameters>> NamedArguments { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<DerivedUnitAttributeParameters>> AllProperties => new()
        {
            Signature,
            Expression
        };

        public static AttributeProperty<DerivedUnitAttributeParameters> Signature { get; } = new("Signature", typeof(INamedTypeSymbol),
            static (parameters, obj) => obj is object[] signature ? parameters with { Signature = ExtractSignatureQuantities(signature) } : parameters);

        public static AttributeProperty<DerivedUnitAttributeParameters> Expression { get; } = new("Expression", typeof(string),
            static (parameters, obj) => obj is string expression ? parameters with { Expression = expression } : parameters);

        private static (INamedTypeSymbol Unit, INamedTypeSymbol Quantity)[] ExtractSignatureQuantities(object[] signature)
        {
            if (signature is null)
            {
                return Array.Empty<(INamedTypeSymbol Unit, INamedTypeSymbol Quantity)>();
            }

            (INamedTypeSymbol Unit, INamedTypeSymbol Quantity)[] combinedSignature = new (INamedTypeSymbol Unit, INamedTypeSymbol Quantity)[signature.Length];

            for (int i = 0; i < signature.Length; i++)
            {
                if (signature[i] is not INamedTypeSymbol symbol)
                {
                    continue;
                }

                if (symbol.GetAttributeOfType<UnitAttribute>() is AttributeData unitData)
                {
                    if (UnitAttributeParameters.Parse(unitData) is UnitAttributeParameters { Quantity: INamedTypeSymbol unitQuantity })
                    {
                        combinedSignature[i] = (symbol, unitQuantity);
                    }
                }
                else if (symbol.GetAttributeOfType<BiasedUnitAttribute>() is AttributeData biasedUnitData)
                {
                    if (UnitAttributeParameters.Parse(biasedUnitData) is UnitAttributeParameters { Quantity: INamedTypeSymbol unbiasedUnitQuantity })
                    {
                        combinedSignature[i] = (symbol, unbiasedUnitQuantity);
                    }
                    else if (UnitAttributeParameters.Parse(biasedUnitData) is UnitAttributeParameters { BiasedQuantity: INamedTypeSymbol biasedUnitQuantity })
                    {
                        combinedSignature[i] = (symbol, biasedUnitQuantity);
                    }
                }
            }

            return combinedSignature;
        }
    }

    public static DerivedUnitAttributeParameters? Parse(AttributeData attributeData)
    {
        DerivedUnitAttributeParameters values = new(Array.Empty<(INamedTypeSymbol Unit, INamedTypeSymbol Quantity)>(), string.Empty);

        (bool success, values) = AttributeHelpers.ParseAttributeParameters(attributeData, values, ConstructorArguments, NamedArguments);

        return success ? values : null;
    }
}
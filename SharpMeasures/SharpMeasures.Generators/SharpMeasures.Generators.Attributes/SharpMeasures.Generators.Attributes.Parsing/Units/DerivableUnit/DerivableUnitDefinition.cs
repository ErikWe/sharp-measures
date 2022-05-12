namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

public record class DerivableUnitDefinition(string Expression, IReadOnlyList<INamedTypeSymbol> Signature, IReadOnlyList<INamedTypeSymbol> Quantities,
    DerivableUnitLocations Locations, DerivableUnitParsingData ParsingData)
{
    public CacheableDerivableUnitDefinition ToCacheable() => CacheableDerivableUnitDefinition.Construct(this);

    internal DerivableUnitDefinition ParseSignature(INamedTypeSymbol[] signature)
    {
        if (signature is null)
        {
            throw new ArgumentNullException(nameof(signature));
        }

        if (signature.Length is 0)
        {
            return this with
            {
                Signature = signature,
                ParsingData = ParsingData with { SignatureValid = true }
            };
        }

        INamedTypeSymbol[] quantities = new INamedTypeSymbol[signature.Length];

        for (int i = 0; i < quantities.Length; i++)
        {
            if (signature[i].GetAttributeOfType<GeneratedUnitAttribute>() is not AttributeData unitData)
            {
                return this with
                {
                    Signature = signature,
                    ParsingData = ParsingData with { SignatureValid = true, SignatureComponentNotUnitIndex = i }
                };
            }

            if (GeneratedUnitParser.Parser.Parse(unitData) is not GeneratedUnitDefinition { Quantity: INamedTypeSymbol unitQuantity })
            {
                return this with
                {
                    Signature = signature,
                    ParsingData = ParsingData with { SignatureValid = true, SignatureComponentNotUnitIndex = i }
                };
            }
                    
            quantities[i] = unitQuantity;
        }

        return this with
        {
            Signature = signature,
            Quantities = quantities,
            ParsingData = ParsingData with { SignatureValid = true }
        };
    }
}
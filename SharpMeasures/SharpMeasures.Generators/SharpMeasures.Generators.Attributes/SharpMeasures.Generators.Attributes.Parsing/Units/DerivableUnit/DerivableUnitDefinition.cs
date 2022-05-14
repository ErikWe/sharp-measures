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
        DerivableUnitDefinition definition = this with { ParsingData = ParsingData with { SignatureCouldBeParsed = true } };

        if (signature is null)
        {
            throw new ArgumentNullException(nameof(signature));
        }

        if (signature.Length is 0)
        {
            return definition with { Signature = signature };
        }

        INamedTypeSymbol[] quantities = new INamedTypeSymbol[signature.Length];

        for (int i = 0; i < quantities.Length; i++)
        {
            if (signature[i].GetAttributeOfType<GeneratedUnitAttribute>() is not AttributeData unitData)
            {
                return definition with
                {
                    Signature = signature,
                    ParsingData = ParsingData with { SignatureComponentNotUnitIndex = i }
                };
            }

            if (GeneratedUnitParser.Parser.Parse(unitData) is not GeneratedUnitDefinition { Quantity: INamedTypeSymbol unitQuantity })
            {
                return definition with
                {
                    Signature = signature,
                    ParsingData = ParsingData with { SignatureComponentNotUnitIndex = i }
                };
            }
                    
            quantities[i] = unitQuantity;
        }

        return definition with
        {
            Signature = signature,
            Quantities = quantities
        };
    }
}
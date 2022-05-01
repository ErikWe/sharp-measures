namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

public class CubableQuantityParser : AArgumentParser<CubableQuantityParameters>
{
    public static CubableQuantityParser Parser { get; } = new();

    public static int QuantityIndex(AttributeData attributeData) => IndexOfArgument(CubableQuantityProperties.Quantity, attributeData);
    public static int SecondaryQuantitiesIndex(AttributeData attributeData) => IndexOfArgument(CubableQuantityProperties.SecondaryQuantities, attributeData);

    protected CubableQuantityParser() : base(DefaultParameters, CubableQuantityProperties.AllProperties) { }

    public override IEnumerable<CubableQuantityParameters> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<CubableQuantityAttribute>(typeSymbol);
    }

    public CubableQuantityParameters? ParseFirst(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        if (typeSymbol.GetAttributeOfType<CubableQuantityAttribute>() is AttributeData attributeData)
        {
            return Parse(attributeData);
        }

        return null;
    }

    private static CubableQuantityParameters DefaultParameters() => new
    (
        Quantity: null,
        SecondaryQuantities: Array.AsReadOnly(Array.Empty<INamedTypeSymbol>())
    );
}

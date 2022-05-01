namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

public class SquarableQuantityParser : AArgumentParser<SquarableQuantityParameters>
{
    public static SquarableQuantityParser Parser { get; } = new();

    public static int QuantityIndex(AttributeData attributeData) => IndexOfArgument(SquarableQuantityProperties.Quantity, attributeData);
    public static int SecondaryQuantitiesIndex(AttributeData attributeData) => IndexOfArgument(SquarableQuantityProperties.SecondaryQuantities, attributeData);

    protected SquarableQuantityParser() : base(DefaultParameters, SquarableQuantityProperties.AllProperties) { }

    public override IEnumerable<SquarableQuantityParameters> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<SquarableQuantityAttribute>(typeSymbol);
    }

    public SquarableQuantityParameters? ParseFirst(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        if (typeSymbol.GetAttributeOfType<SquarableQuantityAttribute>() is AttributeData attributeData)
        {
            return Parse(attributeData);
        }

        return null;
    }

    private static SquarableQuantityParameters DefaultParameters()  => new
    (
        Quantity: null,
        SecondaryQuantities: Array.AsReadOnly(Array.Empty<INamedTypeSymbol>())
    );
}

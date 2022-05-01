namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

public class SquareRootableQuantityParser : AArgumentParser<SquareRootableQuantityParameters>
{
    public static SquareRootableQuantityParser Parser { get; } = new();

    public static int QuantityIndex(AttributeData attributeData) => IndexOfArgument(SquareRootableQuantityProperties.Quantity, attributeData);
    public static int SecondaryQuantitiesIndex(AttributeData attributeData) => IndexOfArgument(SquareRootableQuantityProperties.SecondaryQuantities, attributeData);

    protected SquareRootableQuantityParser() : base(DefaultParameters, SquareRootableQuantityProperties.AllProperties) { }

    public override IEnumerable<SquareRootableQuantityParameters> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<SquareRootableQuantityAttribute>(typeSymbol);
    }

    public SquareRootableQuantityParameters? ParseFirst(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        if (typeSymbol.GetAttributeOfType<SquareRootableQuantityAttribute>() is AttributeData attributeData)
        {
            return Parse(attributeData);
        }

        return null;
    }

    private static SquareRootableQuantityParameters DefaultParameters()  => new
    (
        Quantity: null,
        SecondaryQuantities: Array.AsReadOnly(Array.Empty<INamedTypeSymbol>())
    );
}

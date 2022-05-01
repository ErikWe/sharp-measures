namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

public class CubeRootableQuantityParser : AArgumentParser<CubeRootableQuantityParameters>
{
    public static CubeRootableQuantityParser Parser { get; } = new();

    public static int QuantityIndex(AttributeData attributeData) => IndexOfArgument(CubeRootableQuantityProperties.Quantity, attributeData);
    public static int SecondaryQuantitiesIndex(AttributeData attributeData) => IndexOfArgument(CubeRootableQuantityProperties.SecondaryQuantities, attributeData);

    protected CubeRootableQuantityParser() : base(DefaultParameters, CubeRootableQuantityProperties.AllProperties) { }

    public override IEnumerable<CubeRootableQuantityParameters> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<CubeRootableQuantityAttribute>(typeSymbol);
    }

    public CubeRootableQuantityParameters? ParseFirst(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        if (typeSymbol.GetAttributeOfType<CubeRootableQuantityAttribute>() is AttributeData attributeData)
        {
            return Parse(attributeData);
        }

        return null;
    }

    private static CubeRootableQuantityParameters DefaultParameters() => new
    (
        Quantity: null,
        SecondaryQuantities: Array.AsReadOnly(Array.Empty<INamedTypeSymbol>())
    );
}

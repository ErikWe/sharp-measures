namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

public class InvertibleQuantityParser : AArgumentParser<InvertibleQuantityParameters>
{
    public static InvertibleQuantityParser Parser { get; } = new();

    public static int QuantityIndex(AttributeData attributeData) => IndexOfArgument(InvertibleQuantityProperties.Quantity, attributeData);
    public static int SecondaryQuantitiesIndex(AttributeData attributeData) => IndexOfArgument(InvertibleQuantityProperties.SecondaryQuantities, attributeData);

    protected InvertibleQuantityParser() : base(DefaultParameters, InvertibleQuantityProperties.AllProperties) { }

    public override IEnumerable<InvertibleQuantityParameters> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<InvertibleQuantityAttribute>(typeSymbol);
    }

    public InvertibleQuantityParameters? ParseFirst(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        if (typeSymbol.GetAttributeOfType<InvertibleQuantityAttribute>() is AttributeData attributeData)
        {
            return Parse(attributeData);
        }

        return null;
    }

    private static InvertibleQuantityParameters DefaultParameters()  => new
    (
        Quantity: null,
        SecondaryQuantities: Array.AsReadOnly(Array.Empty<INamedTypeSymbol>())
    );
}

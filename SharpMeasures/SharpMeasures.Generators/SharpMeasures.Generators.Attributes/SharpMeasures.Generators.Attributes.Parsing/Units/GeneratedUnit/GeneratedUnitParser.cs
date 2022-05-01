namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

public class GeneratedUnitParser : AArgumentParser<GeneratedUnitParameters>
{
    public static GeneratedUnitParser Parser { get; } = new();

    public static int QuantityIndex(AttributeData attributeData) => IndexOfArgument(GeneratedUnitProperties.Quantity, attributeData);
    public static int AllowBiasIndex(AttributeData attributeData) => IndexOfArgument(GeneratedUnitProperties.AllowBias, attributeData);

    protected GeneratedUnitParser() : base(DefaultParameters, GeneratedUnitProperties.AllProperties) { }

    public override IEnumerable<GeneratedUnitParameters> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<GeneratedUnitAttribute>(typeSymbol);
    }

    public GeneratedUnitParameters? ParseFirst(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        if (typeSymbol.GetAttributeOfType<GeneratedUnitAttribute>() is AttributeData attributeData)
        {
            return Parse(attributeData);
        }

        return null;
    }

    private static GeneratedUnitParameters DefaultParameters() => new
    (
        Quantity: null,
        AllowBias: false
    );
}

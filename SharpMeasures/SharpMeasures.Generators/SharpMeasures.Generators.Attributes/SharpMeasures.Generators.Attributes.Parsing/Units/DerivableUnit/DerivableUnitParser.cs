namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

public class DerivableUnitParser : AArgumentParser<DerivableUnitParameters>
{
    public static DerivableUnitParser Parser { get; } = new();

    public static int SignatureIndex(AttributeData attributeData) => IndexOfArgument(DerivableUnitProperties.Signature, attributeData);
    public static int ExpressionIndex(AttributeData attributeData) => IndexOfArgument(DerivableUnitProperties.Expression, attributeData);

    protected DerivableUnitParser() : base(DefaultParameters, DerivableUnitProperties.AllProperties) { }

    public override IEnumerable<DerivableUnitParameters> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<DerivableUnitAttribute>(typeSymbol);
    }

    private static DerivableUnitParameters DefaultParameters() => new
    (
        Signature: Array.AsReadOnly(Array.Empty<INamedTypeSymbol>()),
        Quantities: Array.AsReadOnly(Array.Empty<INamedTypeSymbol>()),
        Expression: string.Empty,
        ParsingData: new DerivableUnitParsingData()
    );
}

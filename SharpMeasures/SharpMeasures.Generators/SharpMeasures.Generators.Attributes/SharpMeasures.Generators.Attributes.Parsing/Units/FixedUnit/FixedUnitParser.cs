namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

public class FixedUnitParser : AArgumentParser<FixedUnitParameters>
{
    public static FixedUnitParser Parser { get; } = new();

    public static int NameIndex(AttributeData attributeData) => IndexOfArgument(FixedUnitProperties.Name, attributeData);
    public static int PluralIndex(AttributeData attributeData) => IndexOfArgument(FixedUnitProperties.Plural, attributeData);
    public static int ValueIndex(AttributeData attributeData) => IndexOfArgument(FixedUnitProperties.Value, attributeData);
    public static int BiasIndex(AttributeData attributeData) => IndexOfArgument(FixedUnitProperties.Bias, attributeData);

    protected FixedUnitParser() : base(DefaultParameters, FixedUnitProperties.AllProperties) { }

    public override IEnumerable<FixedUnitParameters> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<FixedUnitAttribute>(typeSymbol);
    }

    private static FixedUnitParameters DefaultParameters() => new
    (
        Name: string.Empty,
        Plural: string.Empty,
        Value: 0,
        Bias: 0
    );
}

namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

public class OffsetUnitParser : AArgumentParser<OffsetUnitParameters>
{
    public static OffsetUnitParser Parser { get; } = new();

    public static int NameIndex(AttributeData attributeData) => IndexOfArgument(OffsetUnitProperties.Name, attributeData);
    public static int PluralIndex(AttributeData attributeData) => IndexOfArgument(OffsetUnitProperties.Plural, attributeData);
    public static int FromIndex(AttributeData attributeData) => IndexOfArgument(OffsetUnitProperties.From, attributeData);
    public static int OffsetIndex(AttributeData attributeData) => IndexOfArgument(OffsetUnitProperties.Offset, attributeData);

    protected OffsetUnitParser() : base(DefaultParameters, OffsetUnitProperties.AllProperties) { }

    public override IEnumerable<OffsetUnitParameters> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<OffsetUnitAttribute>(typeSymbol);
    }

    private static OffsetUnitParameters DefaultParameters() => new
    (
        Name: string.Empty,
        Plural: string.Empty,
        From: string.Empty,
        Offset: 0
    );
}

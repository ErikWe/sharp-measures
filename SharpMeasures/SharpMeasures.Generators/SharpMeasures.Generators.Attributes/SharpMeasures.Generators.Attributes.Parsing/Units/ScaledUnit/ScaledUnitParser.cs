namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

public class ScaledUnitParser : AArgumentParser<ScaledUnitParameters>
{
    public static ScaledUnitParser Parser { get; } = new();

    public static int NameIndex(AttributeData attributeData) => IndexOfArgument(ScaledUnitProperties.Name, attributeData);
    public static int PluralIndex(AttributeData attributeData) => IndexOfArgument(ScaledUnitProperties.Plural, attributeData);
    public static int FromIndex(AttributeData attributeData) => IndexOfArgument(ScaledUnitProperties.From, attributeData);
    public static int ScaleIndex(AttributeData attributeData) => IndexOfArgument(ScaledUnitProperties.Scale, attributeData);

    protected ScaledUnitParser() : base(DefaultParameters, ScaledUnitProperties.AllProperties) { }

    public override IEnumerable<ScaledUnitParameters> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<ScaledUnitAttribute>(typeSymbol);
    }

    private static ScaledUnitParameters DefaultParameters() => new
    (
        Name: string.Empty,
        Plural: string.Empty,
        From: string.Empty,
        Scale: 0
    );
}

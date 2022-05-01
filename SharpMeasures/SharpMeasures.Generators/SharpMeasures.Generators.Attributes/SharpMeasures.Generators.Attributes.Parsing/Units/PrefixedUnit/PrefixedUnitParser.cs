namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

public class PrefixedUnitParser : AArgumentParser<PrefixedUnitParameters>
{
    public static PrefixedUnitParser Parser { get; } = new();

    public static int NameIndex(AttributeData attributeData) => IndexOfArgument(PrefixedUnitProperties.Name, attributeData);
    public static int PluralIndex(AttributeData attributeData) => IndexOfArgument(PrefixedUnitProperties.Plural, attributeData);
    public static int FromIndex(AttributeData attributeData) => IndexOfArgument(PrefixedUnitProperties.From, attributeData);
    public static int MetricPrefixNameIndex(AttributeData attributeData) => IndexOfArgument(PrefixedUnitProperties.MetricPrefixName, attributeData);
    public static int BinaryPrefixNameindex(AttributeData attributeData) => IndexOfArgument(PrefixedUnitProperties.BinaryPrefixName, attributeData);

    protected PrefixedUnitParser() : base(DefaultParameters, PrefixedUnitProperties.AllProperties) { }

    public override IEnumerable<PrefixedUnitParameters> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<PrefixedUnitAttribute>(typeSymbol);
    }

    private static PrefixedUnitParameters DefaultParameters() => new
    (
        Name: string.Empty,
        Plural: string.Empty,
        From: string.Empty,
        MetricPrefixName: MetricPrefixName.Identity,
        BinaryPrefixName: BinaryPrefixName.Identity,
        SpecifiedPrefixType: PrefixedUnitParameters.PrefixType.None
    );
}

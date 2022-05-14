namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

public class PrefixedUnitParser : AArgumentParser<PrefixedUnitDefinition>
{
    public static PrefixedUnitParser Parser { get; } = new();

    protected PrefixedUnitParser() : base(DefaultParameters, PrefixedUnitProperties.AllProperties) { }

    public override IEnumerable<PrefixedUnitDefinition> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<PrefixedUnitAttribute>(typeSymbol);
    }

    private protected override PrefixedUnitDefinition AddCustomData(PrefixedUnitDefinition definition, AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        return definition with { Locations = definition.Locations.LocateBase(attributeSyntax) };
    }

    private static PrefixedUnitDefinition DefaultParameters() => new
    (
        Name: string.Empty,
        Plural: string.Empty,
        From: string.Empty,
        MetricPrefixName: MetricPrefixName.Identity,
        BinaryPrefixName: BinaryPrefixName.Identity,
        Locations: new PrefixedUnitLocations(),
        ParsingData: new PrefixedUnitParsingData()
    );
}

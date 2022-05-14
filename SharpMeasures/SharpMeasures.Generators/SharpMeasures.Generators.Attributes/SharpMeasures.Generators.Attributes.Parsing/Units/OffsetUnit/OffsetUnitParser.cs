namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

public class OffsetUnitParser : AArgumentParser<OffsetUnitDefinition>
{
    public static OffsetUnitParser Parser { get; } = new();

     protected OffsetUnitParser() : base(DefaultParameters, OffsetUnitProperties.AllProperties) { }

    public override IEnumerable<OffsetUnitDefinition> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<OffsetUnitAttribute>(typeSymbol);
    }

    private protected override OffsetUnitDefinition AddCustomData(OffsetUnitDefinition definition, AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        return definition with { Locations = definition.Locations.LocateBase(attributeSyntax) };
    }

    private static OffsetUnitDefinition DefaultParameters() => new
    (
        Name: string.Empty,
        Plural: string.Empty,
        From: string.Empty,
        Offset: 0,
        Locations : new OffsetUnitLocations()
    );
}

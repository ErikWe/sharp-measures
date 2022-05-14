namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

public class FixedUnitParser : AArgumentParser<FixedUnitDefinition>
{
    public static FixedUnitParser Parser { get; } = new();

    protected FixedUnitParser() : base(DefaultParameters, FixedUnitProperties.AllProperties) { }

    public override IEnumerable<FixedUnitDefinition> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<FixedUnitAttribute>(typeSymbol);
    }

    private protected override FixedUnitDefinition AddCustomData(FixedUnitDefinition definition, AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        return definition with { Locations = definition.Locations.LocateBase(attributeSyntax) };
    }

    private static FixedUnitDefinition DefaultParameters() => new
    (
        Name: string.Empty,
        Plural: string.Empty,
        Value: 0,
        Bias: 0,
        Locations: new FixedUnitLocations()
    );
}

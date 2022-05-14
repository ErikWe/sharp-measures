namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

public class ScaledUnitParser : AArgumentParser<ScaledUnitDefinition>
{
    public static ScaledUnitParser Parser { get; } = new();

    protected ScaledUnitParser() : base(DefaultParameters, ScaledUnitProperties.AllProperties) { }

    public override IEnumerable<ScaledUnitDefinition> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<ScaledUnitAttribute>(typeSymbol);
    }

    private protected override ScaledUnitDefinition AddCustomData(ScaledUnitDefinition definition, AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        return definition with { Locations = definition.Locations.LocateBase(attributeSyntax) };
    }

    private static ScaledUnitDefinition DefaultParameters() => new
    (
        Name: string.Empty,
        Plural: string.Empty,
        From: string.Empty,
        Scale: 0,
        Locations: new ScaledUnitLocations()
    );
}

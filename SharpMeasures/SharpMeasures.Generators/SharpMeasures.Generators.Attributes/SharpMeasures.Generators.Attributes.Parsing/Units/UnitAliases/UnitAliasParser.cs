namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

public class UnitAliasParser : AArgumentParser<UnitAliasDefinition>
{
    public static UnitAliasParser Parser { get; } = new();

    protected UnitAliasParser() : base(DefaultParameters, UnitAliasProperties.AllProperties) { }

    public override IEnumerable<UnitAliasDefinition> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<UnitAliasAttribute>(typeSymbol);
    }

    private protected override UnitAliasDefinition AddCustomData(UnitAliasDefinition definition, AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        return definition with { Locations = definition.Locations.LocateBase(attributeSyntax) };
    }

    private static UnitAliasDefinition DefaultParameters() => new
    (
        Name: string.Empty,
        Plural: string.Empty,
        AliasOf: string.Empty,
        Locations: new UnitAliasLocations()
    );
}

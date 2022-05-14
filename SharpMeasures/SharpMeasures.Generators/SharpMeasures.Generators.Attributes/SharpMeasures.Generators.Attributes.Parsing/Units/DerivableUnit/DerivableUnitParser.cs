namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

public class DerivableUnitParser : AArgumentParser<DerivableUnitDefinition>
{
    public static DerivableUnitParser Parser { get; } = new();

    protected DerivableUnitParser() : base(DefaultParameters, DerivableUnitProperties.AllProperties) { }

    public override IEnumerable<DerivableUnitDefinition> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<DerivableUnitAttribute>(typeSymbol);
    }

    private protected override DerivableUnitDefinition AddCustomData(DerivableUnitDefinition definition, AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        return definition with { Locations = definition.Locations.LocateBase(attributeSyntax) };
    }

    private static DerivableUnitDefinition DefaultParameters() => new
    (
        Signature: Array.Empty<INamedTypeSymbol>(),
        Quantities: Array.Empty<INamedTypeSymbol>(),
        Expression: string.Empty,
        Locations: new DerivableUnitLocations(),
        ParsingData: new DerivableUnitParsingData()
    );
}

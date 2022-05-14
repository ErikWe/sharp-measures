namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

public class DerivedUnitParser : AArgumentParser<DerivedUnitDefinition>
{
    public static DerivedUnitParser Parser { get; } = new();

    protected DerivedUnitParser() : base(DefaultParameters, DerivedUnitProperties.AllProperties) { }

    public override IEnumerable<DerivedUnitDefinition> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<DerivableUnitAttribute>(typeSymbol);
    }

    private protected override DerivedUnitDefinition AddCustomData(DerivedUnitDefinition definition, AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        return definition with { Locations = definition.Locations.LocateBase(attributeSyntax) };
    }

    private static DerivedUnitDefinition DefaultParameters() => new
    (
        Name: string.Empty,
        Plural: string.Empty,
        Signature: Array.AsReadOnly(Array.Empty<INamedTypeSymbol>()),
        Units: Array.AsReadOnly(Array.Empty<string>()),
        Locations: new DerivedUnitLocations(),
        ParsingData: new DerivedUnitParsingData()
    );
}

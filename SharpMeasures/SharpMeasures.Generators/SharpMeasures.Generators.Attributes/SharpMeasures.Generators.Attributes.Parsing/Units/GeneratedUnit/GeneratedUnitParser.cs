namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

public class GeneratedUnitParser : AArgumentParser<GeneratedUnitDefinition>
{
    public static GeneratedUnitParser Parser { get; } = new();

    public static int QuantityIndex(AttributeData attributeData) => IndexOfArgument(GeneratedUnitProperties.Quantity, attributeData);
    public static int AllowBiasIndex(AttributeData attributeData) => IndexOfArgument(GeneratedUnitProperties.AllowBias, attributeData);
    public static int GenerateDocumentationIndex(AttributeData attributeData) => IndexOfArgument(GeneratedUnitProperties.GenerateDocumentation, attributeData);

    protected GeneratedUnitParser() : base(DefaultParameters, GeneratedUnitProperties.AllProperties) { }

    public override IEnumerable<GeneratedUnitDefinition> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<GeneratedUnitAttribute>(typeSymbol);
    }

    public GeneratedUnitDefinition? ParseFirst(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        if (typeSymbol.GetAttributeOfType<GeneratedUnitAttribute>() is AttributeData attributeData)
        {
            return Parse(attributeData);
        }

        return null;
    }

    private protected override GeneratedUnitDefinition AddCustomData(GeneratedUnitDefinition definition, AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        return definition with { Locations = definition.Locations.LocateBase(attributeSyntax) };
    }

    private static GeneratedUnitDefinition DefaultParameters() => new
    (
        Quantity: null,
        AllowBias: false,
        GenerateDocumentation: false,
        Locations: new GeneratedUnitLocations(),
        ParsingData: new GeneratedUnitParsingData()
    );
}

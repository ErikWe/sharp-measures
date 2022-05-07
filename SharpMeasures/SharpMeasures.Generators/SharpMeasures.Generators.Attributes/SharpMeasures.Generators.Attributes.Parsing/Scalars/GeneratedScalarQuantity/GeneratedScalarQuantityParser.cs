namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

public class GeneratedScalarQuantityParser : AArgumentParser<GeneratedScalarQuantityParameters>
{
    public static GeneratedScalarQuantityParser Parser { get; } = new();

    public static int UnitIndex(AttributeData attributeData) => IndexOfArgument(GeneratedScalarQuantityProperties.Unit, attributeData);
    public static int BiasedIndex(AttributeData attributeData) => IndexOfArgument(GeneratedScalarQuantityProperties.Biased, attributeData);

    protected GeneratedScalarQuantityParser() : base(DefaultParameters, GeneratedScalarQuantityProperties.AllProperties) { }

    public override IEnumerable<GeneratedScalarQuantityParameters> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<GeneratedScalarQuantityAttribute>(typeSymbol);
    }

    public GeneratedScalarQuantityParameters? ParseFirst(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        if (typeSymbol.GetAttributeOfType<GeneratedScalarQuantityAttribute>() is AttributeData attributeData)
        {
            return Parse(attributeData);
        }

        return null;
    }

    private static GeneratedScalarQuantityParameters DefaultParameters() => new
    (
        Unit: null,
        Biased: false,
        GenerateDocumentation: false,
        ParsingData: new GeneratedScalarQuantityParsingData()
    );
}

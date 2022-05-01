namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

public class IncludeBasesParser : AArgumentParser<IncludeBasesParameters>
{
    public static IncludeBasesParser Parser { get; } = new();

    public static int IncludedBasesIndex(AttributeData attributeData) => IndexOfArgument(IncludeBasesProperties.IncludedBases, attributeData);

    protected IncludeBasesParser() : base(DefaultParameters, IncludeBasesProperties.AllProperties) { }

    public override IEnumerable<IncludeBasesParameters> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<IncludeBasesAttribute>(typeSymbol);
    }

    public IncludeBasesParameters? ParseFirst(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        if (typeSymbol.GetAttributeOfType<IncludeBasesAttribute>() is AttributeData attributeData)
        {
            return Parse(attributeData);
        }

        return null;
    }

    private static IncludeBasesParameters DefaultParameters()  => new
    (
        IncludedBases: Array.AsReadOnly(Array.Empty<string>())
    );
}

namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

public class ExcludeBasesParser : AArgumentParser<ExcludeBasesParameters>
{
    public static ExcludeBasesParser Parser { get; } = new();

    public static int ExcludedBasesIndex(AttributeData attributeData) => IndexOfArgument(ExcludeBasesProperties.ExcludedBases, attributeData);

    protected ExcludeBasesParser() : base(DefaultParameters, ExcludeBasesProperties.AllProperties) { }

    public override IEnumerable<ExcludeBasesParameters> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<ExcludeBasesAttribute>(typeSymbol);
    }

    public ExcludeBasesParameters? ParseFirst(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        if (typeSymbol.GetAttributeOfType<ExcludeBasesAttribute>() is AttributeData attributeData)
        {
            return Parse(attributeData);
        }

        return null;
    }

    private static ExcludeBasesParameters DefaultParameters()  => new
    (
        ExcludedBases: Array.AsReadOnly(Array.Empty<string>())
    );
}

namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

public class IncludeUnitsParser : AArgumentParser<IncludeUnitsParameters>
{
    public static IncludeUnitsParser Parser { get; } = new();

    public static int IncludedUnitsIndex(AttributeData attributeData) => IndexOfArgument(IncludeUnitsProperties.IncludedUnits, attributeData);

    protected IncludeUnitsParser() : base(DefaultParameters, IncludeUnitsProperties.AllProperties) { }

    public override IEnumerable<IncludeUnitsParameters> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<IncludeUnitsAttribute>(typeSymbol);
    }

    public IncludeUnitsParameters? ParseFirst(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        if (typeSymbol.GetAttributeOfType<IncludeUnitsAttribute>() is AttributeData attributeData)
        {
            return Parse(attributeData);
        }

        return null;
    }

    private static IncludeUnitsParameters DefaultParameters()  => new
    (
        IncludedUnits: Array.AsReadOnly(Array.Empty<string>())
    );
}

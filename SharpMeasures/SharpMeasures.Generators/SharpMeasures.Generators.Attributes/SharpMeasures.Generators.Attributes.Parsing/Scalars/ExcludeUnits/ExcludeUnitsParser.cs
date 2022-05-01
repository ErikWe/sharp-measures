namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

public class ExcludeUnitsParser : AArgumentParser<ExcludeUnitsParameters>
{
    public static ExcludeUnitsParser Parser { get; } = new();

    public static int ExcludedUnitsIndex(AttributeData attributeData) => IndexOfArgument(ExcludeUnitsProperties.ExcludedUnits, attributeData);

    protected ExcludeUnitsParser() : base(DefaultParameters, ExcludeUnitsProperties.AllProperties) { }

    public override IEnumerable<ExcludeUnitsParameters> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<ExcludeUnitsAttribute>(typeSymbol);
    }

    public ExcludeUnitsParameters? ParseFirst(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        if (typeSymbol.GetAttributeOfType<ExcludeUnitsAttribute>() is AttributeData attributeData)
        {
            return Parse(attributeData);
        }

        return null;
    }

    private static ExcludeUnitsParameters DefaultParameters()  => new
    (
        ExcludedUnits: Array.AsReadOnly(Array.Empty<string>())
    );
}

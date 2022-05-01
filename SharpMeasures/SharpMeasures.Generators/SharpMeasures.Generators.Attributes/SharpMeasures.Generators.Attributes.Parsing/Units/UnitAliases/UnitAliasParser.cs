namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

public class UnitAliasParser : AArgumentParser<UnitAliasParameters>
{
    public static UnitAliasParser Parser { get; } = new();

    public static int NameIndex(AttributeData attributeData) => IndexOfArgument(UnitAliasProperties.Name, attributeData);
    public static int PluralIndex(AttributeData attributeData) => IndexOfArgument(UnitAliasProperties.Plural, attributeData);
    public static int AliasOfIndex(AttributeData attributeData) => IndexOfArgument(UnitAliasProperties.AliasOf, attributeData);

    protected UnitAliasParser() : base(DefaultParameters, UnitAliasProperties.AllProperties) { }

    public override IEnumerable<UnitAliasParameters> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<UnitAliasAttribute>(typeSymbol);
    }

    private static UnitAliasParameters DefaultParameters() => new
    (
        Name: string.Empty,
        Plural: string.Empty,
        AliasOf: string.Empty
    );
}

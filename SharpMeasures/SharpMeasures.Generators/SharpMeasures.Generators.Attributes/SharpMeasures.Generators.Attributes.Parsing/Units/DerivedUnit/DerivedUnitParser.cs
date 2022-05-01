namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

public class DerivedUnitParser : AArgumentParser<DerivedUnitParameters>
{
    public static DerivedUnitParser Parser { get; } = new();

    public static int NameIndex(AttributeData attributeData) => IndexOfArgument(DerivedUnitProperties.Name, attributeData);
    public static int PluralIndex(AttributeData attributeData) => IndexOfArgument(DerivedUnitProperties.Plural, attributeData);
    public static int SignatureIndex(AttributeData attributeData) => IndexOfArgument(DerivedUnitProperties.Signature, attributeData);
    public static int UnitsIndex(AttributeData attributeData) => IndexOfArgument(DerivedUnitProperties.Units, attributeData);

    protected DerivedUnitParser() : base(DefaultParameters, DerivedUnitProperties.AllProperties) { }

    public override IEnumerable<DerivedUnitParameters> Parse(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse<DerivableUnitAttribute>(typeSymbol);
    }

    private static DerivedUnitParameters DefaultParameters() => new
    (
        Name: string.Empty,
        Plural: string.Empty,
        Signature: Array.AsReadOnly(Array.Empty<INamedTypeSymbol>()),
        Units: Array.AsReadOnly(Array.Empty<string>()),
        ParsingData: new DerivedUnitParsingData()
    );
}

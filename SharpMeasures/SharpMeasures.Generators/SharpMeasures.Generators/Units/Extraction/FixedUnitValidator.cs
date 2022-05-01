namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class FixedUnitValidator : UnitDefinitionValidator<FixedUnitParameters>
{
    public static FixedUnitValidator Validator { get; } = new();

    private FixedUnitValidator() { }

    protected override int NameArgumentIndex(AttributeData attributeData) => FixedUnitParser.NameIndex(attributeData);
    protected override int PluralArgumentIndex(AttributeData attributeData) => FixedUnitParser.PluralIndex(attributeData);
}

namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class OffsetUnitValidator : DependantUnitDefinitionValidator<OffsetUnitParameters>
{
    public OffsetUnitValidator(INamedTypeSymbol unitType) : base(unitType) { }

    protected override int NameArgumentIndex(AttributeData attributeData) => OffsetUnitParser.NameIndex(attributeData);
    protected override int PluralArgumentIndex(AttributeData attributeData) => OffsetUnitParser.PluralIndex(attributeData);
    protected override int DependantOnArgumentIndex(AttributeData attributeData) => OffsetUnitParser.FromIndex(attributeData);
}

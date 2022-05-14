namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class OffsetUnitValidator : DependantUnitDefinitionValidator<OffsetUnitDefinition>
{
    public OffsetUnitValidator(INamedTypeSymbol unitType) : base(unitType) { }
}

namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class UnitAliasValidator : DependantUnitDefinitionValidator<UnitAliasDefinition>
{
    public UnitAliasValidator(INamedTypeSymbol unitType) : base(unitType) { }
}

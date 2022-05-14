namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class ScaledUnitValidator : DependantUnitDefinitionValidator<ScaledUnitDefinition>
{
    public ScaledUnitValidator(INamedTypeSymbol unitType) : base(unitType) { }
}

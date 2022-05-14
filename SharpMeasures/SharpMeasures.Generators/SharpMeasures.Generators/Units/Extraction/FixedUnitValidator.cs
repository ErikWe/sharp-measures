namespace SharpMeasures.Generators.Units.Extraction;

using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class FixedUnitValidator : UnitDefinitionValidator<FixedUnitDefinition>
{
    public static FixedUnitValidator Validator { get; } = new();

    private FixedUnitValidator() { }
}

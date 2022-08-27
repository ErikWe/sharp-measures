namespace SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;

public interface IDefaultUnitValidationDiagnostics
{
    public abstract Diagnostic? UnrecognizedDefaultUnit(IProcessingContext context, IDefaultUnitDefinition definition, IUnitType unit);
}

public static class DefaultUnitValidator
{
    public static IValidityWithDiagnostics Validate(IProcessingContext context, IDefaultUnitValidationDiagnostics diagnostics, IDefaultUnitDefinition definition, IUnitPopulation unitPopulation, NamedType unit)
    {
        if (definition.DefaultUnitName is null)
        {
            return ValidityWithDiagnostics.Valid;
        }

        return RetrieveUnitType(unitPopulation, unit)
            .Validate((unitType) => ValidateUnitExists(context, diagnostics, definition, unitType))
            .Reduce();
    }

    private static IOptionalWithDiagnostics<IUnitType> RetrieveUnitType(IUnitPopulation unitPopulation, NamedType unit)
    {
        var unitCorrectlyResolved = unitPopulation.Units.TryGetValue(unit, out var unitType);

        return OptionalWithDiagnostics.ConditionalWithoutDiagnostics(unitCorrectlyResolved, unitType);
    }

    private static IValidityWithDiagnostics ValidateUnitExists(IProcessingContext context, IDefaultUnitValidationDiagnostics diagnostics, IDefaultUnitDefinition definition, IUnitType unitType)
    {
        var unitExists = unitType.UnitsByName.ContainsKey(definition.DefaultUnitName!);

        return ValidityWithDiagnostics.Conditional(unitExists, () => diagnostics.UnrecognizedDefaultUnit(context, definition, unitType));
    }
}

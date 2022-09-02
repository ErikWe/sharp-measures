namespace SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;

public interface IDefaultUnitInstanceValidationDiagnostics
{
    public abstract Diagnostic? UnrecognizedDefaultUnitInstance(IProcessingContext context, IDefaultUnitInstanceDefinition definition, IUnitType unit);
}

public static class DefaultUnitInstanceValidator
{
    public static IValidityWithDiagnostics Validate(IProcessingContext context, IDefaultUnitInstanceValidationDiagnostics diagnostics, IDefaultUnitInstanceDefinition definition, IUnitPopulation unitPopulation, NamedType unit)
    {
        if (definition.DefaultUnitInstanceName is null)
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

    private static IValidityWithDiagnostics ValidateUnitExists(IProcessingContext context, IDefaultUnitInstanceValidationDiagnostics diagnostics, IDefaultUnitInstanceDefinition definition, IUnitType unitType)
    {
        var unitInstanceExists = unitType.UnitInstancesByName.ContainsKey(definition.DefaultUnitInstanceName!);

        return ValidityWithDiagnostics.Conditional(unitInstanceExists, () => diagnostics.UnrecognizedDefaultUnitInstance(context, definition, unitType));
    }
}

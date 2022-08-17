namespace SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

public interface IDefaultUnitResolutionDiagnostics
{
    public abstract Diagnostic? UnrecognizedDefaultUnit(IProcessingContext context, IRawDefaultUnitDefinition definition, IRawUnitType unit);
}

public static class DefaultUnitResolver
{
    public static IResultWithDiagnostics<IRawUnitInstance?> Resolve(IProcessingContext context, IDefaultUnitResolutionDiagnostics diagnostics, IRawDefaultUnitDefinition definition, IRawUnitType unit)
    {
        if (definition.DefaultUnitName is null)
        {
            return ResultWithDiagnostics.Construct<IRawUnitInstance?>(null);
        }

        if (unit.UnitsByName.TryGetValue(definition.DefaultUnitName, out var unitInstance) is false)
        {
            return ResultWithDiagnostics.Construct<IRawUnitInstance?>(null, diagnostics.UnrecognizedDefaultUnit(context, definition, unit));
        }

        return ResultWithDiagnostics.Construct<IRawUnitInstance?>(unitInstance);
    }
}

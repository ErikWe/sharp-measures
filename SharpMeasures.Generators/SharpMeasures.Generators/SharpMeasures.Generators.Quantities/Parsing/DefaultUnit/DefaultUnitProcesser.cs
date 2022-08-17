namespace SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

public interface IDefaultUnitProcessingDiagnostics
{
    public abstract Diagnostic? NullDefaultUnit(IProcessingContext context, IRawDefaultUnitDefinition definition);
    public abstract Diagnostic? EmptyDefaultUnit(IProcessingContext context, IRawDefaultUnitDefinition definition);
    public abstract Diagnostic? SetDefaultSymbolButNotUnit(IProcessingContext context, IRawDefaultUnitDefinition definition);
    public abstract Diagnostic? SetDefaultUnitButNotSymbol(IProcessingContext context, IRawDefaultUnitDefinition definition);
}

public static class DefaultUnitProcesser
{
    public static IResultWithDiagnostics<(string? Name, string? Symbol)> Process(IProcessingContext context, IDefaultUnitProcessingDiagnostics diagnostics, IRawDefaultUnitDefinition definition)
    {
        if (definition.DefaultUnitLocations.ExplicitlySetDefaultUnitSymbol && definition.DefaultUnitLocations.ExplicitlySetDefaultUnitName is false)
        {
            return ResultWithDiagnostics.Construct<(string?, string?)>((null, null), diagnostics.SetDefaultSymbolButNotUnit(context, definition));
        }

        if (definition.DefaultUnitLocations.ExplicitlySetDefaultUnitName && definition.DefaultUnitLocations.ExplicitlySetDefaultUnitSymbol is false)
        {
            return ResultWithDiagnostics.Construct<(string?, string?)>((null, null), diagnostics.SetDefaultUnitButNotSymbol(context, definition));
        }

        if (definition.DefaultUnitLocations.ExplicitlySetDefaultUnitName)
        {
            if (definition.DefaultUnitName is null)
            {
                return ResultWithDiagnostics.Construct<(string?, string?)>((null, null), diagnostics.NullDefaultUnit(context, definition));
            }

            if (definition.DefaultUnitName.Length is 0)
            {
                return ResultWithDiagnostics.Construct<(string?, string?)>((null, null), diagnostics.EmptyDefaultUnit(context, definition));
            }
        }

        return ResultWithDiagnostics.Construct<(string?, string?)>((definition.DefaultUnitName, definition.DefaultUnitSymbol));
    }
}

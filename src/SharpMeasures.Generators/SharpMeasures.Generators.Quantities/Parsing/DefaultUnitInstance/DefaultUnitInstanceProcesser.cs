namespace SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

public interface IDefaultUnitInstanceProcessingDiagnostics
{
    public abstract Diagnostic? NullDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition);
    public abstract Diagnostic? EmptyDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition);
    public abstract Diagnostic? SetDefaultUnitInstanceSymbolButNotName(IProcessingContext context, IDefaultUnitInstanceDefinition definition);
    public abstract Diagnostic? SetDefaultUnitInstanceNameButNotSymbol(IProcessingContext context, IDefaultUnitInstanceDefinition definition);
}

public static class DefaultUnitInstanceProcesser
{
    public static IResultWithDiagnostics<(string? Name, string? Symbol)> Process(IProcessingContext context, IDefaultUnitInstanceProcessingDiagnostics diagnostics, IDefaultUnitInstanceDefinition definition)
    {
        if (definition.DefaultUnitInstanceLocations.ExplicitlySetDefaultUnitInstanceSymbol && definition.DefaultUnitInstanceLocations.ExplicitlySetDefaultUnitInstanceName is false)
        {
            return ResultWithDiagnostics.Construct<(string?, string?)>((null, null), diagnostics.SetDefaultUnitInstanceSymbolButNotName(context, definition));
        }

        if (definition.DefaultUnitInstanceLocations.ExplicitlySetDefaultUnitInstanceName && definition.DefaultUnitInstanceLocations.ExplicitlySetDefaultUnitInstanceSymbol is false)
        {
            return ResultWithDiagnostics.Construct<(string?, string?)>((null, null), diagnostics.SetDefaultUnitInstanceNameButNotSymbol(context, definition));
        }

        if (definition.DefaultUnitInstanceLocations.ExplicitlySetDefaultUnitInstanceName)
        {
            if (definition.DefaultUnitInstanceName is null)
            {
                return ResultWithDiagnostics.Construct<(string?, string?)>((null, null), diagnostics.NullDefaultUnitInstanceName(context, definition));
            }

            if (definition.DefaultUnitInstanceName.Length is 0)
            {
                return ResultWithDiagnostics.Construct<(string?, string?)>((null, null), diagnostics.EmptyDefaultUnitInstanceName(context, definition));
            }
        }

        return ResultWithDiagnostics.Construct<(string?, string?)>((definition.DefaultUnitInstanceName, definition.DefaultUnitInstanceSymbol));
    }
}

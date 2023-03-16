namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;

public sealed class DefaultUnitInstanceProcessingDiagnostics : IDefaultUnitInstanceProcessingDiagnostics
{
    public static DefaultUnitInstanceProcessingDiagnostics Instance { get; } = new();

    private DefaultUnitInstanceProcessingDiagnostics() { }

    public Diagnostic NullDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitInstanceNameUnknownType(definition.DefaultUnitInstanceLocations.DefaultUnitInstanceName?.AsRoslynLocation());
    }

    public Diagnostic EmptyDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => NullDefaultUnitInstanceName(context, definition);

    public Diagnostic SetDefaultUnitInstanceSymbolButNotName(IProcessingContext context, IDefaultUnitInstanceDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultUnitInstanceName(definition.DefaultUnitInstanceLocations.DefaultUnitInstanceSymbol?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic SetDefaultUnitInstanceNameButNotSymbol(IProcessingContext context, IDefaultUnitInstanceDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultUnitInstanceSymbol(definition.DefaultUnitInstanceLocations.DefaultUnitInstanceName?.AsRoslynLocation(), context.Type.Name);
    }
}

namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

public class DefaultUnitProcessingDiagnostics : IDefaultUnitProcessingDiagnostics
{
    public static DefaultUnitProcessingDiagnostics Instance { get; } = new();

    private DefaultUnitProcessingDiagnostics() { }

    public Diagnostic NullDefaultUnit(IProcessingContext context, IDefaultUnitDefinition definition)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitNameUnknownType(definition.DefaultUnitLocations.DefaultUnitName?.AsRoslynLocation());
    }

    public Diagnostic EmptyDefaultUnit(IProcessingContext context, IDefaultUnitDefinition definition) => NullDefaultUnit(context, definition);

    public Diagnostic SetDefaultSymbolButNotUnit(IProcessingContext context, IDefaultUnitDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultUnit(definition.DefaultUnitLocations.DefaultUnitSymbol?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic SetDefaultUnitButNotSymbol(IProcessingContext context, IDefaultUnitDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultSymbol(definition.DefaultUnitLocations.DefaultUnitName?.AsRoslynLocation(), context.Type.Name);
    }
}

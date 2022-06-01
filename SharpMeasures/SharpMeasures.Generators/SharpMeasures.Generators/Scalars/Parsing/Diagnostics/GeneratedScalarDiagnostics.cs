namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Diagnostics;

internal class GeneratedScalarDiagnostics : IGeneratedScalarDiagnostics
{
    public static GeneratedScalarDiagnostics Instance { get; } = new();

    private GeneratedScalarDiagnostics() { }

    public Diagnostic NullUnit(IProcessingContext context, RawGeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar_Null(definition.Locations.Unit?.AsRoslynLocation());
    }

    public Diagnostic NullVector(IProcessingContext context, RawGeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector_Null(definition.Locations.Vector?.AsRoslynLocation());
    }

    public Diagnostic NullDefaultUnit(IProcessingContext context, RawGeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotUnit_Null(definition.Locations.DefaultUnitName?.AsRoslynLocation());
    }

    public Diagnostic EmptyDefaultUnit(IProcessingContext context, RawGeneratedScalarDefinition definition) => NullDefaultUnit(context, definition);

    public Diagnostic SetDefaultSymbolButNotUnit(IProcessingContext context, RawGeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityUnitAndSymbol_MissingSymbol(definition.Locations.DefaultUnitName?.AsRoslynLocation());
    }

    public Diagnostic NullPowerQuantity(MinimalLocation? location)
    {
        return DiagnosticConstruction.TypeNotScalar_Null(location?.AsRoslynLocation());
    }
}

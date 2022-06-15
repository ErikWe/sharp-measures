namespace SharpMeasures.Generators.Scalars.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.GeneratedScalar;
using SharpMeasures.Generators.Scalars.Refinement.GeneratedScalar;

internal class GeneratedScalarDiagnostics : IGeneratedScalarProcessingDiagnostics, IGeneratedScalarRefinementDiagnostics
{
    public static GeneratedScalarDiagnostics Instance { get; } = new();

    private GeneratedScalarDiagnostics() { }

    public Diagnostic NullUnit(IProcessingContext context, RawGeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotUnit(definition.Locations.Unit?.AsRoslynLocation());
    }

    public Diagnostic NullVector(IProcessingContext context, RawGeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotVector(definition.Locations.Vector?.AsRoslynLocation());
    }

    public Diagnostic NullDefaultUnit(IProcessingContext context, RawGeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitName(definition.Locations.DefaultUnitName?.AsRoslynLocation(), definition.Unit!.Value.Name);
    }

    public Diagnostic EmptyDefaultUnit(IProcessingContext context, RawGeneratedScalarDefinition definition) => NullDefaultUnit(context, definition);

    public Diagnostic SetDefaultSymbolButNotUnit(IProcessingContext context, RawGeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultSymbol(definition.Locations.DefaultUnitName?.AsRoslynLocation());
    }

    public Diagnostic NullReciprocalQuantity(IProcessingContext context, RawGeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Reciprocal?.AsRoslynLocation());
    }

    public Diagnostic NullSquareQuantity(IProcessingContext context, RawGeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Square?.AsRoslynLocation());
    }

    public Diagnostic NullCubeQuantity(IProcessingContext context, RawGeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Cube?.AsRoslynLocation());
    }

    public Diagnostic NullSquareRootQuantity(IProcessingContext context, RawGeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.SquareRoot?.AsRoslynLocation());
    }

    public Diagnostic NullCubeRootQuantity(IProcessingContext context, RawGeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.CubeRoot?.AsRoslynLocation());
    }

    public Diagnostic TypeAlreadyUnit(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.ScalarTypeAlreadyDefinedAsUnit(definition.Locations.Attribute.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeNotUnit(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotUnit(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit.Name);
    }

    public Diagnostic UnitNotSupportingBiasedQuantities(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.UnitNotSupportingBias(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit.Name);
    }

    public Diagnostic TypeNotVector(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.Vector?.AsRoslynLocation(), definition.Vector!.Value.Name);
    }

    public Diagnostic UnrecognizedDefaultUnit(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.DefaultUnitName?.AsRoslynLocation(), definition.DefaultUnitName!, definition.Unit.Name);
    }

    public Diagnostic ReciprocalNotScalar(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Reciprocal?.AsRoslynLocation(), definition.Reciprocal!.Value.Name);
    }

    public Diagnostic SquareNotScalar(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Square?.AsRoslynLocation(), definition.Square!.Value.Name);
    }

    public Diagnostic CubeNotScalar(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Cube?.AsRoslynLocation(), definition.Cube!.Value.Name);
    }

    public Diagnostic SquareRootNotScalar(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.SquareRoot?.AsRoslynLocation(), definition.SquareRoot!.Value.Name);
    }

    public Diagnostic CubeRootNotScalar(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.CubeRoot?.AsRoslynLocation(), definition.CubeRoot!.Value.Name);
    }
}

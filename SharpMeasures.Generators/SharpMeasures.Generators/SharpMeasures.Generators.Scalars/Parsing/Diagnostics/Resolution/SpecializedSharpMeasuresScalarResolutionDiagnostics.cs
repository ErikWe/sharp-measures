namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Resolution;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;
using SharpMeasures.Generators.Unresolved.Units;

internal class SpecialziedSharpMeasuresScalarResolutionDiagnostics : ISpecializedSharpMeasuresScalarResolutionDiagnostics
{
    public static SpecialziedSharpMeasuresScalarResolutionDiagnostics Instance { get; } = new();

    private SpecialziedSharpMeasuresScalarResolutionDiagnostics() { }

    public Diagnostic TypeAlreadyUnit(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.ScalarTypeAlreadyDefinedAsUnit(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic OriginalNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.OriginalScalar?.AsRoslynLocation(), definition.OriginalScalar.Name);
    }

    public Diagnostic TypeNotVector(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.Vector?.AsRoslynLocation(), definition.Vector!.Value.Name);
    }

    public Diagnostic UnrecognizedDefaultUnit(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition,
        IUnresolvedUnitType unit)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.DefaultUnitName?.AsRoslynLocation(), definition.DefaultUnitName!, unit.Type.Name);
    }

    public Diagnostic DifferenceNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference!.Value.Name);
    }

    public Diagnostic ReciprocalNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Reciprocal?.AsRoslynLocation(), definition.Reciprocal!.Value.Name);
    }

    public Diagnostic SquareNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Square?.AsRoslynLocation(), definition.Square!.Value.Name);
    }

    public Diagnostic CubeNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Cube?.AsRoslynLocation(), definition.Cube!.Value.Name);
    }

    public Diagnostic SquareRootNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.SquareRoot?.AsRoslynLocation(), definition.SquareRoot!.Value.Name);
    }

    public Diagnostic CubeRootNotScalar(ISpecializedSharpMeasuresScalarResolutionContext context, UnresolvedSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.CubeRoot?.AsRoslynLocation(), definition.CubeRoot!.Value.Name);
    }
}

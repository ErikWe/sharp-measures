namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Resolution;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

internal class SharpMeasuresVectorGroupResolutionDiagnostics : ISharpMeasuresVectorGroupResolutionDiagnostics
{
    public static SharpMeasuresVectorGroupResolutionDiagnostics Instance { get; } = new();

    private SharpMeasuresVectorGroupResolutionDiagnostics() { }

    public Diagnostic TypeAlreadyUnit(ISharpMeasuresVectorGroupResolutionContext context, UnresolvedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupTypeAlreadyDefinedAsUnit(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyScalar(ISharpMeasuresVectorGroupResolutionContext context, UnresolvedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupTypeAlreadyDefinedAsScalar(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeNotUnit(ISharpMeasuresVectorGroupResolutionContext context, UnresolvedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.TypeNotUnit(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit.Name);
    }

    public Diagnostic TypeNotScalar(ISharpMeasuresVectorGroupResolutionContext context, UnresolvedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Scalar?.AsRoslynLocation(), definition.Scalar!.Value.Name);
    }

    public Diagnostic DifferenceNotVectorGroup(ISharpMeasuresVectorGroupResolutionContext context, UnresolvedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVectorGroup(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference.Name);
    }

    public Diagnostic UnrecognizedDefaultUnit(ISharpMeasuresVectorGroupResolutionContext context, UnresolvedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.DefaultUnitName?.AsRoslynLocation(), definition.DefaultUnitName!, definition.Unit.Name);
    }
}

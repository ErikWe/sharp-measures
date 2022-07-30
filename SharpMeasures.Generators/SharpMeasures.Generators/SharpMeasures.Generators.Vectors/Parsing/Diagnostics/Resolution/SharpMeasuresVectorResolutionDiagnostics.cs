namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Resolution;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

internal class SharpMeasuresVectorResolutionDiagnostics : ISharpMeasuresVectorResolutionDiagnostics
{
    public static SharpMeasuresVectorResolutionDiagnostics Instance { get; } = new();

    private SharpMeasuresVectorResolutionDiagnostics() { }

    public Diagnostic TypeAlreadyUnit(ISharpMeasuresVectorResolutionContext context, UnresolvedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsUnit(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyScalar(ISharpMeasuresVectorResolutionContext context, UnresolvedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsScalar(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyVector(ISharpMeasuresVectorResolutionContext context, UnresolvedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsVector(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeNotUnit(ISharpMeasuresVectorResolutionContext context, UnresolvedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotUnit(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit.Name);
    }

    public Diagnostic TypeNotScalar(ISharpMeasuresVectorResolutionContext context, UnresolvedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Scalar?.AsRoslynLocation(), definition.Scalar!.Value.Name);
    }

    public Diagnostic DifferenceNotVector(ISharpMeasuresVectorResolutionContext context, UnresolvedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference.Name);
    }

    public Diagnostic DifferenceVectorGroupLacksMatchingDimension(ISharpMeasuresVectorResolutionContext context, UnresolvedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupsLacksMemberOfDimension(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference.Name,
            definition.Dimension);
    }

    public Diagnostic UnrecognizedDefaultUnit(ISharpMeasuresVectorResolutionContext context, UnresolvedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.DefaultUnitName?.AsRoslynLocation(), definition.DefaultUnitName!, definition.Unit.Name);
    }
}

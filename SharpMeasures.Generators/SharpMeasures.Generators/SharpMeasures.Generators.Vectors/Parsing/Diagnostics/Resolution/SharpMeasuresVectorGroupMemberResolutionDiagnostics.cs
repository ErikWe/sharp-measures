namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Resolution;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

internal class SharpMeasuresVectorGroupMemberResolutionDiagnostics : ISharpMeasuresVectorGroupMemberResolutionDiagnostics
{
    public static SharpMeasuresVectorGroupMemberResolutionDiagnostics Instance { get; } = new();

    private SharpMeasuresVectorGroupMemberResolutionDiagnostics() { }

    public Diagnostic TypeAlreadyUnit(ISharpMeasuresVectorGroupMemberResolutionContext context, UnresolvedSharpMeasuresVectorGroupMemberDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupMemberTypeAlreadyDefinedAsUnit(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyScalar(ISharpMeasuresVectorGroupMemberResolutionContext context, UnresolvedSharpMeasuresVectorGroupMemberDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupMemberTypeAlreadyDefinedAsScalar(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyVector(ISharpMeasuresVectorGroupMemberResolutionContext context, UnresolvedSharpMeasuresVectorGroupMemberDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupMemberTypeAlreadyDefinedAsVector(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }
    public Diagnostic TypeAlreadyVectorGroup(ISharpMeasuresVectorGroupMemberResolutionContext context, UnresolvedSharpMeasuresVectorGroupMemberDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupMemberTypeAlreadyDefinedAsVectorGroup(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyVectorGroupMember(ISharpMeasuresVectorGroupMemberResolutionContext context, UnresolvedSharpMeasuresVectorGroupMemberDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupMemberTypeAlreadyDefinedAsVectorGroupMember(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeNotVectorGroup(ISharpMeasuresVectorGroupMemberResolutionContext context, UnresolvedSharpMeasuresVectorGroupMemberDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVectorGroup(definition.Locations.VectorGroup?.AsRoslynLocation(), definition.VectorGroup.Name);
    }
}

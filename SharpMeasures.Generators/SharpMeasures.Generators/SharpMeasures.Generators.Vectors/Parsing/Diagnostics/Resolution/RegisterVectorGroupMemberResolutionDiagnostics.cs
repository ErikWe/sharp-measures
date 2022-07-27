namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Resolution;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;

internal class RegisterVectorGroupMemberResolutionDiagnostics : IRegisterVectorGroupMemberResolutionDiagnostics
{
    public static RegisterVectorGroupMemberResolutionDiagnostics Instance { get; } = new();

    private RegisterVectorGroupMemberResolutionDiagnostics() { }

    public Diagnostic TypeNotVectorGroupMember(IRegisterVectorGroupMemberResolutionContext context, UnresolvedRegisterVectorGroupMemberDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVectorGroupMemberSpecificGroup(definition.Locations.Vector?.AsRoslynLocation(), definition.Vector.Name, context.VectorGroup.Type.Name);
    }

    public Diagnostic VectorNotMemberOfVectorGroup(IRegisterVectorGroupMemberResolutionContext context, UnresolvedRegisterVectorGroupMemberDefinition definition)
    {
        return DiagnosticConstruction.VectorNotMemberOfVectorGroup(definition.Locations.Vector?.AsRoslynLocation(), context.VectorGroup.Type.Name, definition.Vector.Name);
    }
}

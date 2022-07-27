namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;

internal class RegisterVectorGroupMemberProcessingDiagnostics : IRegisterVectorGroupMemberProcessingDiagnostics
{
    public static RegisterVectorGroupMemberProcessingDiagnostics Instance { get; } = new();

    private RegisterVectorGroupMemberProcessingDiagnostics() { }

    public Diagnostic NullVector(IRegisterVectorGroupMemberProcessingContext context, RawRegisterVectorGroupMemberDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotVector(definition.Locations.Vector?.AsRoslynLocation());
    }

    public Diagnostic InvalidDimension(IRegisterVectorGroupMemberProcessingContext context, RawRegisterVectorGroupMemberDefinition definition)
    {
        return DiagnosticConstruction.InvalidVectorDimension(definition.Locations.Dimension?.AsRoslynLocation(), definition.Dimension);
    }

    public Diagnostic InvalidInterpretedDimension(IRegisterVectorGroupMemberProcessingContext context, RawRegisterVectorGroupMemberDefinition definition, int dimension)
    {
        return DiagnosticConstruction.InvalidInterpretedVectorDimension(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name, dimension);
    }

    public Diagnostic MissingDimension(IRegisterVectorGroupMemberProcessingContext context, RawRegisterVectorGroupMemberDefinition definition)
    {
        return DiagnosticConstruction.MissingVectorDimension(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic DuplicateDimension(IRegisterVectorGroupMemberProcessingContext context, RawRegisterVectorGroupMemberDefinition definition, int dimension)
    {
        return DiagnosticConstruction.DuplicateVectorDimension(definition.Locations.Dimension?.AsRoslynLocation(), dimension);
    }
}

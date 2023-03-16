namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

internal sealed class EmptySharpMeasuresVectorGroupMemberValidationDiagnostics : ISharpMeasuresVectorGroupMemberValidationDiagnostics
{
    public static EmptySharpMeasuresVectorGroupMemberValidationDiagnostics Instance { get; } = new();

    private EmptySharpMeasuresVectorGroupMemberValidationDiagnostics() { }

    public Diagnostic? TypeAlreadyUnit(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition) => null;
    public Diagnostic? TypeAlreadyScalar(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition) => null;
    public Diagnostic? TypeAlreadyVector(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition) => null;
    public Diagnostic? TypeAlreadyVectorGroup(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition) => null;
    public Diagnostic? TypeNotVectorGroup(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition) => null;
    public Diagnostic? VectorGroupAlreadyContainsDimension(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition) => null;
}

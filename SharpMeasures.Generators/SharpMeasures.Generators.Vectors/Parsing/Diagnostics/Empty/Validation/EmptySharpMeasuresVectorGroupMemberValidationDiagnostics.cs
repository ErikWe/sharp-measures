namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

internal sealed class EmptySharpMeasuresVectorGroupMemberValidationDiagnostics : ISharpMeasuresVectorGroupMemberValidationDiagnostics
{
    public static EmptySharpMeasuresVectorGroupMemberValidationDiagnostics Instance { get; } = new();

    private EmptySharpMeasuresVectorGroupMemberValidationDiagnostics() { }

    Diagnostic? ISharpMeasuresVectorGroupMemberValidationDiagnostics.TypeAlreadyUnit(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorGroupMemberValidationDiagnostics.TypeAlreadyScalar(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorGroupMemberValidationDiagnostics.TypeAlreadyVector(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorGroupMemberValidationDiagnostics.TypeAlreadyVectorGroup(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorGroupMemberValidationDiagnostics.TypeNotVectorGroup(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorGroupMemberValidationDiagnostics.VectorGroupAlreadyContainsDimension(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition) => null;
}

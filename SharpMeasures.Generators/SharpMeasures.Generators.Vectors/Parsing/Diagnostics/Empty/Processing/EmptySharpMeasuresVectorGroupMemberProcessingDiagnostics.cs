namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

internal sealed class EmptySharpMeasuresVectorGroupMemberProcessingDiagnostics : ISharpMeasuresVectorGroupMemberProcessingDiagnostics
{
    public static EmptySharpMeasuresVectorGroupMemberProcessingDiagnostics Instance { get; } = new();

    private EmptySharpMeasuresVectorGroupMemberProcessingDiagnostics() { }

    public Diagnostic? InvalidDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition) => null;
    public Diagnostic? InvalidInterpretedDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition, int dimension) => null;
    public Diagnostic? MissingDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition) => null;
    public Diagnostic? NullVectorGroup(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition) => null;
    public Diagnostic? VectorNameAndDimensionConflict(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition, int interpretedDimension) => null;
}

namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

internal class EmptySharpMeasuresVectorGroupMemberProcessingDiagnostics : ISharpMeasuresVectorGroupMemberProcessingDiagnostics
{
    public static EmptySharpMeasuresVectorGroupMemberProcessingDiagnostics Instance { get; } = new();

    Diagnostic? ISharpMeasuresVectorGroupMemberProcessingDiagnostics.InvalidDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorGroupMemberProcessingDiagnostics.InvalidInterpretedDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition, int dimension) => null;
    Diagnostic? ISharpMeasuresVectorGroupMemberProcessingDiagnostics.MissingDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorGroupMemberProcessingDiagnostics.NullVectorGroup(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorGroupMemberProcessingDiagnostics.VectorNameAndDimensionConflict(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition, int interpretedDimension) => null;
}

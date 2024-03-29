﻿namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

internal sealed class SharpMeasuresVectorGroupMemberProcessingDiagnostics : ISharpMeasuresVectorGroupMemberProcessingDiagnostics
{
    public static SharpMeasuresVectorGroupMemberProcessingDiagnostics Instance { get; } = new();

    private SharpMeasuresVectorGroupMemberProcessingDiagnostics() { }

    public Diagnostic NullVectorGroup(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotVectorGroup(definition.Locations.VectorGroup?.AsRoslynLocation());
    }

    public Diagnostic InvalidDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition)
    {
        return DiagnosticConstruction.InvalidVectorDimension(definition.Locations.Dimension?.AsRoslynLocation(), definition.Dimension!.Value);
    }

    public Diagnostic InvalidInterpretedDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition, int dimension)
    {
        return DiagnosticConstruction.InvalidInterpretedVectorDimension(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name, dimension);
    }

    public Diagnostic MissingDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition)
    {
        return DiagnosticConstruction.MissingVectorDimension(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic VectorNameAndDimensionConflict(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition, int interpretedDimension)
    {
        return DiagnosticConstruction.VectorNameAndDimensionConflict(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name, interpretedDimension, definition.Dimension!.Value);
    }
}

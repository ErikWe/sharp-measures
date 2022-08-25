namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

internal class SharpMeasuresVectorGroupMemberValidationDiagnostics : ISharpMeasuresVectorGroupMemberValidationDiagnostics
{
    public static SharpMeasuresVectorGroupMemberValidationDiagnostics Instance { get; } = new();

    private SharpMeasuresVectorGroupMemberValidationDiagnostics() { }

    public Diagnostic TypeAlreadyUnit(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupMemberTypeAlreadyDefinedAsUnit(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyScalar(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupMemberTypeAlreadyDefinedAsScalar(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyVector(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupMemberTypeAlreadyDefinedAsVector(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyVectorGroup(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupMemberTypeAlreadyDefinedAsVectorGroup(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyVectorGroupMember(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupMemberTypeAlreadyDefinedAsVectorGroupMember(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeNotVectorGroup(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVectorGroup(definition.Locations.VectorGroup?.AsRoslynLocation(), definition.VectorGroup.Name);
    }

    public Diagnostic VectorGroupAlreadyContainsDimension(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition)
    {
        if (definition.Locations.ExplicitlySetDimension)
        {
            return DiagnosticConstruction.DuplicateVectorDimension(definition.Locations.Dimension?.AsRoslynLocation(), definition.VectorGroup.Name, definition.Dimension);
        }

        return DiagnosticConstruction.DuplicateVectorDimension(definition.Locations.AttributeName.AsRoslynLocation(), definition.VectorGroup.Name, definition.Dimension);
    }
}

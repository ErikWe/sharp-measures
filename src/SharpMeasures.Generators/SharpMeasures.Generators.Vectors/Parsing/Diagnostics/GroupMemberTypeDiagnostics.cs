namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;

using System;

internal static class GroupMemberTypeDiagnostics
{
    public static Diagnostic TypeNotPartial<TAttribute>(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<TAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }

    public static Diagnostic TypeStatic<TAttribute>(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeStatic<TAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }

    public static Diagnostic UnitTypeAlreadyGroupMember(AttributeData attribute, DefinedType type) => TypeAlreadyGroupMember(attribute, type, DiagnosticConstruction.UnitTypeAlreadyDefinedAsVectorGroupMember);
    public static Diagnostic ScalarTypeAlreadyGroupMember(AttributeData attribute, DefinedType type) => TypeAlreadyGroupMember(attribute, type, DiagnosticConstruction.ScalarTypeAlreadyDefinedAsVectorGroupMember);
    public static Diagnostic SpecializedScalarTypeAlreadyGroupMember(AttributeData attribute, DefinedType type) => ScalarTypeAlreadyGroupMember(attribute, type);
    public static Diagnostic VectorTypeAlreadyGroupMember(AttributeData attribute, DefinedType type) => TypeAlreadyGroupMember(attribute, type, DiagnosticConstruction.VectorTypeAlreadyDefinedAsVectorGroupMember);
    public static Diagnostic SpecializedVectorTypeAlreadyGroupMember(AttributeData attribute, DefinedType type) => VectorTypeAlreadyGroupMember(attribute, type);

    private static Diagnostic TypeAlreadyGroupMember(AttributeData attribute, DefinedType type, Func<Location?, string, Diagnostic> diagnosticsDelegate)
    {
        var location = attribute.GetSyntax() is AttributeSyntax attributeSyntax ? attributeSyntax.Name.GetLocation() : null;

        return diagnosticsDelegate(location, type.Name);
    }
}

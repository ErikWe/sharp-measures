namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;

using System;

internal static class ScalarTypeDiagnostics
{
    public static Diagnostic TypeNotPartial<TAttribute>(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<TAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }

    public static Diagnostic TypeStatic<TAttribute>(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeStatic<TAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }

    public static Diagnostic UnitTypeAlreadyScalar(AttributeData attribute, DefinedType type) => TypeAlreadyScalar(attribute, type, DiagnosticConstruction.UnitTypeAlreadyDefinedAsScalar);
    public static Diagnostic ScalarTypeAlreadyScalar(AttributeData attribute, DefinedType type) => TypeAlreadyScalar(attribute, type, DiagnosticConstruction.ScalarTypeAlreadyDefinedAsScalar);
    public static Diagnostic SpecializedScalarTypeAlreadyScalar(AttributeData attribute, DefinedType type) => ScalarTypeAlreadyScalar(attribute, type);
    public static Diagnostic VectorTypeAlreadyScalar(AttributeData attribute, DefinedType type) => TypeAlreadyScalar(attribute, type, DiagnosticConstruction.VectorTypeAlreadyDefinedAsScalar);
    public static Diagnostic SpecializedVectorTypeAlreadyScalar(AttributeData attribute, DefinedType type) => VectorTypeAlreadyScalar(attribute, type);
    public static Diagnostic VectorGroupMemberTypeAlreadyScalar(AttributeData attribute, DefinedType type) => TypeAlreadyScalar(attribute, type, DiagnosticConstruction.VectorGroupMemberTypeAlreadyDefinedAsScalar);

    private static Diagnostic TypeAlreadyScalar(AttributeData attribute, DefinedType type, Func<Location?, string, Diagnostic> diagnosticsDelegate)
    {
        var location = attribute.GetSyntax() is AttributeSyntax attributeSyntax ? attributeSyntax.Name.GetLocation() : null;

        return diagnosticsDelegate(location, type.Name);
    }
}

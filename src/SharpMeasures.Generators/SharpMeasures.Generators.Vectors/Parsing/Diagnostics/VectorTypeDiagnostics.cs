namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;

using System;

internal static class VectorTypeDiagnostics
{
    public static Diagnostic TypeNotPartial<TAttribute>(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<TAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }

    public static Diagnostic TypeStatic<TAttribute>(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeStatic<TAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }

    public static Diagnostic UnitTypeAlreadyVector(AttributeData attribute, DefinedType type) => TypeAlreadyVector(attribute, type, DiagnosticConstruction.UnitTypeAlreadyDefinedAsVector);
    public static Diagnostic ScalarTypeAlreadyVector(AttributeData attribute, DefinedType type) => TypeAlreadyVector(attribute, type, DiagnosticConstruction.ScalarTypeAlreadyDefinedAsVector);
    public static Diagnostic SpecializedScalarTypeAlreadyVector(AttributeData attribute, DefinedType type) => ScalarTypeAlreadyVector(attribute, type);
    public static Diagnostic VectorTypeAlreadyVector(AttributeData attribute, DefinedType type) => TypeAlreadyVector(attribute, type, DiagnosticConstruction.VectorTypeAlreadyDefinedAsVector);
    public static Diagnostic SpecializedVectorTypeAlreadyVector(AttributeData attribute, DefinedType type) => VectorTypeAlreadyVector(attribute, type);
    public static Diagnostic VectorGroupMemberTypeAlreadyVector(AttributeData attribute, DefinedType type) => TypeAlreadyVector(attribute, type, DiagnosticConstruction.VectorGroupMemberTypeAlreadyDefinedAsVector);

    private static Diagnostic TypeAlreadyVector(AttributeData attribute, DefinedType type, Func<Location?, string, Diagnostic> diagnosticsDelegate)
    {
        var location = attribute.GetSyntax() is AttributeSyntax attributeSyntax ? attributeSyntax.Name.GetLocation() : null;

        return diagnosticsDelegate(location, type.Name);
    }
}

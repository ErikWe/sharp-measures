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

    public static Diagnostic UnitTypeAlreadyVector(AttributeData attribute, INamedTypeSymbol typeSymbol) => TypeAlreadyVector(attribute, typeSymbol, DiagnosticConstruction.UnitTypeAlreadyDefinedAsVector);
    public static Diagnostic ScalarTypeAlreadyVector(AttributeData attribute, INamedTypeSymbol typeSymbol) => TypeAlreadyVector(attribute, typeSymbol, DiagnosticConstruction.ScalarTypeAlreadyDefinedAsVector);
    public static Diagnostic SpecializedScalarTypeAlreadyVector(AttributeData attribute, INamedTypeSymbol typeSymbol) => ScalarTypeAlreadyVector(attribute, typeSymbol);
    public static Diagnostic VectorTypeAlreadyVector(AttributeData attribute, INamedTypeSymbol typeSymbol) => TypeAlreadyVector(attribute, typeSymbol, DiagnosticConstruction.VectorTypeAlreadyDefinedAsVector);
    public static Diagnostic VectorGroupMemberTypeAlreadyVector(AttributeData attribute, INamedTypeSymbol typeSymbol) => TypeAlreadyVector(attribute, typeSymbol, DiagnosticConstruction.VectorGroupMemberTypeAlreadyDefinedAsVector);

    private static Diagnostic TypeAlreadyVector(AttributeData attribute, INamedTypeSymbol typeSymbol, Func<Location?, string, Diagnostic> diagnosticsDelegate)
    {
        var location = attribute.GetSyntax() is AttributeSyntax attributeSyntax ? attributeSyntax.Name.GetLocation() : null;

        return diagnosticsDelegate(location, typeSymbol.Name);
    }
}

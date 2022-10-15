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

    public static Diagnostic UnitTypeAlreadyScalar(AttributeData attribute, INamedTypeSymbol typeSymbol) => TypeAlreadyScalar(attribute, typeSymbol, DiagnosticConstruction.UnitTypeAlreadyDefinedAsScalar);
    public static Diagnostic ScalarTypeAlreadyScalar(AttributeData attribute, INamedTypeSymbol typeSymbol) => TypeAlreadyScalar(attribute, typeSymbol, DiagnosticConstruction.ScalarTypeAlreadyDefinedAsScalar);
    public static Diagnostic VectorTypeAlreadyScalar(AttributeData attribute, INamedTypeSymbol typeSymbol) => TypeAlreadyScalar(attribute, typeSymbol, DiagnosticConstruction.VectorTypeAlreadyDefinedAsScalar);
    public static Diagnostic SpecializedVectorTypeAlreadyScalar(AttributeData attribute, INamedTypeSymbol typeSymbol) => VectorTypeAlreadyScalar(attribute, typeSymbol);
    public static Diagnostic VectorGroupMemberTypeAlreadyScalar(AttributeData attribute, INamedTypeSymbol typeSymbol) => TypeAlreadyScalar(attribute, typeSymbol, DiagnosticConstruction.VectorGroupMemberTypeAlreadyDefinedAsScalar);

    private static Diagnostic TypeAlreadyScalar(AttributeData attribute, INamedTypeSymbol typeSymbol, Func<Location?, string, Diagnostic> diagnosticsDelegate)
    {
        var location = attribute.GetSyntax() is AttributeSyntax attributeSyntax ? attributeSyntax.Name.GetLocation() : null;

        return diagnosticsDelegate(location, typeSymbol.Name);
    }
}

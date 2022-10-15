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

    public static Diagnostic UnitTypeAlreadyGroupMember(AttributeData attribute, INamedTypeSymbol typeSymbol) => TypeAlreadyGroupMember(attribute, typeSymbol, DiagnosticConstruction.UnitTypeAlreadyDefinedAsVectorGroupMember);
    public static Diagnostic ScalarTypeAlreadyGroupMember(AttributeData attribute, INamedTypeSymbol typeSymbol) => TypeAlreadyGroupMember(attribute, typeSymbol, DiagnosticConstruction.ScalarTypeAlreadyDefinedAsVectorGroupMember);
    public static Diagnostic SpecializedScalarTypeAlreadyGroupMember(AttributeData attribute, INamedTypeSymbol typeSymbol) => ScalarTypeAlreadyGroupMember(attribute, typeSymbol);
    public static Diagnostic VectorTypeAlreadyGroupMember(AttributeData attribute, INamedTypeSymbol typeSymbol) => TypeAlreadyGroupMember(attribute, typeSymbol, DiagnosticConstruction.VectorTypeAlreadyDefinedAsVectorGroupMember);
    public static Diagnostic SpecializedVectorTypeAlreadyGroupMember(AttributeData attribute, INamedTypeSymbol typeSymbol) => VectorTypeAlreadyGroupMember(attribute, typeSymbol);

    private static Diagnostic TypeAlreadyGroupMember(AttributeData attribute, INamedTypeSymbol typeSymbol, Func<Location?, string, Diagnostic> diagnosticsDelegate)
    {
        var location = attribute.GetSyntax() is AttributeSyntax attributeSyntax ? attributeSyntax.Name.GetLocation() : null;

        return diagnosticsDelegate(location, typeSymbol.Name);
    }
}

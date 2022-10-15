namespace SharpMeasures.Generators.Units.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;

using System;

internal static class UnitTypeDiagnostics
{
    public static Diagnostic TypeNotPartial(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeNotPartial<UnitAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }

    public static Diagnostic TypeStatic(BaseTypeDeclarationSyntax declaration)
    {
        return DiagnosticConstruction.TypeStatic<UnitAttribute>(declaration.Identifier.GetLocation(), declaration.Identifier.Text);
    }

    public static Diagnostic ScalarTypeAlreadyUnit(AttributeData attribute, INamedTypeSymbol typeSymbol) => TypeAlreadyUnit(attribute, typeSymbol, DiagnosticConstruction.ScalarTypeAlreadyDefinedAsUnit);
    public static Diagnostic SpecializedScalarTypeAlreadyUnit(AttributeData attribute, INamedTypeSymbol typeSymbol) => ScalarTypeAlreadyUnit(attribute, typeSymbol);
    public static Diagnostic VectorTypeAlreadyUnit(AttributeData attribute, INamedTypeSymbol typeSymbol) => TypeAlreadyUnit(attribute, typeSymbol, DiagnosticConstruction.VectorTypeAlreadyDefinedAsUnit);
    public static Diagnostic SpecializedVectorTypeAlreadyUnit(AttributeData attribute, INamedTypeSymbol typeSymbol) => VectorTypeAlreadyUnit(attribute, typeSymbol);
    public static Diagnostic VectorGroupMemberTypeAlreadyUnit(AttributeData attribute, INamedTypeSymbol typeSymbol) => TypeAlreadyUnit(attribute, typeSymbol, DiagnosticConstruction.VectorGroupMemberTypeAlreadyDefinedAsUnit);

    private static Diagnostic TypeAlreadyUnit(AttributeData attribute, INamedTypeSymbol typeSymbol, Func<Location?, string, Diagnostic> diagnosticsDelegate)
    {
        var location = attribute.GetSyntax() is AttributeSyntax attributeSyntax ? attributeSyntax.Name.GetLocation() : null;

        return diagnosticsDelegate(location, typeSymbol.Name);
    }
}

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

    public static Diagnostic ScalarTypeAlreadyUnit(AttributeData attribute, DefinedType type) => TypeAlreadyUnit(attribute, type, DiagnosticConstruction.ScalarTypeAlreadyDefinedAsUnit);
    public static Diagnostic SpecializedScalarTypeAlreadyUnit(AttributeData attribute, DefinedType type) => ScalarTypeAlreadyUnit(attribute, type);
    public static Diagnostic VectorTypeAlreadyUnit(AttributeData attribute, DefinedType type) => TypeAlreadyUnit(attribute, type, DiagnosticConstruction.VectorTypeAlreadyDefinedAsUnit);
    public static Diagnostic SpecializedVectorTypeAlreadyUnit(AttributeData attribute, DefinedType type) => VectorTypeAlreadyUnit(attribute, type);
    public static Diagnostic VectorGroupMemberTypeAlreadyUnit(AttributeData attribute, DefinedType type) => TypeAlreadyUnit(attribute, type, DiagnosticConstruction.VectorGroupMemberTypeAlreadyDefinedAsUnit);

    private static Diagnostic TypeAlreadyUnit(AttributeData attribute, DefinedType type, Func<Location?, string, Diagnostic> diagnosticsDelegate)
    {
        var location = attribute.GetSyntax() is AttributeSyntax attributeSyntax ? attributeSyntax.Name.GetLocation() : null;

        return diagnosticsDelegate(location, type.Name);
    }
}

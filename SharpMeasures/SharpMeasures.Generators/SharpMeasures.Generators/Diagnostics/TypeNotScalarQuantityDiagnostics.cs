namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

internal static class TypeNotScalarQuantityDiagnostics
{
    public static Diagnostic Create(Location location, string typeName) => Diagnostic.Create(DiagnosticRules.TypeNotScalarQuantity, location, typeName);
    public static Diagnostic Create(MinimalLocation location, string typeName) => Create(location.ToLocation(), typeName);

    public static Diagnostic Create(Location location, INamedTypeSymbol typeSymbol) => Create(location, typeSymbol.Name);
    public static Diagnostic Create(MinimalLocation location, INamedTypeSymbol typeName) => Create(location.ToLocation(), typeName);

    public static Diagnostic CreateForNull(Location location) => Diagnostic.Create(DiagnosticRules.TypeNotScalarQuantity, location);
    public static Diagnostic CreateForNull(MinimalLocation location) => CreateForNull(location.ToLocation());
}

namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

internal static class TypeNotUnitDiagnostics
{
    public static Diagnostic Create(Location location, string typeName) => Diagnostic.Create(DiagnosticRules.TypeNotUnit, location, typeName);
    public static Diagnostic Create(MinimalLocation location, string typeName) => Create(location.ToLocation(), typeName);

    public static Diagnostic Create(Location location, INamedTypeSymbol typeSymbol) => Create(location, typeSymbol.Name);
    public static Diagnostic Create(MinimalLocation location, INamedTypeSymbol typeSymbol) => Create(location.ToLocation(), typeSymbol);
}

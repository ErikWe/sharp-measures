namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

public readonly record struct NamedType(string Name, string Namespace, string Assembly, bool IsValueType)
{
    public static NamedType Empty { get; } = new NamedType(string.Empty, string.Empty, string.Empty, false);

    public static NamedType FromSymbol(INamedTypeSymbol symbol)
    {
        if (symbol.Kind is SymbolKind.ErrorType)
        {
            return Empty;
        }

        return new(symbol.Name, GetNamespace(symbol), symbol.ContainingAssembly.Name, symbol.IsValueType);
    }

    public bool IsReferenceType => IsValueType is false;

    public string FullyQualifiedName => $"global::{(string.IsNullOrEmpty(Namespace) ? string.Empty : $"{Namespace}.")}{Name}";

    private static string GetNamespace(ISymbol symbol)
    {
        if (symbol.ContainingNamespace is null || symbol.ContainingNamespace.Name.Length is 0)
        {
            return string.Empty;
        }

        string containingNamespace = GetNamespace(symbol.ContainingNamespace);

        if (containingNamespace.Length > 0)
        {
            return $"{containingNamespace}.{symbol.ContainingNamespace.Name}";
        }

        return symbol.ContainingNamespace.Name;
    }
}

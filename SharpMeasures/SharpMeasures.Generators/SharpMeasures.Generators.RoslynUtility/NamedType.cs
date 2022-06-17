namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

public readonly record struct NamedType(string Name, string Namespace, bool IsValueType)
{
    public static NamedType Empty { get; } = new NamedType(string.Empty, string.Empty, false);

    internal static NamedType FromSymbol(INamedTypeSymbol symbol)
    {
        return new(symbol.Name, symbol.ContainingNamespace?.Name ?? string.Empty, symbol.IsValueType);
    }

    public bool IsReferenceType => IsValueType is false;

    public string FullyQualifiedName => string.IsNullOrEmpty(Namespace) ? Name : $"{Namespace}.{Name}";
}

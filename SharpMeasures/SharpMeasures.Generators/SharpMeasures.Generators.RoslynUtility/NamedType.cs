namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using System;
using System.Globalization;

public readonly record struct NamedType(string Name, string Namespace)
{
    public static NamedType Empty { get; } = new NamedType(string.Empty, string.Empty);

    internal static NamedType FromSymbol(INamedTypeSymbol symbol)
    {
        if (symbol is null)
        {
            throw new ArgumentNullException(nameof(symbol));
        }

        return new(symbol.Name, symbol.ContainingNamespace?.Name ?? string.Empty);
    }

    public string FullyQualifiedName => string.IsNullOrEmpty(Namespace) ? Name : $"{Namespace}.{Name}";

    public string ParameterName => Name.Substring(0, 1).ToLower(CultureInfo.CurrentCulture) + Name.Substring(1);
}

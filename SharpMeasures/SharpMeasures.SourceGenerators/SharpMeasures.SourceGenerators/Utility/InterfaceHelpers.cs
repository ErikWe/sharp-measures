namespace SharpMeasures.SourceGenerators.Utility;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

internal static class InterfaceHelpers
{
    public static INamedTypeSymbol? GetInterfaceImplementationSymbol(INamedTypeSymbol typeSymbol, INamedTypeSymbol interfaceSymbol)
    {
        foreach (INamedTypeSymbol candidateSymbol in typeSymbol.Interfaces)
        {
            if (interfaceSymbol.Equals(candidateSymbol, SymbolEqualityComparer.Default))
            {
                return candidateSymbol;
            }
        }

        return null;
    }

    public static IEnumerable<INamedTypeSymbol> GetUnimplementedInterfaces(INamedTypeSymbol typeSymbol, IEnumerable<INamedTypeSymbol?> interfaceSymbols)
    {
        List<INamedTypeSymbol> unimplementedInterfaces = new();

        foreach (INamedTypeSymbol? interfaceSymbol in interfaceSymbols)
        {
            if (interfaceSymbol is not null && !DoesTypeImplementInterface(typeSymbol, interfaceSymbol))
            {
                unimplementedInterfaces.Add(interfaceSymbol);
            }
        }

        return unimplementedInterfaces;
    }

    public static bool DoesTypeImplementInterface(INamedTypeSymbol typeSymbol, INamedTypeSymbol interfaceSymbol)
    {
        foreach (INamedTypeSymbol candidateSymbol in typeSymbol.Interfaces)
        {
            if (interfaceSymbol.Equals(candidateSymbol, SymbolEqualityComparer.Default))
            {
                return true;
            }
        }

        return false;
    }

    public static bool DoesTypeImplementInterface(INamedTypeSymbol typeSymbol, INamedTypeSymbol interfaceSymbol,
        params ITypeSymbol[] genericArguments)
        => DoesTypeImplementInterface(typeSymbol, interfaceSymbol, (IEnumerable<ITypeSymbol>)genericArguments);

    public static bool DoesTypeImplementInterface(INamedTypeSymbol typeSymbol, INamedTypeSymbol interfaceSymbol,
        IEnumerable<ITypeSymbol> genericArguments)
    {
        if (GetInterfaceImplementationSymbol(typeSymbol, interfaceSymbol) is not INamedTypeSymbol implementation)
        {
            return false;
        }

        int i = 0;
        foreach (ITypeSymbol genericArgument in genericArguments)
        {
            if (i >= implementation.TypeArguments.Length || !implementation.TypeArguments[i].Equals(genericArgument, SymbolEqualityComparer.Default))
            {
                return false;
            }
        }

        return true;
    }
}

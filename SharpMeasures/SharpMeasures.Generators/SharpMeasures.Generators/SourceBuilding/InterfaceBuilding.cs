namespace SharpMeasures.Generators.SourceBuilding;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Text;

internal static class InterfaceBuilding
{
    public static void AppendInterfaceImplementation(StringBuilder source, IEnumerable<INamedTypeSymbol> interfaceSymbols)
    {
        AppendInterfaceImplementation(source, symbolsToDisplayStrings());

        IEnumerable<string> symbolsToDisplayStrings()
        {
            foreach (INamedTypeSymbol interfaceSymbol in interfaceSymbols)
            {
                yield return interfaceSymbol.ToDisplayString();
            }
        }
    }

    public static void AppendInterfaceImplementation(StringBuilder source, IEnumerable<string> interfaceStrings)
        => IterativeBuilding.AppendEnumerable(source, " : ", interfaceStrings, ", ");
}

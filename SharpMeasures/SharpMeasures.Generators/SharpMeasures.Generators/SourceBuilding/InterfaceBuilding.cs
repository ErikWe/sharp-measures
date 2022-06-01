namespace SharpMeasures.Generators.SourceBuilding;

using System.Collections.Generic;
using System.Text;

internal static class InterfaceBuilding
{
    public static void AppendInterfaceImplementation(StringBuilder source, IEnumerable<string> interfaceNames)
        => IterativeBuilding.AppendEnumerable(source, " : ", interfaceNames, ", ");

    public static void InsertInterfaceImplementation(StringBuilder source, int startIndex, IEnumerable<string> interfaceNames)
    {
        StringBuilder interfaces = new();

        AppendInterfaceImplementation(interfaces, interfaceNames);

        source.Insert(startIndex, interfaces);
    }

    public static void InsertInterfaceImplementation(StringBuilder source, int startIndex, params string[] usingsNames)
    {
        InsertInterfaceImplementation(source, startIndex, usingsNames as IEnumerable<string>);
    }
}

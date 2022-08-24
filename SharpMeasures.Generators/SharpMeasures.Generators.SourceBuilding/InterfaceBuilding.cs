namespace SharpMeasures.Generators.SourceBuilding;

using System;
using System.Collections.Generic;
using System.Text;

public static class InterfaceBuilding
{
    public static void AppendInterfaceImplementation(StringBuilder source, IEnumerable<string> interfaceNames)
        => IterativeBuilding.AppendEnumerable(source, " : ", interfaceNames, ", ");

    public static void AppendInterfaceImplementationOnNewLines(StringBuilder source, Indentation indentation, IEnumerable<string> interfaceNames)
    {
        IterativeBuilding.AppendEnumerable(source, $" :{Environment.NewLine}", withIndentation(), $",{Environment.NewLine}");

        IEnumerable<string> withIndentation()
        {
            foreach (string interfaceName in interfaceNames)
            {
                yield return $"{indentation}{interfaceName}";
            }
        }
    }

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

    public static void InsertInterfaceImplementationOnNewLines(StringBuilder source, Indentation indentation, int startIndex, IEnumerable<string> interfaceNames)
    {
        StringBuilder interfaces = new();

        AppendInterfaceImplementationOnNewLines(interfaces, indentation, interfaceNames);

        source.Insert(startIndex, interfaces);
    }

    public static void InsertInterfaceImplementationOnNewLines(StringBuilder source, Indentation indentation, int startIndex, params string[] usingsNames)
    {
        InsertInterfaceImplementationOnNewLines(source, indentation, startIndex, usingsNames as IEnumerable<string>);
    }
}

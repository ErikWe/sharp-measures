namespace SharpMeasures.Generators.SourceBuilding;

using System;
using System.Globalization;
using System.Text;

internal static class SourceBuildingUtility
{
    public static string ToParameterName(string name) => name.Substring(0, 1).ToLower(CultureInfo.CurrentCulture) + name.Substring(1);

    public static void RemoveOneNewLine(StringBuilder source) => source.Remove(source.Length - Environment.NewLine.Length, Environment.NewLine.Length);
}

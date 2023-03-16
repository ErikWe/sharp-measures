namespace SharpMeasures.Generators.SourceBuilding;

using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

public static class SourceBuildingUtility
{
    public static string ToParameterName(string name)
    {
        for (var i = name.Length - 1; i >= 0; i--)
        {
            if (char.IsDigit(name[i]))
            {
                name = name.Substring(0, name.Length - 1);
            }
        }

        return name.Substring(0, 1).ToLower(CultureInfo.CurrentCulture) + name.Substring(1);
    }

    public static string ToQuantityName(string name) => Regex.Replace(name, "([A-Z])", " $1").Trim();

    public static void RemoveOneNewLine(StringBuilder source) => source.Remove(source.Length - Environment.NewLine.Length, Environment.NewLine.Length);
}

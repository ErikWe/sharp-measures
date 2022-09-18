namespace SharpMeasures.Generators.SourceBuilding;

using System;
using System.Globalization;
using System.Text;

public static class SourceBuildingUtility
{
    public static string ToParameterName(string name)
    {
        for (int i = name.Length - 1; i >= 0; i--)
        {
            if (char.IsDigit(name[i]))
            {
                name = name.Substring(0, name.Length - 1);
            }
        }

        return name.Substring(0, 1).ToLower(CultureInfo.CurrentCulture) + name.Substring(1);
    }

    public static void RemoveOneNewLine(StringBuilder source) => source.Remove(source.Length - Environment.NewLine.Length, Environment.NewLine.Length);
}

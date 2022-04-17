namespace SharpMeasures.Generators.SourceBuilding;

using System.Globalization;

internal static class SourceBuildingUtility
{
    public static string ToParameterName(string name) => name.Substring(0, 1).ToLower(CultureInfo.CurrentCulture) + name.Substring(1);
}

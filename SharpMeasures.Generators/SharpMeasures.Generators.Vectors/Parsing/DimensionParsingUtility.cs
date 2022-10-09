namespace SharpMeasures.Generators.Vectors.Parsing;

using System.Globalization;
using System.Text.RegularExpressions;

internal static class DimensionParsingUtility
{
    public static bool CheckVectorDimensionValidity(int dimension) => dimension is >= 2 and <= 4;

    public static int? InterpretDimensionFromName(string name)
    {
        var trailingNumber = Regex.Match(name, @"\d+$", RegexOptions.RightToLeft);

        if (trailingNumber.Success && int.TryParse(trailingNumber.Value, NumberStyles.None, CultureInfo.InvariantCulture, out int result))
        {
            return result;
        }

        return null;
    }

    public static string InterpretNameWithoutDimension(string name)
    {
        var trailingNumber = Regex.Match(name, @"\d+$", RegexOptions.RightToLeft);

        if (trailingNumber.Success)
        {
            return name.Substring(0, name.Length - trailingNumber.Value.Length);
        }

        return name;
    }
}

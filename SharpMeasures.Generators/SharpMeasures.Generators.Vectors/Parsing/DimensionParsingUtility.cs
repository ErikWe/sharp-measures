namespace SharpMeasures.Generators.Vectors.Parsing;

using System.Globalization;
using System.Text.RegularExpressions;

internal static class DimensionParsingUtility
{
    public static bool CheckVectorDimensionValidity(int dimension) => dimension is 2 or 3;

    public static int? InterpretDimensionFromName(string name)
    {
        var trailingNumber = Regex.Match(name, @"\d+$", RegexOptions.RightToLeft);
        if (trailingNumber.Success && int.TryParse(trailingNumber.Value, NumberStyles.None, CultureInfo.InvariantCulture, out int result))
        {
            return result;
        }

        return null;
    }
}

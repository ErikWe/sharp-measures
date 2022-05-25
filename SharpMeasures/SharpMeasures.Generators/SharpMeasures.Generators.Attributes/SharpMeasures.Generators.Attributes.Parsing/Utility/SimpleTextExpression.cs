namespace SharpMeasures.Generators.Attributes.Parsing.Utility;

internal static class SimpleTextExpression
{
    public static string Interpret(string original, string expression)
    {
        if (string.IsNullOrEmpty(expression))
        {
            return string.Empty;
        }

        string[] splitByOpenBracket = expression.Split('[');

        if (splitByOpenBracket.Length is not 2)
        {
            return string.Empty;
        }

        string[] splitByCloseBracket = splitByOpenBracket[1].Split(']');

        if (splitByCloseBracket.Length is not 2)
        {
            return string.Empty;
        }

        string targetText = splitByCloseBracket[0];

        if (targetText is "*")
        {
            targetText = original;
        }    
        else if (original.Contains(targetText) is false)
        {
            return string.Empty;
        }

        string newText = splitByOpenBracket[0] + targetText + splitByCloseBracket[1];

        return original.Replace(targetText, newText);
    }
}

namespace SharpMeasures.Generators.Attributes.Parsing.Utility;

using System.Text.RegularExpressions;

public static class SimpleTextExpression
{
    public static string? Interpret(string original, string expression)
    {
        if (original is null || original.Length is 0 || expression is null || expression.Length is 0)
        {
            return null;
        }

        string[] splitByOpenBracket = expression.Split('[');

        if (splitByOpenBracket.Length is 1)
        {
            return expression;
        }

        if (splitByOpenBracket.Length is not 2)
        {
            return null;
        }

        string[] splitByCloseBracket = splitByOpenBracket[1].Split(']');

        if (splitByCloseBracket.Length is not 2)
        {
            return null;
        }

        string targetText = splitByCloseBracket[0];

        if (targetText is "*")
        {
            targetText = original;
        }    
        else if (original.Contains(targetText) is false)
        {
            return null;
        }

        string newText = splitByOpenBracket[0] + targetText + splitByCloseBracket[1];

        return original.Replace(targetText, newText);
    }

    public static string? Interpret(string original, string expression, string substitution)
    {
        var replaced = new Regex(expression).Replace(original, substitution);

        if (replaced == original)
        {
            return null;
        }

        return replaced;
    }
}

﻿namespace SharpMeasures.Generators.Attributes.Parsing.Utility;

using System.Text.RegularExpressions;

public static class SimpleTextExpression
{
    public static string? Interpret(string original, string expression)
    {
        if (original is null || original.Length is 0 || expression is null || expression.Length is 0)
        {
            return null;
        }

        var splitByOpenBracket = expression.Split('[');

        if (splitByOpenBracket.Length is 1)
        {
            return expression;
        }

        if (splitByOpenBracket.Length is not 2)
        {
            return null;
        }

        var splitByCloseBracket = splitByOpenBracket[1].Split(']');

        if (splitByCloseBracket.Length is not 2)
        {
            return null;
        }

        var targetText = splitByCloseBracket[0];

        if (targetText is "*")
        {
            targetText = original;
        }
        else if (original.Contains(targetText) is false)
        {
            return null;
        }

        var newText = splitByOpenBracket[0] + targetText + splitByCloseBracket[1];

        return original.Replace(targetText, newText);
    }

    public static string? Interpret(string original, string expression, string substitution)
    {
        Regex regex = new(expression);

        var interpreted = regex.Replace(original, substitution);

        if (interpreted != original)
        {
            return interpreted;
        }

        return regex.Match(original).Success ? original : null;
    }
}

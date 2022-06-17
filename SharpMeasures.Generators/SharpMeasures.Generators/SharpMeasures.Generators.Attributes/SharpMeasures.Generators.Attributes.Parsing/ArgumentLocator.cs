namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;

internal static class ArgumentLocator
{
    public static MinimalLocation SimpleArgument(AttributeArgumentListSyntax argumentList, int index)
    {
        if (argumentList.Arguments[index].Expression is TypeOfExpressionSyntax typeofExpression)
        {
            return typeofExpression.Type.GetLocation().Minimize();
        }

        return argumentList.Arguments[index].Expression.GetLocation().Minimize();
    }

    public static MinimalLocation SimpleArgument(SeparatedSyntaxList<ExpressionSyntax> expressions, int index)
    {
        if (expressions[index] is TypeOfExpressionSyntax typeofExpression)
        {
            return typeofExpression.Type.GetLocation().Minimize();
        }

        return expressions[index].GetLocation().Minimize();
    }

    public static (MinimalLocation Collection, IReadOnlyList<MinimalLocation> Elements) FromArrayOrParamsList(AttributeArgumentListSyntax argumentList, int index)
    {
        if (argumentList.Arguments[index].Expression is InitializerExpressionSyntax initializerExpression)
        {
            if (initializerExpression.IsNotKind(SyntaxKind.ArrayInitializerExpression))
            {
                return (argumentList.Arguments[index].Expression.GetLocation().Minimize(), Array.Empty<MinimalLocation>());
            }

            return FromArray(initializerExpression);
        }

        return FromParamsList(argumentList, index);
    }

    private static (MinimalLocation Collection, IReadOnlyList<MinimalLocation> Elements) FromArray(InitializerExpressionSyntax initializerExpression)
    {
        MinimalLocation[] elements = new MinimalLocation[initializerExpression.Expressions.Count];

        for (int i = 0; i < elements.Length; i++)
        {
            elements[i] = SimpleArgument(initializerExpression.Expressions, i);
            elements[i] = initializerExpression.Expressions[i].GetLocation().Minimize();
        }

        return (initializerExpression.GetLocation().Minimize(), elements);
    }

    private static (MinimalLocation Collection, IReadOnlyList<MinimalLocation> Elements) FromParamsList(AttributeArgumentListSyntax argumentList, int startIndex)
    {
        MinimalLocation[] elements = new MinimalLocation[argumentList.Arguments.Count - startIndex];

        for (int i = 0; i < elements.Length; i++)
        {
            elements[i] = argumentList.Arguments[i].GetLocation().Minimize();
        }

        Location firstLocation = argumentList.Arguments[startIndex].GetLocation();
        Location lastLocation = argumentList.Arguments[argumentList.Arguments.Count - 1].GetLocation();

        MinimalLocation signatureLocation = firstLocation.ExtendToInclude(lastLocation).Minimize();

        return (signatureLocation, elements);
    }
}

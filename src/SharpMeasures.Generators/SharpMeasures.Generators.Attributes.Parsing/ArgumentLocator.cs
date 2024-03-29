﻿namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;

internal static class ArgumentLocator
{
    public static MinimalLocation SimpleArgument(AttributeArgumentListSyntax argumentList, int index)
    {
        return SimpleArgument(argumentList.Arguments[index].Expression);
    }

    public static MinimalLocation SimpleArgument(ExpressionSyntax expression)
    {
        if (expression is TypeOfExpressionSyntax typeofExpression)
        {
            return typeofExpression.Type.GetLocation().Minimize();
        }

        if (expression is CastExpressionSyntax castExpression)
        {
            return SimpleArgument(castExpression.Expression);
        }

        if (expression is ParenthesizedExpressionSyntax parenthesizedExpression)
        {
            return SimpleArgument(parenthesizedExpression.Expression);
        }

        return expression.GetLocation().Minimize();
    }

    public static MinimalLocation SimpleArgument(SeparatedSyntaxList<ExpressionSyntax> expressions, int index)
    {
        if (expressions[index] is TypeOfExpressionSyntax typeofExpression)
        {
            return typeofExpression.Type.GetLocation().Minimize();
        }

        return expressions[index].GetLocation().Minimize();
    }

    public static (MinimalLocation Collection, IReadOnlyList<MinimalLocation> Elements) FromArrayOrParamsList(AttributeArgumentListSyntax argumentList, int index, bool definitelyParams)
    {
        if (definitelyParams)
        {
            return FromParamsList(argumentList, index);
        }

        if (AttemptFromArray(argumentList.Arguments[index].Expression) is var locatedArray and not null)
        {
            return locatedArray.Value;
        }

        return FromParamsList(argumentList, index);
    }

    private static (MinimalLocation Collection, IReadOnlyList<MinimalLocation> Elements)? AttemptFromArray(ExpressionSyntax expression)
    {
        if (expression is ArrayCreationExpressionSyntax arrayCreationExpression)
        {
            if (arrayCreationExpression.Initializer is not null)
            {
                return FromArray(arrayCreationExpression.Initializer);
            }

            return (arrayCreationExpression.Type.GetLocation().Minimize(), Array.Empty<MinimalLocation>());
        }

        if (expression is InitializerExpressionSyntax initializerExpression)
        {
            if (initializerExpression.IsNotKind(SyntaxKind.ArrayInitializerExpression))
            {
                return (initializerExpression.GetLocation().Minimize(), Array.Empty<MinimalLocation>());
            }

            return FromArray(initializerExpression);
        }

        if (expression is ImplicitArrayCreationExpressionSyntax implicitArrayCreationExpression)
        {
            return FromArray(implicitArrayCreationExpression.Initializer);
        }

        if (expression is CastExpressionSyntax castExpression)
        {
            if (castExpression.Type is ArrayTypeSyntax)
            {
                return AttemptFromArray(castExpression.Expression);
            }

            return null;
        }

        if (expression is LiteralExpressionSyntax && expression.IsKind(SyntaxKind.NullLiteralExpression))
        {
            return (expression.GetLocation().Minimize(), Array.Empty<MinimalLocation>());
        }

        if (expression is ParenthesizedExpressionSyntax parenthesizedExpression)
        {
            return AttemptFromArray(parenthesizedExpression.Expression);
        }

        return null;
    }

    private static (MinimalLocation Collection, IReadOnlyList<MinimalLocation> Elements) FromArray(InitializerExpressionSyntax initializerExpression)
    {
        var elements = new MinimalLocation[initializerExpression.Expressions.Count];

        for (var i = 0; i < elements.Length; i++)
        {
            elements[i] = SimpleArgument(initializerExpression.Expressions, i);
        }

        return (initializerExpression.GetLocation().Minimize(), elements);
    }

    private static (MinimalLocation Collection, IReadOnlyList<MinimalLocation> Elements) FromParamsList(AttributeArgumentListSyntax argumentList, int startIndex)
    {
        var elements = new MinimalLocation[argumentList.Arguments.Count - startIndex];

        for (var i = 0; i < elements.Length; i++)
        {
            elements[i] = SimpleArgument(argumentList, startIndex + i);
        }

        var firstLocation = argumentList.Arguments[startIndex].GetLocation();
        var lastLocation = argumentList.Arguments[argumentList.Arguments.Count - 1].GetLocation();

        var signatureLocation = firstLocation.ExtendToInclude(lastLocation).Minimize();

        return (signatureLocation, elements);
    }
}

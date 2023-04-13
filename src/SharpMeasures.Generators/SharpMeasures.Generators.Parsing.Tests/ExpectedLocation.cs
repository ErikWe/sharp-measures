namespace SharpMeasures.Generators.Parsing.Tests;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;
using System.Linq;

internal static class ExpectedLocation
{
    public static Location TypeArgument(AttributeSyntax syntax, int index)
    {
        if (syntax.Name is QualifiedNameSyntax qualifiedName && qualifiedName.Right is GenericNameSyntax qualifiedGenericName)
        {
            return TypeArgument(qualifiedGenericName, index);
        }

        if (syntax.Name is GenericNameSyntax genericName)
        {
            return TypeArgument(genericName, index);
        }

        throw new ArgumentException($"The provided {nameof(AttributeSyntax)} does not describe a generic attribute.", nameof(syntax));
    }

    public static Location SingleArgument(AttributeSyntax syntax, int index)
    {
        if (syntax.ArgumentList is null)
        {
            throw new ArgumentException($"The provided {nameof(AttributeSyntax)} does not describe an attribute with arguments.", nameof(syntax));
        }

        if (syntax.ArgumentList.Arguments.Count < index + 1)
        {
            throw new ArgumentException($"The attribute described by the provided {nameof(AttributeSyntax)} does not include at least {index + 1} arguments.");
        }

        return DependencyInjection.GetRequiredService<IArgumentLocator>().SingleArgument(syntax.ArgumentList.Arguments[index].Expression);
    }

    public static (Location Collection, IReadOnlyList<Location> Elements) ArrayArgument(AttributeSyntax syntax, int index)
    {
        if (syntax.ArgumentList is null)
        {
            throw new ArgumentException($"The provided {nameof(AttributeSyntax)} does not describe an attribute with arguments.", nameof(syntax));
        }

        if (syntax.ArgumentList.Arguments.Count < index + 1)
        {
            throw new ArgumentException($"The attribute described by the provided {nameof(AttributeSyntax)} does not include at least {index + 1} arguments.");
        }

        return DependencyInjection.GetRequiredService<IArgumentLocator>().ArrayArgument(syntax.ArgumentList.Arguments[index].Expression);
    }

    public static (Location Collection, IReadOnlyList<Location> Elements) ParamsArgument(AttributeSyntax syntax, int firstIndex, int count)
    {
        if (syntax.ArgumentList is null)
        {
            throw new ArgumentException($"The provided {nameof(AttributeSyntax)} does not describe an attribute with arguments.", nameof(syntax));
        }

        if (syntax.ArgumentList.Arguments.Count < firstIndex + count)
        {
            throw new ArgumentException($"The attribute described by the provided {nameof(AttributeSyntax)} does not include at least {firstIndex + count} arguments.");
        }

        return DependencyInjection.GetRequiredService<IArgumentLocator>().ParamsArguments(syntax.ArgumentList.Arguments.Skip(firstIndex).Select(static (argument) => argument.Expression).ToList());
    }

    private static Location TypeArgument(GenericNameSyntax genericNameSyntax, int index)
    {
        return DependencyInjection.GetRequiredService<IArgumentLocator>().TypeArgument(genericNameSyntax.TypeArgumentList.Arguments[index]);
    }
}

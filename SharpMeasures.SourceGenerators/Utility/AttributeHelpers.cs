namespace ErikWe.SharpMeasures.SourceGenerators.Utility;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;
using System.Collections.Immutable;

internal static class AttributeHelpers
{
    public static AttributeData? ExtractAttributeData(INamedTypeSymbol typeSymbol, AttributeSyntax attributeSyntax)
    {
        SyntaxReference reference = attributeSyntax.GetReference();
        foreach (AttributeData attributeData in typeSymbol.GetAttributes())
        {
            if (reference.SyntaxTree == attributeData.ApplicationSyntaxReference?.SyntaxTree
                && reference.Span == attributeData.ApplicationSyntaxReference?.Span)
            {
                return attributeData;
            }
        }

        return null;
    }

    public static (bool success, TParameters values) ParseAttributeParameters<TParameters>(AttributeData attributeData, TParameters values,
        Dictionary<string, AttributeProperty<TParameters>> constructorArguments, Dictionary<string, AttributeProperty<TParameters>> namedArguments)
    {
        (bool success, values) = AddConstructorArguments(attributeData, values, constructorArguments);

        if (success)
        {
            (success, values) = AddNamedArguments(attributeData, values, namedArguments);

            if (success)
            {
                return (true, values);
            }
        }

        return (false, values);
    }

    private static (bool success, TParameters values) AddConstructorArguments<TParameters>(AttributeData attributeData, TParameters values,
        Dictionary<string, AttributeProperty<TParameters>> constructorArguments)
    {
        if (!attributeData.ConstructorArguments.IsEmpty)
        {
            if (attributeData.AttributeConstructor?.Parameters is not ImmutableArray<IParameterSymbol> parameterSymbols)
            {
                return (false, values);
            }

            for (int i = 0; i < parameterSymbols.Length; i++)
            {
                if (attributeData.ConstructorArguments[i].Kind == TypedConstantKind.Error)
                {
                    return (false, values);
                }

                if (attributeData.ConstructorArguments[i].Kind == TypedConstantKind.Array)
                {
                    if (constructorArguments.TryGetValue(parameterSymbols[i].Name, out AttributeProperty<TParameters> property))
                    {
                        ImmutableArray<TypedConstant> arrayElements = attributeData.ConstructorArguments[i].Values;
                        object?[] objs = new object[arrayElements.Length];
                        for (int j = 0; j < arrayElements.Length; j++)
                        {
                            objs[j] = arrayElements[j].Value;
                        }

                        values = property.Setter(values, objs);
                    }
                }
                else if (parameterSymbols[i].IsParams)
                {
                    if (constructorArguments.TryGetValue(parameterSymbols[i].Name, out AttributeProperty<TParameters> property))
                    {
                        object?[] objs = new object[attributeData.ConstructorArguments.Length - i];
                        for (int j = 0; j < attributeData.ConstructorArguments.Length - i; j++)
                        {
                            objs[j] = attributeData.ConstructorArguments[i + j].Value;
                        }

                        values = property.Setter(values, objs);
                    }
                }
                else
                {
                    if (constructorArguments.TryGetValue(parameterSymbols[i].Name, out AttributeProperty<TParameters> property)
                        && attributeData.ConstructorArguments[i].Value is object obj
                        && property.Type.IsAssignableFrom(obj.GetType()))
                    {
                        values = property.Setter(values, obj);
                    }
                }
            }
        }

        return (true, values);
    }

    private static (bool success, TParameters values) AddNamedArguments<TParameters>(AttributeData attributeData, TParameters values,
        Dictionary<string, AttributeProperty<TParameters>> namedArguments)
    {
        if (!attributeData.NamedArguments.IsEmpty)
        {
            foreach (KeyValuePair<string, TypedConstant> argument in attributeData.NamedArguments)
            {
                if (argument.Value.Kind == TypedConstantKind.Error)
                {
                    return (false, values);
                }
                else
                {
                    if (namedArguments.TryGetValue(argument.Key, out AttributeProperty<TParameters> property)
                        && argument.Value.Value is object obj
                        && obj.GetType() == property.Type)
                    {
                        values = property.Setter(values, obj);
                    }
                }
            }
        }

        return (true, values);
    }
}

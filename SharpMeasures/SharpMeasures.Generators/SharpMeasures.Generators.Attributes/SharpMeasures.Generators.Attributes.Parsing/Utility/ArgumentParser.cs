﻿namespace SharpMeasures.Generators.Attributes.Parsing.Utility;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

internal static class ArgumentParser
{
    public static (bool success, TParameters values) Parse<TParameters>(AttributeData attributeData, TParameters values,
        Dictionary<string, AttributeProperty<TParameters>> constructorParameters, Dictionary<string, AttributeProperty<TParameters>> namedParameters)
    {
        (bool success, values) = AddConstructorArguments(attributeData, values, constructorParameters);

        if (success)
        {
            (success, values) = AddNamedArguments(attributeData, values, namedParameters);

            if (success)
            {
                return (true, values);
            }
        }

        return (false, values);
    }

    private static (bool success, TParameters values) AddConstructorArguments<TParameters>(AttributeData attributeData, TParameters values,
        Dictionary<string, AttributeProperty<TParameters>> constructorParameters)
    {
        if (attributeData.ConstructorArguments.IsEmpty)
        {
            return (true, values);
        }

        if (attributeData.AttributeConstructor?.Parameters is not ImmutableArray<IParameterSymbol> parameterSymbols)
        {
            return (false, values);
        }

        for (int i = 0; i < parameterSymbols.Length; i++)
        {
            if (attributeData.ConstructorArguments[i].Kind == TypedConstantKind.Error
                || !constructorParameters.TryGetValue(parameterSymbols[i].Name, out AttributeProperty<TParameters> property))
            {
                continue;
            }

            if (attributeData.ConstructorArguments[i].Kind == TypedConstantKind.Array)
            {
                ImmutableArray<TypedConstant> arrayElements = attributeData.ConstructorArguments[i].Values;
                object?[] objs = new object[arrayElements.Length];
                for (int j = 0; j < arrayElements.Length; j++)
                {
                    objs[j] = arrayElements[j].Value;
                }

                values = property.Setter(values, objs);
            }
            else if (parameterSymbols[i].IsParams)
            {
                object?[] objs = new object[attributeData.ConstructorArguments.Length - i];
                for (int j = 0; j < attributeData.ConstructorArguments.Length - i; j++)
                {
                    objs[j] = attributeData.ConstructorArguments[i + j].Value;
                }

                values = property.Setter(values, objs);
            }
            else
            {
                values = property.Setter(values, attributeData.ConstructorArguments[i].Value);
            }
        }

        return (true, values);
    }

    private static (bool success, TParameters values) AddNamedArguments<TParameters>(AttributeData attributeData, TParameters values,
        Dictionary<string, AttributeProperty<TParameters>> namedParameters)
    {
        if (attributeData.NamedArguments.IsEmpty)
        {
            return (true, values);
        }

        foreach (KeyValuePair<string, TypedConstant> argument in attributeData.NamedArguments)
        {
            if (argument.Value.Kind == TypedConstantKind.Error
                || !namedParameters.TryGetValue(argument.Key, out AttributeProperty<TParameters> property))
            {
                continue;
            }

            values = property.Setter(values, argument.Value.Value);
        }

        return (true, values);
    }
}
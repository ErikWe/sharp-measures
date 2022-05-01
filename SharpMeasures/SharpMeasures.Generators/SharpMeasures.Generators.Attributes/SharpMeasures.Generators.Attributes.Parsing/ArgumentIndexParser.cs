namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

internal static class ArgumentIndexParser
{
    public static int Parse<TParameters>(AttributeProperty<TParameters> property, AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        return new ArgumentIndexParser<TParameters>(property).Parse(attributeData);
    }
}

public class ArgumentIndexParser<TParameters>
{
    private AttributeProperty<TParameters> Property { get; }

    internal ArgumentIndexParser(AttributeProperty<TParameters> property)
    {
        Property = property;
    }

    public int Parse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (CheckConstructor(attributeData) is int index and not -1)
        {
            return index;
        }

        return CheckNamedArguments(attributeData);
    }

    public IEnumerable<int> Parse(IEnumerable<AttributeData> attributeDataIterator)
    {
        if (attributeDataIterator is null)
        {
            throw new ArgumentNullException(nameof(attributeDataIterator));
        }

        foreach (AttributeData attributeData in attributeDataIterator)
        {
            yield return Parse(attributeData);
        }
    }
    
    private int CheckConstructor(AttributeData attributeData)
    {
        if (attributeData.ConstructorArguments.IsEmpty
            || attributeData.AttributeConstructor?.Parameters is not ImmutableArray<IParameterSymbol> parameterSymbols)
        {
            return -1;
        }

        for (int i = 0; i < parameterSymbols.Length; i++)
        {
            if (parameterSymbols[i].Name == Property.ParameterName)
            {
                return i;
            }
        }

        return -1;
    }

    private int CheckNamedArguments(AttributeData attributeData)
    {
        if (attributeData.NamedArguments.IsEmpty)
        {
            return -1;
        }

        for (int i = 0; i < attributeData.NamedArguments.Length; i++)
        {
            if (attributeData.NamedArguments[i].Key == Property.Name)
            {
                return i + (attributeData.AttributeConstructor?.Parameters.Length ?? 0);
            }
        }

        return -1;
    }
}

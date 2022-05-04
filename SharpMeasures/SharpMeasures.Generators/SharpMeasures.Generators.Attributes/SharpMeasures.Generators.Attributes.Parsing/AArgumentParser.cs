namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

public abstract class AArgumentParser<TParameters>
{
    private Func<TParameters> DefaultValueConstructor { get; }

    private IReadOnlyDictionary<string, AttributeProperty<TParameters>> ConstructorParameters { get; }
    private IReadOnlyDictionary<string, AttributeProperty<TParameters>> NamedParameters { get; }

    protected AArgumentParser(Func<TParameters> defaultValueConstructor, IReadOnlyDictionary<string, AttributeProperty<TParameters>> constructorParameters,
        IReadOnlyDictionary<string, AttributeProperty<TParameters>> namedParameters)
    {
        DefaultValueConstructor = defaultValueConstructor;

        ConstructorParameters = constructorParameters;
        NamedParameters = namedParameters;
    }

    protected AArgumentParser(Func<TParameters> defaultValueConstructor, IEnumerable<AttributeProperty<TParameters>> properties)
        : this(defaultValueConstructor, properties.ToImmutableDictionary(static (x) => x.ParameterName), properties.ToImmutableDictionary(static (x) => x.Name)) { }

    public TParameters Parse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        TParameters parameters = DefaultValueConstructor();

        parameters = AddConstructorArguments(parameters, attributeData);
        parameters = AddNamedArguments(parameters, attributeData);
        return parameters;
    }

    public IEnumerable<TParameters> Parse(IEnumerable<AttributeData> attributeDataIterator)
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

    public IEnumerable<TParameters> Parse<TAttribute>(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse(typeSymbol.GetAttributesOfType<TAttribute>());
    }

    public abstract IEnumerable<TParameters> Parse(INamedTypeSymbol typeSymbol);

    private TParameters AddConstructorArguments(TParameters parameters, AttributeData attributeData)
    {
        if (attributeData.ConstructorArguments.IsEmpty
            || attributeData.AttributeConstructor?.Parameters is not ImmutableArray<IParameterSymbol> parameterSymbols)
        {
            return parameters;
        }

        for (int i = 0; i < parameterSymbols.Length && i < attributeData.ConstructorArguments.Length; i++)
        {
            if (attributeData.ConstructorArguments[i].Kind is TypedConstantKind.Error
                || !ConstructorParameters.TryGetValue(parameterSymbols[i].Name, out AttributeProperty<TParameters> property))
            {
                continue;
            }

            if (attributeData.ConstructorArguments[i].Kind is TypedConstantKind.Array)
            {
                parameters = property.Setter(parameters, ParseArray(attributeData.ConstructorArguments[i]));
            }
            else if (parameterSymbols[i].IsParams)
            {
                parameters = property.Setter(parameters, ParseParams(attributeData.ConstructorArguments, i));
            }
            else
            {
                parameters = property.Setter(parameters, attributeData.ConstructorArguments[i].Value);
            }
        }

        return parameters;
    }

    private TParameters AddNamedArguments(TParameters parameters, AttributeData attributeData)
    {
        if (attributeData.NamedArguments.IsEmpty)
        {
            return parameters;
        }

        foreach (KeyValuePair<string, TypedConstant> argument in attributeData.NamedArguments)
        {
            if (argument.Value.Kind is TypedConstantKind.Error
                || !NamedParameters.TryGetValue(argument.Key, out AttributeProperty<TParameters> property))
            {
                continue;
            }

            if (argument.Value.Kind is TypedConstantKind.Array)
            {
                parameters = property.Setter(parameters, ParseArray(argument.Value));
            }
            else
            {
                parameters = property.Setter(parameters, argument.Value.Value);
            }
        }

        return parameters;
    }

    protected static int IndexOfArgument(AttributeProperty<TParameters> property, AttributeData attributeData)
        => ArgumentIndexParser.Parse(property, attributeData);

    private static object?[] ParseArray(TypedConstant value)
    {
        ImmutableArray<TypedConstant> arrayConstants = value.Values;
        object?[] arrayValues = new object[arrayConstants.Length];
        for (int j = 0; j < arrayConstants.Length; j++)
        {
            arrayValues[j] = arrayConstants[j].Value;
        }

        return arrayValues;
    }

    private static object?[] ParseParams(IList<TypedConstant> values, int startIndex)
    {
        object?[] paramsValues = new object[values.Count - startIndex];
        for (int i = 0; i < values.Count - startIndex; i++)
        {
            paramsValues[i] = values[startIndex + i].Value;
        }

        return paramsValues;
    }
}
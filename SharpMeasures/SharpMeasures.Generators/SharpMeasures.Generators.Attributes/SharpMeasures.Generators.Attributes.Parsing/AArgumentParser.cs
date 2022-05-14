namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

public abstract class AArgumentParser<TDefinition>
{
    private Func<TDefinition> DefaultValueConstructor { get; }

    private IReadOnlyDictionary<string, AttributeProperty<TDefinition>> ConstructorParameters { get; }
    private IReadOnlyDictionary<string, AttributeProperty<TDefinition>> NamedParameters { get; }

    protected AArgumentParser(Func<TDefinition> defaultValueConstructor, IReadOnlyDictionary<string, AttributeProperty<TDefinition>> constructorParameters,
        IReadOnlyDictionary<string, AttributeProperty<TDefinition>> namedParameters)
    {
        DefaultValueConstructor = defaultValueConstructor;

        ConstructorParameters = constructorParameters;
        NamedParameters = namedParameters;
    }

    protected AArgumentParser(Func<TDefinition> defaultValueConstructor, IEnumerable<AttributeProperty<TDefinition>> properties)
        : this(defaultValueConstructor, properties.ToImmutableDictionary(static (x) => x.ParameterName), properties.ToImmutableDictionary(static (x) => x.Name)) { }

    public TDefinition Parse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeData.GetSyntax() is not AttributeSyntax attributeSyntax)
        {
            throw new InvalidOperationException("Could not retrieve AttributeSyntax from AttributeData");
        }

        TDefinition parameters = DefaultValueConstructor();

        parameters = AddConstructorArguments(parameters, attributeData, attributeSyntax);
        parameters = AddNamedArguments(parameters, attributeData, attributeSyntax);
        parameters = AddCustomData(parameters, attributeData, attributeSyntax);
        return parameters;
    }

    public IEnumerable<TDefinition> Parse(IEnumerable<AttributeData> attributeDataIterator)
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

    public IEnumerable<TDefinition> Parse<TAttribute>(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse(typeSymbol.GetAttributesOfType<TAttribute>());
    }

    public abstract IEnumerable<TDefinition> Parse(INamedTypeSymbol typeSymbol);

    private TDefinition AddConstructorArguments(TDefinition parameters, AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData.ConstructorArguments.IsEmpty
            || attributeData.AttributeConstructor?.Parameters is not ImmutableArray<IParameterSymbol> parameterSymbols)
        {
            return parameters;
        }

        for (int i = 0; i < parameterSymbols.Length && i < attributeData.ConstructorArguments.Length; i++)
        {
            if (attributeData.ConstructorArguments[i].Kind is TypedConstantKind.Error
                || !ConstructorParameters.TryGetValue(parameterSymbols[i].Name, out AttributeProperty<TDefinition> property))
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

            parameters = property.SyntaxSetter(parameters, attributeSyntax.ArgumentList!, i);
        }

        return parameters;
    }

    private TDefinition AddNamedArguments(TDefinition parameters, AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData.NamedArguments.IsEmpty)
        {
            return parameters;
        }

        for (int i = 0; i < attributeData.NamedArguments.Length; i++)
        {
            if (attributeData.NamedArguments[i].Value.Kind is TypedConstantKind.Error
                || !NamedParameters.TryGetValue(attributeData.NamedArguments[i].Key, out AttributeProperty<TDefinition> property))
            {
                continue;
            }

            if (attributeData.NamedArguments[i].Value.Kind is TypedConstantKind.Array)
            {
                parameters = property.Setter(parameters, ParseArray(attributeData.NamedArguments[i].Value));
            }
            else
            {
                parameters = property.Setter(parameters, attributeData.NamedArguments[i].Value.Value);
            }

            parameters = property.SyntaxSetter(parameters, attributeSyntax.ArgumentList!, attributeData.ConstructorArguments.Length + i);
        }

        return parameters;
    }

    private protected virtual TDefinition AddCustomData(TDefinition parameters, AttributeData attributeData, AttributeSyntax attributeSyntax) => parameters;

    protected static int IndexOfArgument(AttributeProperty<TDefinition> property, AttributeData attributeData)
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
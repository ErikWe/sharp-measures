namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

public interface IAttributeParser<TDefinition>
{
    public abstract TDefinition Parse(AttributeData attributeData);
    public abstract IEnumerable<TDefinition> Parse(IEnumerable<AttributeData> attributeData);

    public abstract TDefinition? ParseFirstOccurrence(INamedTypeSymbol typeSymbol);
    public abstract IEnumerable<TDefinition> ParseAllOccurrences(INamedTypeSymbol typeSymbol);
}

internal abstract class AAttributeParser<TDefinition, TParsingData, TLocations, TAttribute> : AAttributeParser<TDefinition, TParsingData, TLocations>
    where TDefinition : AAttributeDefinition<TParsingData, TLocations>
    where TParsingData : AAttributeParsingData<TLocations>
    where TLocations : AAttributeLocations
{
    protected override Type AttributeType { get; } = typeof(TAttribute);

    protected AAttributeParser(Func<TDefinition> defaultValueConstructor, IReadOnlyDictionary<string, IAttributeProperty<TDefinition>> constructorParameters,
        IReadOnlyDictionary<string, IAttributeProperty<TDefinition>> namedParameters)
        : base(defaultValueConstructor, constructorParameters, namedParameters) { }

    protected AAttributeParser(Func<TDefinition> defaultValueConstructor, IEnumerable<IAttributeProperty<TDefinition>> properties)
        : base(defaultValueConstructor, properties) { }
}

internal abstract class AAttributeParser<TDefinition, TParsingData, TLocations> : IAttributeParser<TDefinition>
    where TDefinition : AAttributeDefinition<TParsingData, TLocations>
    where TParsingData : AAttributeParsingData<TLocations>
    where TLocations : AAttributeLocations
{
    protected abstract Type AttributeType { get; }

    private Func<TDefinition> DefaultValueConstructor { get; }

    private IReadOnlyDictionary<string, IAttributeProperty<TDefinition>> ConstructorParameters { get; }
    private IReadOnlyDictionary<string, IAttributeProperty<TDefinition>> NamedParameters { get; }

    protected AAttributeParser(Func<TDefinition> defaultValueConstructor, IReadOnlyDictionary<string, IAttributeProperty<TDefinition>> constructorParameters,
        IReadOnlyDictionary<string, IAttributeProperty<TDefinition>> namedParameters)
    {
        DefaultValueConstructor = defaultValueConstructor;

        ConstructorParameters = constructorParameters;
        NamedParameters = namedParameters;
    }

    protected AAttributeParser(Func<TDefinition> defaultValueConstructor, IEnumerable<IAttributeProperty<TDefinition>> properties)
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

        TDefinition definition = DefaultValueConstructor();

        definition = AddAttributeLocation(definition, attributeSyntax.GetLocation().Minimize());
        definition = AddConstructorArguments(definition, attributeData, attributeSyntax);
        definition = AddNamedArguments(definition, attributeData, attributeSyntax);
        definition = AddCustomData(definition, attributeData, attributeSyntax);

        return definition;
    }

    public IEnumerable<TDefinition> Parse(IEnumerable<AttributeData> attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        foreach (AttributeData data in attributeData)
        {
            yield return Parse(data);
        }
    }

    public TDefinition? ParseFirstOccurrence(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        if (typeSymbol.GetAttributeOfType(AttributeType) is AttributeData attributeData)
        {
            return Parse(attributeData);
        }

        return null;
    }

    public IEnumerable<TDefinition> ParseAllOccurrences(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        return Parse(typeSymbol.GetAttributesOfType(AttributeType));
    }

    protected virtual TDefinition AddCustomData(TDefinition definition, AttributeData attributeData, AttributeSyntax attributeSyntax) => definition;

    private TDefinition AddConstructorArguments(TDefinition definition, AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData.ConstructorArguments.IsEmpty
            || attributeData.AttributeConstructor?.Parameters is not ImmutableArray<IParameterSymbol> parameterSymbols)
        {
            return definition;
        }

        for (int i = 0; i < parameterSymbols.Length && i < attributeData.ConstructorArguments.Length; i++)
        {
            if (attributeData.ConstructorArguments[i].Kind is TypedConstantKind.Error
                || ConstructorParameters.TryGetValue(parameterSymbols[i].Name, out IAttributeProperty<TDefinition> property) is false)
            {
                continue;
            }

            definition = SetConstructorArgument(definition, property, parameterSymbols[i], attributeData.ConstructorArguments, i);
            definition = property.Locator(definition, attributeSyntax.ArgumentList!, i);

        }

        return definition;
    }

    private TDefinition AddNamedArguments(TDefinition definition, AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData.NamedArguments.IsEmpty)
        {
            return definition;
        }

        for (int i = 0; i < attributeData.NamedArguments.Length; i++)
        {
            if (attributeData.NamedArguments[i].Value.Kind is TypedConstantKind.Error
                || NamedParameters.TryGetValue(attributeData.NamedArguments[i].Key, out IAttributeProperty<TDefinition> property) is false)
            {
                continue;
            }

            definition = SetNamedArgument(definition, property, attributeData.NamedArguments[i].Value);
            definition = property.Locator(definition, attributeSyntax.ArgumentList!, attributeData.ConstructorArguments.Length + i);
        }

        return definition;
    }

    private static TDefinition SetConstructorArgument(TDefinition definition, IAttributeProperty<TDefinition> property,
        IParameterSymbol parameterSymbol, IReadOnlyList<TypedConstant> arguments, int argumentIndex)
    {
        if (arguments[argumentIndex].Kind is TypedConstantKind.Array)
        {
            return property.Setter(definition, ParseArray(arguments[argumentIndex]));
        }
        
        if (parameterSymbol.IsParams)
        {
            return property.Setter(definition, ParseParams(arguments, argumentIndex));
        }

        return property.Setter(definition, arguments[argumentIndex].Value);
    }

    private static TDefinition SetNamedArgument(TDefinition definition, IAttributeProperty<TDefinition> property, TypedConstant value)
    {
        if (value.Kind is TypedConstantKind.Array)
        {
            return property.Setter(definition, ParseArray(value));
        }

        return property.Setter(definition, value.Value);
    }

    private static TDefinition AddAttributeLocation(TDefinition definition, MinimalLocation attributeLocation)
    {
        return definition with
        {
            ParsingData = definition.ParsingData with
            {
                Locations = definition.ParsingData.Locations with
                {
                    Attribute = attributeLocation
                }
            }
        };
    }

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

    private static object?[] ParseParams(IReadOnlyList<TypedConstant> values, int startIndex)
    {
        object?[] paramsValues = new object[values.Count - startIndex];
        for (int i = 0; i < values.Count - startIndex; i++)
        {
            paramsValues[i] = values[startIndex + i].Value;
        }

        return paramsValues;
    }
}

namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

public interface IAttributeParser<TDefinition>
{
    public abstract TDefinition? Parse(AttributeData attributeData);
    public abstract IEnumerable<TDefinition> Parse(IEnumerable<AttributeData> attributeData);

    public abstract TDefinition? ParseFirstOccurrence(INamedTypeSymbol typeSymbol);
    public abstract IEnumerable<TDefinition> ParseAllOccurrences(INamedTypeSymbol typeSymbol);
}

public abstract class AAttributeParser<TDefinition, TLocations, TAttribute> : AAttributeParser<TDefinition, TLocations>
    where TDefinition : IOpenAttributeDefinition<TDefinition, TLocations>
    where TLocations : IOpenAttributeLocations<TLocations>
{
    protected override Type AttributeType { get; } = typeof(TAttribute);

    protected AAttributeParser(Func<TDefinition> defaultValueConstructor, IReadOnlyDictionary<string, IAttributeProperty<TDefinition>> constructorParameters, IReadOnlyDictionary<string, IAttributeProperty<TDefinition>> namedParameters)
        : base(defaultValueConstructor, constructorParameters, namedParameters) { }

    protected AAttributeParser(Func<TDefinition> defaultValueConstructor, IEnumerable<IAttributeProperty<TDefinition>> properties)
        : base(defaultValueConstructor, properties) { }
}

public abstract class AAttributeParser<TDefinition, TLocations> : IAttributeParser<TDefinition>
    where TDefinition : IOpenAttributeDefinition<TDefinition, TLocations>
    where TLocations : IOpenAttributeLocations<TLocations>
{
    protected abstract Type AttributeType { get; }

    private Func<TDefinition> DefaultValueConstructor { get; }

    private IReadOnlyDictionary<string, IAttributeProperty<TDefinition>> ConstructorParameters { get; }
    private IReadOnlyDictionary<string, IAttributeProperty<TDefinition>> NamedParameters { get; }

    protected AAttributeParser(Func<TDefinition> defaultValueConstructor, IReadOnlyDictionary<string, IAttributeProperty<TDefinition>> constructorParameters, IReadOnlyDictionary<string, IAttributeProperty<TDefinition>> namedParameters)
    {
        DefaultValueConstructor = defaultValueConstructor;

        ConstructorParameters = constructorParameters;
        NamedParameters = namedParameters;
    }

    protected AAttributeParser(Func<TDefinition> defaultValueConstructor, IEnumerable<IAttributeProperty<TDefinition>> properties)
        : this(defaultValueConstructor, properties.ToImmutableDictionary(static (x) => x.ParameterName), properties.ToImmutableDictionary(static (x) => x.Name)) { }

    public TDefinition? Parse(AttributeData attributeData)
    {
        if (attributeData.GetSyntax() is not AttributeSyntax attributeSyntax)
        {
            throw new InvalidOperationException("Could not retrieve AttributeSyntax from AttributeData");
        }

        TDefinition definition = DefaultValueConstructor();

        if (attributeData.AttributeConstructor?.Parameters is not ImmutableArray<IParameterSymbol> parameterSymbols)
        {
            return default;
        }

        definition = AddAttributeLocation(definition, attributeSyntax);
        definition = AddConstructorArguments(definition, attributeData, attributeSyntax, parameterSymbols);
        definition = AddNamedArguments(definition, attributeData, attributeSyntax, parameterSymbols);
        definition = AddCustomData(definition, attributeData, attributeSyntax, parameterSymbols);

        return definition;
    }

    public IEnumerable<TDefinition> Parse(IEnumerable<AttributeData> attributeData)
    {
        foreach (AttributeData data in attributeData)
        {
            if (Parse(data) is TDefinition definition)
            {
                yield return definition;
            }
        }
    }

    public TDefinition? ParseFirstOccurrence(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol.GetAttributeOfType(AttributeType) is AttributeData attributeData)
        {
            return Parse(attributeData);
        }

        return default;
    }

    public IEnumerable<TDefinition> ParseAllOccurrences(INamedTypeSymbol typeSymbol)
    {
        return Parse(typeSymbol.GetAttributesOfType(AttributeType));
    }

    protected virtual TDefinition AddCustomData(TDefinition definition, AttributeData attributeData, AttributeSyntax attributeSyntax, ImmutableArray<IParameterSymbol> parameterSymbols) => definition;
 
    private TDefinition AddConstructorArguments(TDefinition definition, AttributeData attributeData, AttributeSyntax attributeSyntax, ImmutableArray<IParameterSymbol> parameterSymbols)
    {
        if (attributeData.ConstructorArguments.IsEmpty)
        {
            return definition;
        }

        for (int i = 0; i < parameterSymbols.Length && i < attributeData.ConstructorArguments.Length && i < attributeSyntax.ArgumentList?.Arguments.Count; i++)
        {
            if (attributeData.ConstructorArguments[i].Kind is TypedConstantKind.Error || ConstructorParameters.TryGetValue(parameterSymbols[i].Name, out IAttributeProperty<TDefinition> property) is false)
            {
                continue;
            }

            if (i == parameterSymbols.Length - 1 && parameterSymbols.Length < attributeData.ConstructorArguments.Length)
            {
                definition = SetLastConstructorArgument(definition, property, attributeData.ConstructorArguments, i);
                definition = property.Locator(definition, attributeSyntax.ArgumentList!, i, true);

                break;
            }

            bool definitelyParams = parameterSymbols[i].IsParams && attributeSyntax.ArgumentList!.Arguments.Count > i + 1 && attributeSyntax.ArgumentList!.Arguments[i + 1].NameColon is null;

            definition = SetConstructorArgument(definition, property, attributeData.ConstructorArguments, i);
            definition = property.Locator(definition, attributeSyntax.ArgumentList!, i, definitelyParams);
        }

        return definition;
    }

    private TDefinition AddNamedArguments(TDefinition definition, AttributeData attributeData, AttributeSyntax attributeSyntax, ImmutableArray<IParameterSymbol> parameterSymbols)
    {
        if (attributeData.NamedArguments.IsEmpty)
        {
            return definition;
        }

        int argumentIndexOffset = attributeData.ConstructorArguments.Length;

        if (parameterSymbols[parameterSymbols.Length - 1].IsParams && attributeData.ConstructorArguments[attributeData.ConstructorArguments.Length - 1].Kind is TypedConstantKind.Array)
        {
            argumentIndexOffset += attributeData.ConstructorArguments[attributeData.ConstructorArguments.Length - 1].Values.Length - 1;
        }

        for (int i = 0; i < attributeData.NamedArguments.Length; i++)
        {
            if (attributeData.NamedArguments[i].Value.Kind is TypedConstantKind.Error
                || NamedParameters.TryGetValue(attributeData.NamedArguments[i].Key, out IAttributeProperty<TDefinition> property) is false)
            {
                continue;
            }

            definition = SetNamedArgument(definition, property, attributeData.NamedArguments[i].Value);
            definition = property.Locator(definition, attributeSyntax.ArgumentList!, argumentIndexOffset + i);
        }

        return definition;
    }

    private static TDefinition SetConstructorArgument(TDefinition definition, IAttributeProperty<TDefinition> property, IReadOnlyList<TypedConstant> arguments, int argumentIndex)
    {
        if (arguments[argumentIndex].Kind is TypedConstantKind.Array)
        {
            return property.Setter(definition, ParseArray(arguments[argumentIndex]));
        }

        return property.Setter(definition, arguments[argumentIndex].Value);
    }

    private static TDefinition SetLastConstructorArgument(TDefinition definition, IAttributeProperty<TDefinition> property, IReadOnlyList<TypedConstant> arguments, int firstIndex)
    {
        object?[] array = new object?[arguments.Count - firstIndex];

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = arguments[firstIndex + i].Value;
        }

        return property.Setter(definition, array);
    }

    private static TDefinition SetNamedArgument(TDefinition definition, IAttributeProperty<TDefinition> property, TypedConstant value)
    {
        if (value.Kind is TypedConstantKind.Array)
        {
            return property.Setter(definition, ParseArray(value));
        }

        return property.Setter(definition, value.Value);
    }

    private static TDefinition AddAttributeLocation(TDefinition definition, AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax.Name is QualifiedNameSyntax qualifiedName)
        {
            return definition.WithLocations(definition.Locations.WithAttribute(attributeSyntax.GetLocation().Minimize(), qualifiedName.Right.GetLocation().Minimize()));
        }

        return definition.WithLocations(definition.Locations.WithAttribute(attributeSyntax.GetLocation().Minimize(), attributeSyntax.Name.GetLocation().Minimize()));
    }

    private static object?[]? ParseArray(TypedConstant value)
    {
        if (value.Values.IsDefault)
        {
            return null;
        }

        ImmutableArray<TypedConstant> arrayConstants = value.Values;
        object?[] arrayValues = new object[arrayConstants.Length];
        for (int j = 0; j < arrayConstants.Length; j++)
        {
            arrayValues[j] = arrayConstants[j].Value;
        }

        return arrayValues;
    }
}
